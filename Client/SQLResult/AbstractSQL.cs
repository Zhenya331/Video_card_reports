using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    abstract class AbstractSQL<Input, Output>
    {
        protected List<Output> Res = new List<Output>();
        protected Input data;
        private IPEndPoint tcpEndPoint;
        protected Socket tcpSocket;
        private event Action Log;
        
        public AbstractSQL()
        {
            tcpEndPoint = new IPEndPoint(IPAddress.Parse(MainWindow.ip), MainWindow.port);
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Connect(tcpEndPoint);
        }

        // struct data convert to string
        protected abstract string DataToString();

        // result convert to struct Output in List Res
        protected abstract void StringToOut(string result);

        // Результат выводится в блокнот
        protected abstract void EventTxt();

        private void SendData()
        {
            string sendData = DataToString();
            MainWindow.tcpSendData(tcpSocket, sendData);
        }

        private void GetData()
        {
            StringBuilder res = MainWindow.tcpGetData(tcpSocket);
            StringToOut(res.ToString());
        }

        public void Result()
        {
            SendData();
            GetData();
            Log += EventTxt;
            Log?.Invoke();
        }
    }
}
