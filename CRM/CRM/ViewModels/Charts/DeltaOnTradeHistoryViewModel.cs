using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class DeltaOnTradeHistoryViewModel
    {
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<long> DatesDelta { get; set; }
        public List<string> DeltaValues { get; set; }

        public List<long> DatesTHBuy { get; set; }
        public List<string> THBuyValues { get; set; }

        public List<long> DatesTHSell { get; set; }
        public List<string> THSellValues { get; set; }
    }
}
