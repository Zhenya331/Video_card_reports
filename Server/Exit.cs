using Server.SQLResult;
using System;
using System.Net.Sockets;

namespace Server
{
    class Exit
    {
        private Socket Listener;
        private string data;

        public Exit(Socket listener, string information)
        {
            Listener = listener;
            data = information;
        }

        public void Result()
        {
            SQLEvent sqlEvent = new SQLEvent($" Пользователь {Code.Decoding(data.ToString(), Code.exit)} вышел");
            sqlEvent.Start();
        }
    }
}
