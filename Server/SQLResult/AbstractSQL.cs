using System.Collections.Generic;
using System.Net.Sockets;

namespace Server.SQLResult
{
    abstract class AbstractSQL<Input, Output>
    {
        protected List<Output> Res = new List<Output>();
        protected Input data;
        protected string dataStr;
        protected string UserName;
        protected string ReportDescription;
        protected Socket Listener;
        protected SQLEvent sqlEvent;

        public AbstractSQL(Socket listener, string information)
        {
            Listener = listener;
            dataStr = information;
        }

        // string dataStr convert to Input data
        // При выполнении данного метода будут заполнены
        // Input data и string UserName
        protected abstract void StringToData();

        // get Output Res
        protected abstract void Query();

        // result Output Res convert to string
        protected abstract string OutToString();

        // Возможно не каждому отчету нужны все методы в событии SQLEvent.Log
        // Здесь предоставляется возможность их убрать
        protected abstract void ChangeEvent();

        public void Result()
        {
            StringToData();
            Query();
            string SendData = OutToString();
            Program.tcpSendData(Listener, SendData);

            string log = " Пользователь " + UserName + " запросил отчет " + ReportDescription;
            sqlEvent = new SQLEvent(log);
            ChangeEvent();
            sqlEvent.Start();
        }
    }
}
