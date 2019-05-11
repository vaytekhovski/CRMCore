using CRM.Master;
using CRM.Models.Master;
using CRM.Models.SignalsAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.SignalsAnalytics
{
    public class SignalsAnalyticsService
    {
        public static List<TradeHistoryDelta> LoadTradeHistoryDelta(string exchange, string coin, DateTime StartDate, DateTime EndDate)
        {
            using (masterContext context = new masterContext())
            {
                return context.TradeHistoryDelta.Where(x => x.Exchange == exchange && x.Base == coin && x.TimeFrom >= StartDate && x.TimeTo <= EndDate).ToList();
            }
        }

        public static List<SignalsPrivate> LoadSignalsPrivate(string exchange, string coin, DateTime StartDate, DateTime EndDate)
        {
            using (masterContext context = new masterContext())
            {
                return context.SignalsPrivate.Where(x => x.Exchange == exchange && x.Base == coin && x.SourceTime >= StartDate && x.SourceTime <= EndDate).ToList();
            }
        }
    }
}