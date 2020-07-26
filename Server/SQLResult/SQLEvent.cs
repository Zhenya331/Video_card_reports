using System;
using System.IO;
using System.Text;

namespace Server.SQLResult
{
    class SQLEvent
    {
        public event Action Log;
        private readonly string information;
        private readonly DateTime date;
        private readonly string time;

        public SQLEvent(string log)
        {
            information = log;
            date = DateTime.Now;
            time = "(" + date.Day.ToString() + "-" + date.Month.ToString() + "-" + date.Year.ToString() + ";" + date.Hour.ToString() + "-" + date.Minute.ToString() + "-" + date.Second.ToString() + ")";

            Log += LogConsole;
            Log += LogTxt;
        }

        public void Start()
        {
            Log?.Invoke();
        }

        private void LogConsole()
        {
            Console.WriteLine(time + information);
        }

        private void LogTxt()
        {
            string Path = @"Logs\";
            string filename = date.Day.ToString() + "-" + date.Month.ToString() + "-" + date.Year.ToString() + ".txt";

            using (FileStream fstream = new FileStream(Path + filename, FileMode.Append))
            {
                byte[] array = Encoding.Default.GetBytes(time + information + Environment.NewLine);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
