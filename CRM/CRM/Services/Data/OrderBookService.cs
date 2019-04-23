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

        public void Load(string bookType, string coin, string situation, string startDate, string endDate)
        {
            if (startDate == null && endDate == null)
                return;

            DateTime SD = DateTime.Parse(startDate);
            DateTime ED = DateTime.Parse(endDate);

            using (CRMContext context = new CRMContext())
            {
                Show = context.OrderBookModels
                    .Where(z => z.BookType == bookType &&
                    coin == "all" ? true : z.CurrencyName == coin)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                Show = Show.Where(x => x.Date >= SD && x.Date <= ED).ToList();
                Show = Show.Where(x => situation == "all" ? true : x.MarketSituation == situation).ToList();

                foreach (var item in Show)
                {
                    SummVolume += item.Volume;
                }
            }

            
        }
    }
}
