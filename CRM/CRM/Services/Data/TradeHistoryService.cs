using CRM.Models;
using CRM.Models.Database;
using CRM.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Data
{
    public class TradeHistoryService
    {
        public double SummVolume { get; private set; }
        public List<TradeHistoryModel> Show { get; private set; }

        public TradeHistoryService() { }

        public TradeHistoryViewModel Load(string coin, string situation, string orderType, DateTime startDate, DateTime endDate)
        {
            if (startDate == null && endDate == null)
                return null;

            using (CRMContext context = new CRMContext())
            {
                Show = context.TradeHistoryModels
                    .Where(x => x.Date >= startDate && x.Date <= endDate)
                    .Where(x => coin == null ? true : x.CurrencyName == coin)
                    .Where(x => situation == null ? true : x.MarketSituation == situation)
                    .Where(x => orderType == null ? true : x.Side == orderType)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                SummVolume = Show.Sum(item => item.Volume);
            }

            TradeHistoryViewModel model = new TradeHistoryViewModel
            {
                Show = Show,
                SummVolume = SummVolume
            };

            return model;
        }
    }
}
