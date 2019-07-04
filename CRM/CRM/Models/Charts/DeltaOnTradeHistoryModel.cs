using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.Charts
{
    public class DeltaOnTradeHistoryModel
    {
        public List<DateTime> DatesDelta { get; set; }
        public List<double> DeltaValues { get; set; }

        public List<DateTime> DatesTHBuy { get; set; }
        public List<double> THBuyValues { get; set; }

        public List<DateTime> DatesTHSell { get; set; }
        public List<double> THSellValues { get; set; }
    }
}
