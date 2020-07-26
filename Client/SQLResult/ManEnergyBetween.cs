using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client.SQLResult
{
    class ManEnergyBetween : AbstractSQL<InpManEnergBeetw, ResManEnergBeetwen>, IDisposable
    {
        private string User;
        private string Path;

        public ManEnergyBetween(InpManEnergBeetw input, string UserName, string path) : base()
        {
            data.ManName = input.ManName;
            data.MinEnerg = input.MinEnerg;
            data.MaxEnerg = input.MaxEnerg;
            User = UserName;
            Path = path;
        }

        protected override string DataToString()
        {
            string DataStr = "";
            DataStr += Code.ManEnBet + User + '|';
            DataStr += data.ManName + ',';
            DataStr += data.MinEnerg.ToString() + ',';
            DataStr += data.MaxEnerg.ToString() + ',';
            return DataStr;
        }

        protected override void StringToOut(string result)
        {
            ResManEnergBeetwen buffer;
            buffer.Name = ""; buffer.Energy = 0;
            string ResOut = Code.Decoding(result);
            string buf = "";
            int j = 0;
            for (int i = 0; i < ResOut.Length; i++)
            {
                if (ResOut[i] == ',')
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
                        buffer.Energy = Int32.Parse(buf);
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
                string filename = time + "-" + User + "-" + "ManEnergyBetween" + ".txt";

                using (FileStream fstream = new FileStream(Path + filename, FileMode.Create))
                {
                    string Param = data.ManName;
                    byte[] array = Encoding.Default.GetBytes("Manufactory name = " + Param + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);
                    Param = data.MinEnerg.ToString();
                    array = Encoding.Default.GetBytes("Min energy use = " + Param + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);
                    Param = data.MaxEnerg.ToString();
                    array = Encoding.Default.GetBytes("Max energy use = " + Param + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);

                    string Header = "Name" + "\t" + "Energy Use";
                    array = Encoding.Default.GetBytes(Header + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);

                    foreach (var res in Res)
                    {
                        string toSend = res.Name + "\t" + res.Energy.ToString();
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
