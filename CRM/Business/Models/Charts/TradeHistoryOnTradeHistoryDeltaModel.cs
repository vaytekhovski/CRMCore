using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class TradeHistoryOnTradeHistoryDeltaModel
    {
        public TradeHistoryOnTradeHistoryDeltaModel()
        {
            DatesTH = new List<DateTime>();
            DatesTHD = new List<DateTime>();
            THValues = new List<decimal>();
            THDValues = new List<decimal>();
        }

        public List<DateTime> DatesTH { get; set; }
        public List<DateTime> DatesTHD { get; set; }
        public List<decimal> THValues { get; set; }
        public List<decimal> THDValues { get; set; }
    }
}
