using CRM.Models;
using CRM.Models.Database;
using CRM.Models.Filters;
using CRM.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Data
{
    public class OrderBookService
    {
        public double SummVolume { get; private set; }
        public List<OrderBookModel> Show { get; private set; }

        public OrderBookService() { }

        public OrderBookViewModel Load(DataFilter filter)
        {
            using (CRMContext context = new CRMContext())
            {
                Show = context.OrderBookModels
                    .Where(x => x.BookType == filter.BookType)
                    .Where(x => x.CurrencyName == null ? true : x.CurrencyName == filter.Coin)
                    .Where(x => x.MarketSituation == null ? true : x.MarketSituation == filter.Situation)
                    .Where(x => x.Date >= filter.StartDate && x.Date <= filter.EndDate)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                SummVolume = Show.Sum(item => item.Volume);

                Show = Show.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();
            }

            OrderBookViewModel model = new OrderBookViewModel
            {
                Show = Show,
                SummVolume = SummVolume
            };

            return model;
        }
    }
}
