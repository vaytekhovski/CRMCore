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

        public void Load(string coin, string startDate = "", string endDate = "", string nulldelta = "all")
        {
            using (CRMContext context = new CRMContext())
            {
                Show = context.TradeDeltaModels.ToList();

                Show = context.TradeDeltaModels.Where(z => z.CurrencyName == coin).OrderByDescending(x => x.TimeFrom).ToList();

                if (startDate != "" && endDate != "")
                {
                    DateTime SD = DateTime.Parse(startDate);
                    DateTime ED = DateTime.Parse(endDate);
                    
                    Show = Show.Where(x => x.TimeFrom >= SD && x.TimeTo <= ED).ToList();
                }

                if (nulldelta == "notnull")
                {
                    Show = Show.Where(x => x.Delta > 0 || x.Delta < 0).ToList();
                }

                foreach (var item in Show)
                {
                    SummDelta += item.Delta;
                }
                
                
            }
        }
    }
}
