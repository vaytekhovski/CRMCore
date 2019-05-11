using CRM.Master;
using CRM.Models.SignalsAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.SignalsAnalytics
{
    public class SignalsAnalyticsService
    {
        private List<SignalsAnalyticsModel> SignalsAnalytics { get; set; }

        public List<SignalsAnalyticsModel> Load(string exchange, string coin, DateTime StartDate, DateTime EndDate)
        {
            using (masterContext context = new masterContext())
            {
                context.TradeHistoryDelta ...
            }


            return SignalsAnalytics;
        }
    }
}
