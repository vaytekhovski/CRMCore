using Business;
using Business.Contexts;
using CRM.ViewModels.Data;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Data
{
    public class TradeHistoryService
    {
        public double SummVolume { get; private set; }
        public List<Business.Data.TradeHistoryModel> Show { get; private set; }

        public TradeHistoryService() { }

        public TradeHistoryViewModel Load(DataFilter filter)
        {
            int countOfElements = 0;
            using (BasicContext context = new BasicContext())
            {
                Show = context.TradeHistoryModels
                    .Where(x => filter.OrderType == null ? true : x.Side == filter.OrderType)
                    .Where(x => filter.Coin == null ? true : x.CurrencyName == filter.Coin)
                    .Where(x => filter.Situation == null ? true : x.MarketSituation == filter.Situation)
                    .Where(x => x.Date >= filter.StartDate && x.Date <= filter.EndDate)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                countOfElements = Show.Count();
                SummVolume = Show.Sum(item => item.Volume);
                Show = Show.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();
           
               
            }

            TradeHistoryViewModel model = new TradeHistoryViewModel
            {
                Show = Show,
                SummVolume = SummVolume,
                CountOfElements = countOfElements
            };

            return model;
        }
    }
}
