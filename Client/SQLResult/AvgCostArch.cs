using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client.SQLResult
{
    class AvgCostArch : AbstractSQL<InpArchCostAvg, ResArchCostAvg>, IDisposable
    {
        private string User;
        private string Path;

        public AvgCostArch(InpArchCostAvg input, string Username, string path): base()
        {
            data.Arch = input.Arch;
            User = Username;
            Path = path;
        }

        protected override string DataToString()
        {
            string DataStr = "";
            DataStr += Code.ArchCostAvg + User + '|';
            DataStr += data.Arch + ',';
            return DataStr;
        }

        protected override void StringToOut(string result)
        {
            ResArchCostAvg buffer;
            buffer.Name = ""; buffer.Cost = 0;
            string ResOut = Code.Decoding(result);
            string buf = "";
            int j = 0;
            for (int i = 0; i < ResOut.Length; i++)
            {
                if(ResOut[i] == ',')
                {
                    if (j == 0)
                    {
                        buffer.Name = buf;
                        j++;
                        buf = "";
                        continue;
                    }
                    if (j == 1)
                    {
                        buffer.Cost = Int32.Parse(buf);
                        j = 0;
                        buf = "";
                        continue;
                    }
                }
                if (ResOut[i] == ';')
                {
                    Res.Add(buffer);
                    continue;
                }
                buf += ResOut[i];
            }
        }

        protected override void EventTxt()
        {
            try
            {
                DateTime date = DateTime.Now;
                string time = "(" + date.Day.ToString() + "-" + date.Month.ToString() + "-" + date.Year.ToString() + ";" + date.Hour.ToString() + "-" + date.Minute.ToString() + "-" + date.Second.ToString() + ")";
                string filename = time + "-" + User + "-" + "AvgCostArch" + ".txt";

                using (FileStream fstream = new FileStream(Path + filename, FileMode.Create))
                {
                    string Param = data.Arch;
                    byte[] array = Encoding.Default.GetBytes("Architecture name = " + Param + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);

                    string Header = "Name" + "\t" + "Cost";
                    array = Encoding.Default.GetBytes(Header + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);

                    foreach (var res in Res)
                    {
                        string toSend = res.Name + "\t" + res.Cost.ToString();
                        array = Encoding.Default.GetBytes(toSend + Environment.NewLine);
                        fstream.Write(array, 0, array.Length);
                    }
                }
                MessageBox.Show("Отчет получен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Dispose()
        {
            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
        }
    }
}
