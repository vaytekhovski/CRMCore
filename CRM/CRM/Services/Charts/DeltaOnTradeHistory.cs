using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Charts
{
    public class DeltaOnTradeHistoryService
    {
        private List<long> datesDelta = new List<long>();
        private List<string> deltaValues = new List<string>();

        private List<long> datesTHBuy = new List<long>();
        private List<string> tHBuyValues = new List<string>();

        private List<long> datesTHSell = new List<long>();
        private List<string> tHSellValues = new List<string>();

        public List<long> DatesDelta { get => datesDelta; }
        public List<string> DeltaValues { get => deltaValues; }

        public List<long> DatesTHBuy { get => datesTHBuy; }
        public List<string> THBuyValues { get => tHBuyValues; }

        public List<long> DatesTHSell { get => datesTHSell; }
        public List<string> THSellValues { get => tHSellValues; }

        public DeltaOnTradeHistoryService() { }

        public void Load(string coin, string startDate, string endDate)
        {
            if (startDate != "" && endDate != "")
            {
                var SD = DateTime.Parse(startDate);
                var ED = DateTime.Parse(endDate);

                //Session["SD"] = HomeController.DatesToSession(SD);
                //Session["ED"] = HomeController.DatesToSession(ED);

                using (CRMContext context = new CRMContext())
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

            if (coin != DropDownFields.Coins.ToArray()[0].Value)
                DropDownFields.SwapCoins(coin);
            
        }
    }
}
