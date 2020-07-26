
namespace Client
{
    class Code
    {
        public const string login = "_";
        public const string exit = "__";
        public const string ArchCostAvg = "___";
        public const string ManEnBet = "____";
        public const string ManMemEaYe = "_____";

        // Проверка введенных данных
        public static bool Check(string data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '_')
                {
                    return false;
                }
            }
            return true;
        }

        // Раскодировать данные
        public static string Decoding(string data)
        {
            string result = "";

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != '_')
                {
                    result += data[i];
                }
            }

            return result;
        }

        // Проверка на недопустимые символы (они используются при передеаче данных)
        public static bool CheckSQL(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] == ',') || (str[i] == '|') || (str[i] == ';'))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
