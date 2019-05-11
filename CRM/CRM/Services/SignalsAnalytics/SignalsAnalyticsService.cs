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
        private static List<SignalsAnalyticsModel> SignalsAnalytics { get; set; }

        public static List<SignalsAnalyticsModel> Load(string exchange, string coin, DateTime StartDate, DateTime EndDate)
        {
            using (masterContext context = new masterContext())
            {
                var buf = context.TradeHistoryDelta.Where(x => x.Exchange == exchange && x.Base == coin && x.TimeFrom >= StartDate && x.TimeTo <= EndDate)
                     .Join(context.SignalsPrivate,
                     p => p.Id,
                     c => c.Id,
                     (p, c) => new
                     {
                         TimeFrom = p.TimeFrom,
                         TimeTo = p.TimeTo,
                         Situation = p.Situation,
                         Value = p.Value,
                         MinLongDiff = p.MinLongDiff,
                         MaxLongDiff = p.MaxLongDiff,
                         TrendDiff = c.TrendDiff,
                         ErrorMessages = c.ErrorMessages
                     }).ToList();

                foreach (var item in buf)
                {
                    SignalsAnalytics.Add(new SignalsAnalyticsModel
                    {
                        TimeFrom = item.TimeFrom,
                        TimeTo = item.TimeTo,
                        Situation = item.Situation,
                        Value = item.Value,
                        MinLongDiff = item.MinLongDiff,
                        MaxLongDiff = item.MaxLongDiff,
                        TrendDiff = item.TrendDiff,
                        ErrorMessages = item.ErrorMessages
                    });
                }
            }

            return SignalsAnalytics;
        }
    }
}