using System;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Exit : IDisposable
    {
        private IPEndPoint tcpEndPointExit;
        private Socket tcpSocketExit;
        private string ExitUser;

        public Exit(string UserName)
        {
            ExitUser = Code.exit + UserName;
            tcpEndPointExit = new IPEndPoint(IPAddress.Parse(MainWindow.ip), MainWindow.port);
            tcpSocketExit = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocketExit.Connect(tcpEndPointExit);
        }

        public void Result()
        {
            MainWindow.tcpSendData(tcpSocketExit, ExitUser);
        }

        public void Dispose()
        {
            tcpSocketExit.Shutdown(SocketShutdown.Both);
            tcpSocketExit.Close();
        }
    }
}
