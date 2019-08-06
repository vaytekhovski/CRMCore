using Business;
using Business.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Charts
{
    public class TradeHistoryOnTradeHistoryDeltaService
    {

        public TradeHistoryOnTradeHistoryDeltaModel Load(ChartsFilter filter)
        {
            TradeHistoryOnTradeHistoryDeltaModel model = new TradeHistoryOnTradeHistoryDeltaModel();
            List<TradeHistory> TH;
            List<TradeHistoryDelta> THD;

            using (MySQLContext db = new MySQLContext())
            {
                TH = db.TradeHistory.Where(x => x.Time > filter.CalculatingStartDate && x.Time < filter.EndDate).Where(x => x.Base == filter.Coin).ToList();
                THD = db.TradeHistoryDelta.Where(x => x.TimeTo > filter.StartDate && x.TimeTo < filter.EndDate).Where(x => x.Base == filter.Coin).ToList();
            }



            decimal SellVolume = 0; // SV - sell volume (первый промежуток времени)
            decimal BuyVolme = 0; // buy volume(прогрессивный объем)
            decimal point = 0; // (SV - BV) / SV


            SellVolume = TH.Where(x => x.Side == "sell").Where(x => x.Time <= filter.StartDate).Select(x => x.Amount).Sum();

            foreach (var item in TH.Where(x => x.Side == "buy").Where(x => x.Time > filter.StartDate))
            {
                BuyVolme += item.Amount;

                try
                {
                    point = (SellVolume - BuyVolme) / SellVolume;
                }
                catch
                {
                    point = 0;
                }

                model.THValues.Add(point);
                model.DatesTH.Add(item.Time);
            }


            model.DatesTHD.AddRange(THD.Select(x => x.TimeTo));
            model.THDValues.AddRange(THD.Select(x => x.TickerAsk));

            return model;
        }


    }
}