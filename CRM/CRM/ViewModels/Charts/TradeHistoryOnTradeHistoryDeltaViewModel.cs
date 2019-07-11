using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class TradeHistoryOnTradeHistoryDeltaViewModel
    {
        public TradeHistoryOnTradeHistoryDeltaViewModel()
        {
            DatesTH = new List<long>();
            DatesTHD = new List<long>();
            THValues = new List<string>();
            THDValues = new List<string>();
        }
        public string CalculatingStartDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Base { get; set; }

        public List<long> DatesTH { get; set; }
        public List<long> DatesTHD { get; set; }
        public List<string> THValues { get; set; }
        public List<string> THDValues { get; set; }
    }
}
