using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client.SQLResult
{
    class ManMemEachYear : AbstractSQL<InpManMemEachYear, ResManMemEachYear>, IDisposable
    {
        private string User;
        private string Path;

        public ManMemEachYear(InpManMemEachYear input, string UserName, string path) : base()
        {
            data.ManName = input.ManName;
            User = UserName;
            Path = path;
        }

        protected override string DataToString()
        {
            string DataStr = "";
            DataStr += Code.ManMemEaYe + User + '|';
            DataStr += data.ManName + ',';
            return DataStr;
        }

        protected override void StringToOut(string result)
        {
            ResManMemEachYear buffer;
            buffer.Year = 0; buffer.Min = 0; buffer.Max = 0; buffer.Avg = 0;
            string ResOut = Code.Decoding(result);
            string buf = "";
            int j = 0;
            for (int i = 0; i < ResOut.Length; i++)
            {
                if (ResOut[i] == ',')
                {
                    if (j == 0)
                    {
                        buffer.Year = Int32.Parse(buf);
                        j++;
                        buf = "";
                        continue;
                    }
                    if (j == 1)
                    {
                        buffer.Min = Int32.Parse(buf);
                        j++;
                        buf = "";
                        continue;
                    }
                    if (j == 2)
                    {
                        buffer.Max = Int32.Parse(buf);
                        j++;
                        buf = "";
                        continue;
                    }
                    if (j == 3)
                    {
                        buffer.Avg = Int32.Parse(buf);
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
                string filename = time + "-" + User + "-" + "ManMemEachYear" + ".txt";

                using (FileStream fstream = new FileStream(Path + filename, FileMode.Create))
                {
                    string Param = data.ManName;
                    byte[] array = Encoding.Default.GetBytes("Manufactory name = " + Param + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);

                    string Header = "Year" + "\t" + "Max" + "\t" + "Min" + "\t" + "Avg";
                    array = Encoding.Default.GetBytes(Header + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);

                    foreach (var res in Res)
                    {
                        string toSend = res.Year.ToString() + "\t" + res.Max.ToString() + "\t" + res.Min.ToString() + "\t" + res.Avg.ToString();
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
