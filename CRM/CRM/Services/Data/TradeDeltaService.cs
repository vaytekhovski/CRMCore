using CRM.Models;
using CRM.Models.Database;
using CRM.ViewModels.Data;
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

        public TradeDeltaViewModel Load(string coin, DateTime startDate, DateTime endDate, string nulldelta = "all")
        {
            if (startDate == null && endDate == null)
                return null;

            using (CRMContext context = new CRMContext())
            {
                Show = context.TradeDeltaModels
                    .Where(x => coin == null ? true : x.CurrencyName == coin)
                    .Where(x => x.TimeFrom >= startDate && x.TimeTo <= endDate)
                    .Where(x => nulldelta == null ? true : x.Delta != 0)
                    .OrderByDescending(x => x.TimeFrom)
                    .ToList();


                SummDelta = Show.Sum(item => item.Delta);
            }

            TradeDeltaViewModel model = new TradeDeltaViewModel
            {
                Show = Show,
                SummDelta = SummDelta
            };

            return model;
        }
    }
}
