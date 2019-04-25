using CRM.Models;
using CRM.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Data
{
    public class TradeDeltaService
    {
        public double SummDelta { get; private set; }
        public List<TradeDeltaModel> Show { get; private set; }

        public TradeDeltaService() { }

        public void Load(string coin, DateTime startDate, DateTime endDate, string nulldelta = "all")
        {
            if (startDate == null && endDate == null)
                return;

            using (CRMContext context = new CRMContext())
            {
                Show = context.TradeDeltaModels
                    .Where(x => coin == "all" ? true : x.CurrencyName == coin &&
                    x.TimeFrom >= startDate && x.TimeTo <= endDate &&
                    nulldelta == "all" ? true : x.Delta != 0)
                    .OrderByDescending(x => x.TimeFrom)
                    .ToList();


                SummDelta = Show.Sum(item => item.Delta);
                
            }
        }
    }
}
