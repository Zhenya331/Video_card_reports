using Server.sql_Query;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace Server.SQLResult
{
    class ManMemEachYear : AbstractSQL<InpManMemEachYear, ResManMemEachYear>
    {
        public ManMemEachYear(Socket listener, string information) : base(listener, information)
        {

        }

        protected override void StringToData()
        {
            dataStr = Code.Decoding(dataStr, Code.ManMemEaYe);
            string buf = "";

            int j = 0;
            for (int i = 0; i < dataStr.Length; i++)
            {
                if (dataStr[i] == '|')
                {
                    UserName = buf;
                    buf = "";
                    continue;
                }
                if (dataStr[i] == ',')
                {
                    if (j == 0)
                    {
                        data.ManName = buf;
                        buf = "";
                        //j++;
                        continue;
                    }
                }
                buf += dataStr[i];
            }

            ReportDescription = "Manufactory memory each year: Manufact Name = " + data.ManName;
        }

        protected override void Query()
        {
            using (var vc = new Videocards())
            {
                var mn = new SqlParameter("@Manufact_Name", data.ManName);
                var result = vc.Database.SqlQuery<sql_Manufact_memory_each_year_Result>("sql_Manufact_memory_each_year @Manufact_Name", mn);
                ResManMemEachYear buf;
                foreach (var res in result)
                {
                    buf.Year = res.Год;
                    buf.Min = res.Минимальный;
                    buf.Max = res.Максимальный;
                    buf.Avg = res.Средний;
                    Res.Add(buf);
                }
            }
        }

        protected override string OutToString()
        {
            string result = "";
            result += Code.ManMemEaYe;

            foreach (var res in Res)
            {
                result += res.Year.ToString() + ',';
                result += res.Min.ToString() + ',';
                result += res.Max.ToString() + ',';
                result += res.Avg.ToString() + ',';
                result += ';';
            }

            return result;
        }

        protected override void ChangeEvent()
        {
            
        }
    }
}
