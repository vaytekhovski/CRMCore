using CRM.Master;
using CRM.Models;
using CRM.Models.Filters;
using CRM.ViewModels.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.IndicatorPoints
{
    public class IndicatorPointsService
    {
        public IndicatorPointsViewModel Load(IndicatorPointsFilter filter)
        {
            var model = new IndicatorPointsViewModel();

            using(masterContext context = new masterContext())
            {
                var query = context.IndicatorPoints
                    .Where(x => x.Time > filter.StartDate && x.Time < filter.EndDate);

                if (filter.Coin != null)
                    query = query.Where(x => x.Base == filter.Coin);

                if (filter.Exchange != null)
                    query = query.Where(x => x.Exchange == filter.Exchange);

                model.Dates = query.Select(x => x.Time).Select(x => x.ToJavascriptTicks()).ToList();

                model.MACDValues = query.Select(x => x.MACD.ToString().Replace(',','.')).ToList();
                model.SIGValues = query.Select(x => x.SIG.ToString().Replace(',', '.')).ToList();


            };

            return model;
        }
    }
}
