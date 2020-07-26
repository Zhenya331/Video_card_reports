using Server.SQLResult;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        //public const string ip = "127.0.0.1";
        public const int port = 8080;
        private const int backlog = 5;

        public static StringBuilder tcpGetData(Socket listener)
        {
            var buffer = new byte[256];
            int size = 0;
            var data = new StringBuilder();
            do
            {
                size = listener.Receive(buffer);
                data.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (listener.Available > 0);
            return data;
        }

        public static void tcpSendData(Socket listener, string data)
        {
            listener.Send(Encoding.UTF8.GetBytes(data));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Begin server");

            IPEndPoint tcpEndPoint = new IPEndPoint(IPAddress.Any, port);
            Socket tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(backlog);

            while (true)
            {
                Socket Listener = tcpSocket.Accept();
                StringBuilder data = tcpGetData(Listener);
                string CodeName = Code.GetCode(data.ToString());

                switch (CodeName)
                {
                    case Code.login:
                        Login login = new Login(Listener, data.ToString());
                        login.Result();
                        break;

                    case Code.exit:
                        Exit exit = new Exit(Listener, data.ToString());
                        exit.Result();
                        break;

                    case Code.ArchCostAvg:
                        ArCostAvg arCostAvg = new ArCostAvg(Listener, data.ToString());
                        arCostAvg.Result();
                        break;

                    case Code.ManEnBet:
                        ManEnergyBetween manEnergyBetween = new ManEnergyBetween(Listener, data.ToString());
                        manEnergyBetween.Result();
                        break;

                    case Code.ManMemEaYe:
                        ManMemEachYear manMemEachYear = new ManMemEachYear(Listener, data.ToString());
                        manMemEachYear.Result();
                        break;
                }

                Listener.Shutdown(SocketShutdown.Both);
                Listener.Close();
            }

            Console.WriteLine("End server");
            Console.ReadLine();
        }

    }
}
