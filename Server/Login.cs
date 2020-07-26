using Server.SQLResult;
using System;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace Server
{
    class Login
    {
        private Socket Listener;
        private string data;
        private string Username = "";
        private string Password = "";
        private const char delimeter = ' ';

        public Login(Socket listener, string information)
        {
            Listener = listener;
            data = information;
        }

        private (string, string) Split(string data)
        {
            string UserData = "";
            string PassData = "";
            bool flag = true;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == delimeter)
                {
                    flag = false;
                    continue;
                }
                if (flag)
                {
                    UserData += data[i];
                }
                else
                {
                    PassData += data[i];
                }
            }
            return (UserData, PassData);
        }

        private string CheckUserSQL(string Name)
        {
            using (var user = new Model1())
            {
                var nm = new SqlParameter("@name", Name);
                var us = user.Database.SqlQuery<Users>("SELECT * FROM Users AS U WHERE U.[Username] = @name", nm);
                string result = "NotFindError";
                foreach (var res in us)
                {
                    result = res.Username + delimeter + res.Password;
                    break;
                }
                return result;
            }
        }

        private bool Check()
        {
            string DataDecode = Code.Decoding(data, Code.login);
            (Username, Password) = Split(DataDecode);

            string buf = CheckUserSQL(Username);
            string UsernameSQL, PasswordSQL;
            (UsernameSQL, PasswordSQL) = Split(buf);

            return (Username == UsernameSQL) && (Password == PasswordSQL);
        }

        public void Result()
        {
            bool message;
            message = Check();

            string MessageData = Code.login + message.ToString();
            Program.tcpSendData(Listener, MessageData);

            if (message)
            {
                SQLEvent sqlEvent = new SQLEvent($" Пользователь {Username} авторизовался");
                sqlEvent.Start();
            }
        }
    }
}
