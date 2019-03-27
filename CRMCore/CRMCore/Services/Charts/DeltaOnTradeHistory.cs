using CRMCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Services.Charts
{
    public class DeltaOnTradeHistory
    {
        public List<long> datesDelta = new List<long>();
        public List<string> deltaValues = new List<string>();

        public List<long> datesTHBuy = new List<long>();
        public List<string> THBuyValues = new List<string>();

        public List<long> datesTHSell = new List<long>();
        public List<string> THSellValues = new List<string>();

        public DeltaOnTradeHistory(string coin, string startDate, string endDate)
        {
            if (startDate != "" && endDate != "")
            {
                var SD = DateTime.Parse(startDate);
                var ED = DateTime.Parse(endDate);

                //Session["SD"] = HomeController.DatesToSession(SD);
                //Session["ED"] = HomeController.DatesToSession(ED);

                using (CRMCoreContext context = new CRMCoreContext())
                {
                    var Deltas = context.TradeDeltaModels
                        .Where(x => x.CurrencyName == coin)
                        .Where(x => x.TimeTo >= SD && x.TimeTo <= ED)
                        .Where(x => x.Delta > 0 || x.Delta < 0)
                        .OrderBy(x => x.TimeFrom);

                    var THBuy = context.TradeHistoryModels
                        .Where(x => x.CurrencyName == coin)
                        .Where(x => x.Date >= SD && x.Date <= ED)
                        .Where(x => x.Side == "Buy")
                        .OrderBy(x => x.Date);

                    var THSell = context.TradeHistoryModels
                        .Where(x => x.CurrencyName == coin)
                        .Where(x => x.Date >= SD && x.Date <= ED)
                        .Where(x => x.Side == "Sell")
                        .OrderBy(x => x.Date);

                    foreach (var item in Deltas)
                    {
                        DateTime DatePlusTime = item.TimeTo.DateTime;
                        string value = item.Delta.ToString();

                        datesDelta.Add(DatePlusTime.ToJavascriptTicks());
                        deltaValues.Add(value.Replace(',', '.'));
                    }

                    foreach (var item in THBuy)
                    {
                        DateTime DatePlusTime = item.Date.DateTime;
                        string value = item.Volume.ToString();

                        datesTHBuy.Add(DatePlusTime.ToJavascriptTicks());
                        THBuyValues.Add(value.Replace(',', '.'));
                    }

                    foreach (var item in THSell)
                    {
                        DateTime DatePlusTime = item.Date.DateTime;
                        string value = item.Volume.ToString();

                        datesTHSell.Add(DatePlusTime.ToJavascriptTicks());
                        THSellValues.Add(value.Replace(',', '.'));
                    }
                }
            }

            DropDownFields.Swap(coin);
        }
    }
}
