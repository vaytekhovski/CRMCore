using CRM.Models;
using CRM.Models.Database;
using CRM.Models.Filters;
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

        public TradeDeltaViewModel Load(DataFilter filter)
        {
            int CountOfElements = 0;
            using (CRMContext context = new CRMContext())
            {

                Show = context.TradeDeltaModels
                    .Where(x => filter.Coin == null ? true : x.CurrencyName == filter.Coin)
                    .Where(x => x.TimeFrom >= filter.StartDate && x.TimeTo <= filter.EndDate)
                    .Where(x => filter.nulldelta == null ? true : x.Delta != 0)
                    .OrderByDescending(x => x.TimeFrom)
                    .ToList();

                CountOfElements = Show.Count();
                
                SummDelta = Show.Sum(item => item.Delta);
                Show = Show.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();
            }

            TradeDeltaViewModel model = new TradeDeltaViewModel
            {
                Show = Show,
                SummDelta = SummDelta,
                CountOfElements = CountOfElements
            };

            return model;
        }
    }
}
