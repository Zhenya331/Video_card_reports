using Server.sql_Query;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace Server.SQLResult
{
    class ArCostAvg : AbstractSQL<InpArchCostAvg, ResArchCostAvg>
    {
        public ArCostAvg(Socket listener, string information) : base(listener, information)
        {

        }

        protected override void StringToData()
        {
            dataStr = Code.Decoding(dataStr, Code.ArchCostAvg);
            string buf = "";

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
                    data.Arch = buf;
                    buf = "";
                    continue;
                }
                buf += dataStr[i];
            }

            ReportDescription = "Architecture cost average: Architecture Name = " + data.Arch;
        }

        protected override void Query()
        {
            using (var vc = new Videocards())
            {
                var param = new SqlParameter("@Arch_Name", data.Arch);
                var result = vc.Database.SqlQuery<sql_Arch_cost_avg_Result>("sql_Arch_cost_avg @Arch_Name", param);

                ResArchCostAvg buf;
                foreach (var res in result)
                {
                    buf.Name = res.Название;
                    buf.Cost = res.Цена;
                    Res.Add(buf);
                }
            }
        }

        protected override string OutToString()
        {
            string result = "";
            result += Code.ArchCostAvg;

            foreach(var res in Res)
            {
                result += res.Name + ',';
                result += res.Cost.ToString() + ',';
                result += ';';
            }

            return result;
        }

        protected override void ChangeEvent()
        {
            
        }
    }
}
