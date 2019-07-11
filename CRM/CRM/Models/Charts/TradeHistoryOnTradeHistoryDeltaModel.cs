using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.Charts
{
    public class TradeHistoryOnTradeHistoryDeltaModel
    {
        public List<DateTime> DatesTH { get; set; }
        public List<DateTime> DatesTHD { get; set; }
        public List<decimal> THValues { get; set; }
        public List<decimal> THDValues { get; set; }
    }
}
