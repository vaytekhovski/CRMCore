using CRM.Helpers;
using CRM.Master;
using CRM.Models;
using CRM.Models.Charts;
using CRM.Models.Database;
using CRM.Models.Filters;
using CRM.ViewModels.Charts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Charts
{
    public class TradeHistoryOnTradeHistoryDeltaService
    {

        public TradeHistoryOnTradeHistoryDeltaModel Load(ChartsFilter filter)
        {
            TradeHistoryOnTradeHistoryDeltaModel model = new TradeHistoryOnTradeHistoryDeltaModel();
            List<Models.Master.TradeHistory> TH;
            List<Models.Master.TradeHistoryDelta> THD;

            using (masterContext db = new masterContext())
            {
                TH = db.TradeHistory.Where(x => x.Time > filter.CalculatingStartDate && x.Time < filter.EndDate).Where(x => x.Base == filter.Coin).ToList();
                THD = db.TradeHistoryDelta.Where(x => x.TimeTo > filter.StartDate && x.TimeTo < filter.EndDate).Where(x => x.Base == filter.Coin).ToList();
            }

            decimal SellVolumeAmounts = 0;
            decimal BuyVolumeAmounts = 0;

            //for (DateTime time = TH.First().Time; time <= TH.Last().Time.AddMinutes(-5); time = time.AddMinutes(5))
            //{
            //    BuyVolumeAmounts += TH.Where(x => x.Side != "sell").Where(x => x.Time >= time && x.Time < time.AddMinutes(5)).Sum(x => x.Amount);
            //    SellVolumeAmounts += TH.Where(x => x.Side == "sell").Where(x => x.Time >= time && x.Time < time.AddMinutes(5)).Sum(x => x.Amount);

            //    model.DatesTH.Add(time);
            //    model.THValues.Add(BuyVolumeAmounts / SellVolumeAmounts);
            //}

            DateTime lastTime = new DateTime(1999, 01, 01);
            decimal diff = 0;
            foreach (var item in TH)
            {
                if (item.Side == "buy")
                    BuyVolumeAmounts += item.Amount;
                else
                    SellVolumeAmounts += item.Amount;


                try
                {
                    diff = BuyVolumeAmounts / SellVolumeAmounts;
                }
                catch
                {
                    diff = 0;
                }

                if (item.Time > lastTime.AddMinutes(5) && item.Time >= filter.StartDate)
                {
                    lastTime = item.Time;
                    model.DatesTH.Add(lastTime);
                    model.THValues.Add(diff);
                }
                
            }




            model.DatesTHD.AddRange(THD.Select(x => x.TimeTo));
            model.THDValues.AddRange(THD.Select(x => x.TickerAsk));

            return model;
        }


    }
}