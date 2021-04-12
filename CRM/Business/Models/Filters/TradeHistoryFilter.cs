using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class TradeHistoryFilter
    {
        public string Account { get; set; }
        public string Coin { get; set; }
        public string Quote { get; set; }
        public int CurrentPage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
