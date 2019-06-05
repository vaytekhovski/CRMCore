using CRM.Models;
using CRM.Models.Database;
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

        public OrderBookViewModel Load(string bookType, string coin, string situation, DateTime startDate, DateTime endDate)
        {
            if (startDate == null && endDate == null)
                return null;

            using (CRMContext context = new CRMContext())
            {
                Show = context.OrderBookModels
                    .Where(x => x.BookType == bookType)
                    .Where(x => coin == "all" ? true : x.CurrencyName == coin)
                    .Where(x => situation == "all" ? true : x.MarketSituation == situation)
                    .Where(x => x.Date >= startDate && x.Date <= endDate)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                SummVolume = Show.Sum(item => item.Volume);
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
