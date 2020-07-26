
namespace Client.SQLResult
{
    // Эти структуры будут содержать в себе 
    // результаты выполнения запросов
    struct ResArchCostAvg
    {
        public string Name;
        public int? Cost;
    }
    struct ResManEnergBeetwen
    {
        public string Name;
        public int Energy;
    }
    struct ResManMemEachYear
    {
        public int Year;
        public int? Min;
        public int? Max;
        public int? Avg;
    }

    // Эти структуры содержат входящую информацию
    struct InpArchCostAvg
    {
        public string Arch;
    }
    struct InpManEnergBeetw
    {
        public string ManName;
        public int MinEnerg;
        public int MaxEnerg;
    }
    struct InpManMemEachYear
    {
        public string ManName;
    }
}
