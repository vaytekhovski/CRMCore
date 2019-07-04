using CRM.Helpers;
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

        public DeltaOnTradeHistoryModel Load(ChartsFilter filter)
        {
            using (CRMContext context = new CRMContext())
            {
                Deltas = context.TradeDeltaModels
                    .Where(x => x.CurrencyName == filter.Coin)
                    .Where(x => x.TimeTo >= filter.StartDate && x.TimeTo <= filter.EndDate)
                    .Where(x => x.Delta > 0 || x.Delta < 0)
                    .OrderBy(x => x.TimeFrom).ToList();

                TH = context.TradeHistoryModels
                    .Where(x => x.CurrencyName == filter.Coin)
                    .Where(x => x.Date >= filter.StartDate && x.Date <= filter.EndDate)
                    .OrderBy(x => x.Date).ToList();
            }

            DeltaOnTradeHistoryModel deltaOnTradeHistoryModel = new DeltaOnTradeHistoryModel
            {
                DatesDelta = Deltas.Select(x => x.TimeTo.DateTime).ToList(),
                DeltaValues = Deltas.Select(x => x.Delta).ToList(),

                DatesTHBuy = TH.Where(x => x.Side == "Buy").Select(x => x.Date.DateTime).ToList(),
                THBuyValues = TH.Where(x => x.Side == "Buy").Select(x => x.Volume).ToList(),

                DatesTHSell = TH.Where(x => x.Side == "Sell").Select(x => x.Date.DateTime).ToList(),
                THSellValues = TH.Where(x => x.Side == "Sell").Select(x => x.Volume).ToList()
            };

            return deltaOnTradeHistoryModel;
        }
    }
}
