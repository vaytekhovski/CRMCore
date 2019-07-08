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
                    .Where(x => x.SourceTime >= DateTime.Parse(ViewModel.StartDate))
                    .Where(x => x.SourceTime <= DateTime.Parse(ViewModel.EndDate))
                    .OrderByDescending(x => x.SourceTime)
                    .ToList();

                if (ViewModel.Coin != null)
                    SignalsPrivates = SignalsPrivates.Where(x => x.Base == ViewModel.Coin).ToList();

                if (ViewModel.Nullable != null)
                    SignalsPrivates = SignalsPrivates.Where(x => ViewModel.Nullable == "notnull" ? x.ErrorMessages != null : x.ErrorMessages == null).ToList();

                if (ViewModel.Exchange != null)
                    SignalsPrivates = SignalsPrivates.Where(x => x.Exchange == ViewModel.Exchange.ToLower()).ToList();

                SignalsAnalyticsViewModel model = new SignalsAnalyticsViewModel
                {
                    SignalsPrivates = SignalsPrivates
                };

                return model;
            }
        }

        public SignalsAnalyticsViewModel LoadTradeHistoryDelta(SignalsAnalyticsViewModel ViewModel)
        {
            using (masterContext context = new masterContext())
            {
                TradeHistoryDeltas = context.TradeHistoryDelta
                    .Where(x => x.TimeFrom >= DateTime.Parse(ViewModel.StartDate))
                    .Where(x => x.TimeTo <= DateTime.Parse(ViewModel.EndDate))
                    .OrderByDescending(x => x.TimeFrom)
                    .ToList();

                if (ViewModel.Coin != null)
                    TradeHistoryDeltas = TradeHistoryDeltas.Where(x => x.Base == ViewModel.Coin).ToList();

                if (ViewModel.Nullable != null)
                {
                    if (ViewModel.Nullable == "notnull")
                        TradeHistoryDeltas = TradeHistoryDeltas.Where(x => x.MaxLongDiff != null).Where(x => x.MinLongDiff != null).ToList();
                    else
                        TradeHistoryDeltas = TradeHistoryDeltas.Where(x => x.MaxLongDiff == null).Where(x => x.MinLongDiff == null).ToList();
                }

                if (ViewModel.Situation != null)
                    TradeHistoryDeltas = TradeHistoryDeltas.Where(x => x.Situation == ViewModel.Situation.ToLower()).ToList();

                if (ViewModel.Exchange != null)
                    TradeHistoryDeltas = TradeHistoryDeltas.Where(x => x.Exchange == ViewModel.Exchange.ToLower()).ToList();


                SignalsAnalyticsViewModel model = new SignalsAnalyticsViewModel
                {
                    TradeHistoryDeltas = TradeHistoryDeltas
                };

                return model;
            }
        }

        
    }
}