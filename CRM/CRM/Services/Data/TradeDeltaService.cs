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

        public void Load(string coin, string startDate, string endDate, string nulldelta = "all")
        {
            if (startDate == null && endDate == null)
                return;

            DateTime SD = DateTime.Parse(startDate);
            DateTime ED = DateTime.Parse(endDate);

            using (CRMContext context = new CRMContext())
            {
                Show = context.TradeDeltaModels
                    .Where(z => coin == "all" ? true : z.CurrencyName == coin)
                    .OrderByDescending(x => x.TimeFrom)
                    .ToList();


                Show = Show.Where(x => x.TimeFrom >= SD && x.TimeTo <= ED).ToList();
                Show = Show.Where(x => nulldelta == "all" ? true : x.Delta != 0).ToList();
                //TODO: do like in OrderBookService

                foreach (var item in Show)
                {
                    SummDelta += item.Delta;
                }
                
                
            }
        }
    }
}
