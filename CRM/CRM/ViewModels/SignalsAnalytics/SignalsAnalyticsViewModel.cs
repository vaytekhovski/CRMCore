using CRM.Models.SignalsAnalytics;
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

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<SignalsAnalyticsModel> SignalsAnalytics { get; set; }
    }
}
