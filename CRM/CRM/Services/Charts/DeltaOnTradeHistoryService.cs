using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Charts
{
    public class DeltaOnTradeHistoryService
    {
        public List<long> DatesDelta { get; private set; } = new List<long>(); 
        public List<string> DeltaValues { get; private set; } = new List<string>();

        public List<long> DatesTHBuy { get; private set; } = new List<long>();
        public List<string> THBuyValues { get; private set; } = new List<string>();

        public List<long> DatesTHSell { get; private set; } = new List<long>();
        public List<string> THSellValues { get; private set; } = new List<string>();

        public DeltaOnTradeHistoryService() { }

        public void Load(string coin, string startDate, string endDate)
        {
            if (startDate != "" && endDate != "")
            {
                var SD = DateTime.Parse(startDate);
                var ED = DateTime.Parse(endDate);

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

                        DatesDelta.Add(DatePlusTime.ToJavascriptTicks());
                        DeltaValues.Add(value.Replace(',', '.'));
                    }

                    foreach (var item in THBuy)
                    {
                        DateTime DatePlusTime = item.Date.DateTime;
                        string value = item.Volume.ToString();

                        DatesTHBuy.Add(DatePlusTime.ToJavascriptTicks());
                        THBuyValues.Add(value.Replace(',', '.'));
                    }

                    foreach (var item in THSell)
                    {
                        DateTime DatePlusTime = item.Date.DateTime;
                        string value = item.Volume.ToString();

                        DatesTHSell.Add(DatePlusTime.ToJavascriptTicks());
                        THSellValues.Add(value.Replace(',', '.'));
                    }
                }
            }
            
        }
    }
}
