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
        public SignalsAnalyticsService()
        {
            SignalsPrivates = new List<SignalsPrivate>();
            TradeHistoryDeltas = new List<TradeHistoryDelta>();
        }

        public List<SignalsPrivate> SignalsPrivates { get; set; }
        public List<TradeHistoryDelta> TradeHistoryDeltas { get; set; }


        public SignalsAnalyticsViewModel LoadTradeHistoryDelta(SignalsAnalyticsViewModel model)
        {
            using (masterContext context = new masterContext())
            {
                TradeHistoryDeltas = context.TradeHistoryDelta
                    .Where(x => x.Exchange == model.Exchange && 
                    x.Base == model.Coin && 
                    model.Nullable == "notnull" ? (x.MaxLongDiff != null && x.MinLongDiff != null): true &&
                    x.TimeFrom >= DateTime.Parse(model.StartDate) && 
                    x.TimeTo <= DateTime.Parse(model.EndDate))
                    .ToList();

                model.TradeHistoryDeltas = TradeHistoryDeltas;

                model.CountOfPages = (int)Math.Ceiling((decimal)((double)model.TradeHistoryDeltas.Count / 100));

                return model;
            }
        }

        public SignalsAnalyticsViewModel LoadSignalsPrivate(SignalsAnalyticsViewModel model)
        {
            using (masterContext context = new masterContext())
            {
                SignalsPrivates = context.SignalsPrivate
                    .Where(x => x.Exchange == model.Exchange.ToLower() &&
                    x.Base == model.Coin &&
                    model.Nullable == "null" ? x.ErrorMessages == null : true &&
                    x.SourceTime >= DateTime.Parse(model.StartDate) &&
                    x.SourceTime <= DateTime.Parse(model.EndDate))
                    .ToList();

                model.SignalsPrivates = SignalsPrivates;

                model.CountOfPages = (int)Math.Ceiling((decimal)((double)model.SignalsPrivates.Count / 100));

                return model;
            }
        }
    }
}