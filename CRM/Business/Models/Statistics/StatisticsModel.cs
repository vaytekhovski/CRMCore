using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class StatisticsModel
    {
        public List<StatisticsElement> Statistics { get; set; }
        public int CountOfPages { get; set; }
    }

    public class StatisticsElement
    {
        public DateTime Date { get; set; }
        public decimal ProfitOfDay { get; set; }
        public decimal TotalProfit { get; set; }

    }
}
