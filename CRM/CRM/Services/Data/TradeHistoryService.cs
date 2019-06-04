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

        public void Load(string coin, string situation, string orderType, DateTime startDate, DateTime endDate)
        {
            if (startDate == null && endDate == null)
                return;

            using (CRMContext context = new CRMContext())
            {
                Show = context.TradeHistoryModels
                    .Where(x => x.Date >= startDate && x.Date <= endDate)
                    .Where(x => coin == "all" ? true : x.CurrencyName == coin)
                    .Where(x => situation == "all" ? true : x.MarketSituation == situation)
                    .Where(x => orderType == "all" ? true : x.Side == orderType)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                SummVolume = Show.Sum(item => item.Volume);
            }
            
        }
    }
}
