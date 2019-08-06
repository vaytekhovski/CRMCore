using Business;
using Business.Contexts;
using Business.Data;
using CRM.ViewModels.Data;
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
            int countOfElements = 0;
            using (BasicContext context = new BasicContext())
            {
                Show = context.OrderBookModels
                    .Where(x => x.BookType == filter.BookType)
                    .Where(x => filter.Coin == null ? true : x.CurrencyName == filter.Coin)
                    .Where(x => filter.Situation == null ? true : x.MarketSituation == filter.Situation)
                    .Where(x => x.Date >= filter.StartDate && x.Date <= filter.EndDate)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                SummVolume = Show.Sum(item => item.Volume);
                countOfElements = Show.Count();
                Show = Show.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();
            }

            OrderBookViewModel model = new OrderBookViewModel
            {
                Show = Show,
                SummVolume = SummVolume,
                CountOfElements = countOfElements
            };

            return model;
        }
    }
}
