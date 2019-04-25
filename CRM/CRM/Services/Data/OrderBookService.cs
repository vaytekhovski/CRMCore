using CRM.Models;
using CRM.Models.Database;
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

        public void Load(string bookType, string coin, string situation, DateTime startDate, DateTime endDate)
        {
            if (startDate == null && endDate == null)
                return;

            using (CRMContext context = new CRMContext())
            {
                Show = context.OrderBookModels
                    .Where(x => x.BookType == bookType &&
                        (coin == "all" ? true : x.CurrencyName == coin) &&
                        (situation == "all" ? true : x.MarketSituation == situation) &&
                        x.Date >= startDate && x.Date <= endDate)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                SummVolume = Show.Sum(item => item.Volume);
            }
        }
    }
}
