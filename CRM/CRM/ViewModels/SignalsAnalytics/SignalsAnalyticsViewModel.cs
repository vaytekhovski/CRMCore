using CRM.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.SignalsAnalytics
{
    public class SignalsAnalyticsViewModel
    {
        public string Exchange { get; set; }
        public string Coin { get; set; }
        public string Nullable { get; set; }
        public string StartDate { get; set; }
        public int CountOfPages { get; set; }

        public int MaxAvailablePageNumber { get; set; }
        public int CurrentPage { get; set; }
        public string EndDate { get; set; }

        public List<SignalsPrivate> SignalsPrivates { get; set; }
        public List<TradeHistoryDelta> TradeHistoryDeltas { get; set; }
    }
}
