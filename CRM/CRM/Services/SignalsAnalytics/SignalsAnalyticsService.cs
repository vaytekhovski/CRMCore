﻿using CRM.Master;
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


        public SignalsAnalyticsViewModel LoadSignalsPrivate(SignalsAnalyticsViewModel model)
        {
            using (masterContext context = new masterContext())
            {
                SignalsPrivates = context.SignalsPrivate
                    .Where(x =>
                    model.Nullable == "null" ? x.ErrorMessages == null : model.Nullable == "notnull" ? x.ErrorMessages != null : true && 
                    x.Exchange == model.Exchange.ToLower() &&
                    model.Coin != "all" ? x.Base == model.Coin : true &&
                    x.SourceTime >= DateTime.Parse(model.StartDate) &&
                    x.SourceTime <= DateTime.Parse(model.EndDate) &&
                    x.SourceTime.TimeOfDay >= TimeSpan.Parse(model.StartTime) &&
                    x.SourceTime.TimeOfDay <= TimeSpan.Parse(model.EndTime))
                    .OrderBy(x => x.SourceTime).ToList();

                model.SignalsPrivates = SignalsPrivates;


                return model;
            }
        }

        public SignalsAnalyticsViewModel LoadTradeHistoryDelta(SignalsAnalyticsViewModel model)
        {
            using (masterContext context = new masterContext())
            {
                TradeHistoryDeltas = context.TradeHistoryDelta
                    .Where(x =>
                    (model.Nullable == "notnull" ? (x.MaxLongDiff != null && x.MinLongDiff != null) : model.Nullable == "null" ? (x.MaxLongDiff == null && x.MinLongDiff == null) : true) &&
                    x.Exchange == model.Exchange.ToLower() && 
                    model.Coin != "all" ? x.Base == model.Coin : true && 
                    x.TimeFrom >= DateTime.Parse(model.StartDate) && 
                    x.TimeTo <= DateTime.Parse(model.EndDate) &&
                    x.TimeFrom.TimeOfDay >= TimeSpan.Parse(model.StartTime) &&
                    x.TimeTo.TimeOfDay <= TimeSpan.Parse(model.EndTime))
                    .ToList();

                model.TradeHistoryDeltas = TradeHistoryDeltas;


                return model;
            }
        }

        
    }
}