using CRM.Models;
using CRM.Models.Database;
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

        public void Load(string coin, string situation, string orderType, string startDate, string endDate)
        {

            if (startDate == null && endDate == null)
                return;

            DateTime SD = DateTime.Parse(startDate);
            DateTime ED = DateTime.Parse(endDate);

            using (CRMContext context = new CRMContext())
            {
                Show = context.TradeHistoryModels
                    .Where(z => coin == "all" ? true : z.CurrencyName == coin)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                Show = Show.Where(x => x.Date >= SD && x.Date <= ED).ToList();
                Show = Show.Where(x => situation == "all" ? true : x.MarketSituation == situation).ToList();
                Show = Show.Where(x => orderType == "all" ? true : x.Side == orderType).ToList();
                //TODO: do like in OrderBookService
                foreach (var item in Show)
                {
                    SummVolume += item.Volume;
                }

            }
            
        }
    }
}
