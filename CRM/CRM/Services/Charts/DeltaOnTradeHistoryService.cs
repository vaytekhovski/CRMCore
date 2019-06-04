using CRM.Models;
using CRM.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Charts
{
    public class DeltaOnTradeHistoryService
    {
        public List<DateTime> DatesDelta { get; private set; } = new List<DateTime>(); 
        public List<double> DeltaValues { get; private set; } = new List<double>();

        public List<DateTime> DatesTHBuy { get; private set; } = new List<DateTime>();
        public List<double> THBuyValues { get; private set; } = new List<double>();

        public List<DateTime> DatesTHSell { get; private set; } = new List<DateTime>();
        public List<double> THSellValues { get; private set; } = new List<double>();



        private List<TradeDeltaModel> Deltas = new List<TradeDeltaModel>();
        private List<TradeHistoryModel> TH = new List<TradeHistoryModel>();

        public DeltaOnTradeHistoryService() { }

        public void Load(string coin, DateTime startDate, DateTime endDate)
        {
            if (startDate == null && endDate == null)
                return;

            using (CRMContext context = new CRMContext())
            {
                Deltas = context.TradeDeltaModels
                    .Where(x => x.CurrencyName == coin)
                    .Where(x => x.TimeTo >= startDate && x.TimeTo <= endDate)
                    .Where(x => x.Delta > 0 || x.Delta < 0)
                    .OrderBy(x => x.TimeFrom).ToList();

                TH = context.TradeHistoryModels
                    .Where(x => x.CurrencyName == coin)
                    .Where(x => x.Date >= startDate && x.Date <= endDate)
                    .OrderBy(x => x.Date).ToList();
            }

            DatesDelta = Deltas.Select(x => x.TimeTo.Date).ToList();
            DeltaValues = Deltas.Select(x => x.Delta).ToList();

            DatesTHBuy = TH.Where(x => x.Side == "Buy").Select(x => x.Date.Date).ToList();
            THBuyValues = TH.Where(x => x.Side == "Buy").Select(x => x.Volume).ToList();

            DatesTHSell = TH.Where(x => x.Side == "Sell").Select(x => x.Date.Date).ToList();
            THSellValues = TH.Where(x => x.Side == "Sell").Select(x => x.Volume).ToList();
        }
    }
}
