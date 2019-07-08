using CRM.Models;
using CRM.Models.Database;
using CRM.Models.Filters;
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

        public TradeHistoryViewModel Load(DataFilter filter)
        {
            using (CRMContext context = new CRMContext())
            {
                    Show = context.TradeHistoryModels
                        .Where(x => x.Side == filter.OrderType)
                        .Where(x => x.CurrencyName == null ? true : x.CurrencyName == filter.Coin)
                        .Where(x => x.MarketSituation == null ? true : x.MarketSituation == filter.Situation)
                        .Where(x => x.Date >= filter.StartDate && x.Date <= filter.EndDate)
                        .OrderByDescending(x => x.Date)
                        .ToList();

                    Show = Show.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();
           

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
