using Business;
using Business.Contexts;
using System;
using System.Linq;

namespace CRM.Services.IndicatorPoints
{
    public class IndicatorPointsService
    {
        public IndicatorPointsModel Load(ChartsFilter filter)
        {
            var model = new IndicatorPointsModel();

            using(MySQLContext context = new MySQLContext())
            {
                var query = context.IndicatorPoints
                    .Where(x => x.Time > filter.StartDate && x.Time < filter.EndDate);

                if (filter.Coin != null)
                    query = query.Where(x => x.Base == filter.Coin);

                if (filter.Exchange != null)
                    query = query.Where(x => x.Exchange == filter.Exchange);

                model.Dates = query.Select(x => x.Time).ToList();

                model.MACDValues = query.Select(x => Convert.ToDouble(x.MACD)).ToList();
                model.SIGValues = query.Select(x => Convert.ToDouble(x.SIG)).ToList();


            };

            return model;
        }
    }
}
