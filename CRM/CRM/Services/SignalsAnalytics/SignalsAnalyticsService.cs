using CRM.Master;
using CRM.Models.Master;
using CRM.ViewModels.SignalsAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.SignalsAnalytics
{
    public class SignalsAnalyticsService
    {
        public List<SignalsPrivate> SignalsPrivates { get; set; }
        public List<TradeHistoryDelta> TradeHistoryDeltas { get; set; }

        public SignalsAnalyticsService()
        {
            SignalsPrivates = new List<SignalsPrivate>();
            TradeHistoryDeltas = new List<TradeHistoryDelta>();
        }


        public SignalsAnalyticsViewModel LoadSignalsPrivate(SignalsAnalyticsViewModel model)
        {
            using (masterContext context = new masterContext())
            {
                SignalsPrivates = context.SignalsPrivate
                    .Where(x => model.Nullable == "null" ? x.ErrorMessages == null : model.Nullable == "all" ? x.ErrorMessages != null : true)
                    .Where(x => x.Exchange == model.Exchange.ToLower())
                    .Where(x => model.Coin != "all" ? x.Base == model.Coin : true)
                    .Where(x => x.SourceTime >= DateTime.Parse(model.StartDate))
                    .Where(x => x.SourceTime <= DateTime.Parse(model.EndDate).AddDays(1))
                    .Where(x => x.SourceTime.TimeOfDay >= TimeSpan.Parse(model.StartTime))
                    .Where(x => x.SourceTime.TimeOfDay <= TimeSpan.Parse(model.EndTime))
                    .OrderByDescending(x => x.SourceTime)
                    .ToList();

                model.SignalsPrivates = SignalsPrivates;

                return model;
            }
        }

        public SignalsAnalyticsViewModel LoadTradeHistoryDelta(SignalsAnalyticsViewModel model)
        {
            model.Situation = model.Situation == null ? "all" : model.Situation;

            using (masterContext context = new masterContext())
            {
                TradeHistoryDeltas = context.TradeHistoryDelta
                    .Where(x => model.Nullable == "null" ? (x.MaxLongDiff == null && x.MinLongDiff == null) : model.Nullable == "notnull" ? (x.MaxLongDiff != null && x.MinLongDiff != null) : true)
                    .Where(x => x.Exchange == model.Exchange.ToLower())
                    .Where(x => model.Coin != "all" ? x.Base == model.Coin : true)
                    .Where(x => model.Situation != "all" ? x.Situation == model.Situation.ToLower() : true)
                    .Where(x => x.TimeFrom >= DateTime.Parse(model.StartDate))
                    .Where(x => x.TimeTo <= DateTime.Parse(model.EndDate).AddDays(1))
                    .Where(x => x.TimeFrom.TimeOfDay >= TimeSpan.Parse(model.StartTime))
                    .Where(x => x.TimeTo.TimeOfDay <= TimeSpan.Parse(model.EndTime))
                    .OrderByDescending(x => x.TimeFrom)
                    .ToList();

                model.TradeHistoryDeltas = TradeHistoryDeltas;

                return model;
            }
        }

        
    }
}