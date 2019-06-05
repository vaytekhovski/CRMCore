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


        public SignalsAnalyticsViewModel LoadSignalsPrivate(SignalsAnalyticsViewModel ViewModel)
        {
            using (masterContext context = new masterContext())
            {
                SignalsPrivates = context.SignalsPrivate
                    .Where(x => ViewModel.Nullable == "null" ? x.ErrorMessages == null : ViewModel.Nullable == "all" ? x.ErrorMessages != null : true)
                    .Where(x => x.Exchange == ViewModel.Exchange.ToLower())
                    .Where(x => ViewModel.Coin != "all" ? x.Base == ViewModel.Coin : true)
                    .Where(x => x.SourceTime >= DateTime.Parse(ViewModel.StartDate))
                    .Where(x => x.SourceTime <= DateTime.Parse(ViewModel.EndDate).AddDays(1))
                    .Where(x => x.SourceTime.TimeOfDay >= TimeSpan.Parse(ViewModel.StartTime))
                    .Where(x => x.SourceTime.TimeOfDay <= TimeSpan.Parse(ViewModel.EndTime))
                    .OrderByDescending(x => x.SourceTime)
                    .ToList();

                SignalsAnalyticsViewModel model = new SignalsAnalyticsViewModel
                {
                    SignalsPrivates = SignalsPrivates
                };

                return model;
            }
        }

        public SignalsAnalyticsViewModel LoadTradeHistoryDelta(SignalsAnalyticsViewModel ViewModel)
        {
            ViewModel.Situation = ViewModel.Situation == null ? "all" : ViewModel.Situation;

            using (masterContext context = new masterContext())
            {
                TradeHistoryDeltas = context.TradeHistoryDelta
                    .Where(x => ViewModel.Nullable == "null" ? (x.MaxLongDiff == null && x.MinLongDiff == null) : ViewModel.Nullable == "notnull" ? (x.MaxLongDiff != null && x.MinLongDiff != null) : true)
                    .Where(x => x.Exchange == ViewModel.Exchange.ToLower())
                    .Where(x => ViewModel.Coin != "all" ? x.Base == ViewModel.Coin : true)
                    .Where(x => ViewModel.Situation != "all" ? x.Situation == ViewModel.Situation.ToLower() : true)
                    .Where(x => x.TimeFrom >= DateTime.Parse(ViewModel.StartDate))
                    .Where(x => x.TimeTo <= DateTime.Parse(ViewModel.EndDate).AddDays(1))
                    .Where(x => x.TimeFrom.TimeOfDay >= TimeSpan.Parse(ViewModel.StartTime))
                    .Where(x => x.TimeTo.TimeOfDay <= TimeSpan.Parse(ViewModel.EndTime))
                    .OrderByDescending(x => x.TimeFrom)
                    .ToList();

                SignalsAnalyticsViewModel model = new SignalsAnalyticsViewModel
                {
                    TradeHistoryDeltas = TradeHistoryDeltas
                };

                return model;
            }
        }

        
    }
}