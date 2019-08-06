using Business;
using System.Collections.Generic;

namespace CRM.ViewModels.SignalsAnalytics
{
    public class SignalsAnalyticsViewModel
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public string Exchange { get; set; }
        public string Coin { get; set; }
        public string Nullable { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TypeOfDate { get; set; }
        public int CountOfPages { get; set; }
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }
        public string Situation { get; set; }

        public int MaxAvailablePageNumber { get; set; }
        public int CurrentPage { get; set; }

        public List<SignalsPrivate> SignalsPrivates { get; set; }
        public List<TradeHistoryDelta> TradeHistoryDeltas { get; set; }
    }
}
