using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client
{
    class LoginClass : IDisposable
    {
        private IPEndPoint tcpEndPointLogin;
        private Socket tcpSocketLogin;
        private string User;
        private string Pass;
        private const char delimeter = ' ';

        public LoginClass(string Username, string Password)
        {
            User = Username;
            Pass = Password;
            tcpEndPointLogin = new IPEndPoint(IPAddress.Parse(MainWindow.ip),MainWindow.port);
            tcpSocketLogin = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocketLogin.Connect(tcpEndPointLogin);
        }

        public static bool CheckDelimiter(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == delimeter)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Result()
        {
            string data = Code.login + User + delimeter + Pass;
            MainWindow.tcpSendData(tcpSocketLogin, data);

            StringBuilder res = MainWindow.tcpGetData(tcpSocketLogin);
            string result = Code.Decoding(res.ToString());

            if (result == "True")
            {
                return true;
            }

            MessageBox.Show("Неправильное имя пользователя или пароль");
            return false;
        }

        public void Dispose()
        {
            tcpSocketLogin.Shutdown(SocketShutdown.Both);
            tcpSocketLogin.Close();
        }
    }
}
