
namespace Server
{
    class Code
    {
        public const string login = "_";
        public const string exit = "__";
        public const string ArchCostAvg = "___";
        public const string ManEnBet = "____";
        public const string ManMemEaYe = "_____";

        // Раскодировать данные кодом code
        public static string Decoding(string data, string code)
        {
            string result = "";
            
            for (int i = code.Length; i < data.Length; i++)
            {
                result += data[i];
            }

            return result;
        }

        // Получить код из data
        public static string GetCode(string data)
        {
            string code = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '_')
                {
                    code += data[i];
                }
                else { break; }
            }
            return code;
        }
    }
}
