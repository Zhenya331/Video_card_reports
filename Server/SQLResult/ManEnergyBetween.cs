using Server.sql_Query;
using System;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace Server.SQLResult
{
    class ManEnergyBetween : AbstractSQL<InpManEnergBeetw, ResManEnergBeetw>
    {
        public ManEnergyBetween(Socket listener, string information) : base(listener, information)
        {

        }

        protected override void StringToData()
        {
            dataStr = Code.Decoding(dataStr, Code.ManEnBet);
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
                        j++;
                        continue;
                    }
                    if (j == 1)
                    {
                        data.MinEnerg = Int32.Parse(buf);
                        buf = "";
                        j++;
                        continue;
                    }
                    if (j == 2)
                    {
                        data.MaxEnerg = Int32.Parse(buf);
                        buf = "";
                        j = 0;
                        continue;
                    }
                }
                buf += dataStr[i];
            }

            ReportDescription = "Manufactory energy between: Manufact Name = " + data.ManName + ", Min Energy = " + data.MinEnerg.ToString() + ", Max Energy = " + data.MaxEnerg.ToString();
        }

        protected override void Query()
        {
            using (var vc = new Videocards())
            {
                var mn = new SqlParameter("@Manufact_name", data.ManName);
                var minE = new SqlParameter("@Min_Energy", data.MinEnerg);
                var maxE = new SqlParameter("@Max_Energy", data.MaxEnerg);
                var result = vc.Database.SqlQuery<sql_Manufact_energy_between_Result>("sql_Manufact_energy_between @Manufact_name, @Min_Energy, @Max_Energy", mn, minE, maxE);

                ResManEnergBeetw buf;
                foreach (var res in result)
                {
                    buf.Name = res.Название;
                    buf.Energy = res.Energy_use;
                    Res.Add(buf);
                }
            }
        }

        protected override string OutToString()
        {
            string result = "";
            result += Code.ManEnBet;

            foreach (var res in Res)
            {
                result += res.Name + ',';
                result += res.Energy.ToString() + ',';
                result += ';';
            }

            return result;
        }

        protected override void ChangeEvent()
        {
            
        }
    }
}
