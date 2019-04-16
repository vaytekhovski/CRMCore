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

        public void Load(string coin, string situation, string orderType, string startDate = "", string endDate = "")
        {
            using (CRMContext context = new CRMContext())
            {
                Show = context.TradeHistoryModels.Where(z => z.CurrencyName == coin).OrderByDescending(x => x.Date).ToList();

                if (startDate != "" && endDate != "")
                {
                    DateTime SD = DateTime.Parse(startDate);
                    DateTime ED = DateTime.Parse(endDate);

                    Show = Show.Where(x => x.Date >= SD && x.Date <= ED).ToList();
                }

                if (situation != "all")
                {
                    Show = Show.Where(x => x.MarketSituation == situation).ToList();
                }

                if (orderType != "all")
                {
                    Show = Show.Where(x => x.Side == orderType).ToList();
                }

                foreach (var item in Show)
                {
                    SummVolume += item.Volume;
                }

            }
            
        }
    }
}
