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



        private List<TradeDeltaModel> _deltas = new List<TradeDeltaModel>();
        private List<TradeHistoryModel> _tradeHistory = new List<TradeHistoryModel>();

        public DeltaOnTradeHistoryService() { }

        public DeltaOnTradeHistoryModel Load(ChartsFilter filter)
        {
            using (CRMContext context = new CRMContext())
            {
                _deltas = context.TradeDeltaModels
                    .Where(x => x.CurrencyName == filter.Coin)
                    .Where(x => x.TimeTo >= filter.StartDate && x.TimeTo <= filter.EndDate)
                    .Where(x => x.Delta > 0 || x.Delta < 0)
                    .OrderBy(x => x.TimeFrom).ToList();

                _tradeHistory = context.TradeHistoryModels
                    .Where(x => x.CurrencyName == filter.Coin)
                    .Where(x => x.Date >= filter.StartDate && x.Date <= filter.EndDate)
                    .OrderBy(x => x.Date).ToList();
            }

            DeltaOnTradeHistoryModel deltaOnTradeHistoryModel = new DeltaOnTradeHistoryModel
            {
                DatesDelta = _deltas.Select(x => x.TimeTo.DateTime).ToList(),
                DeltaValues = _deltas.Select(x => x.Delta).ToList(),

                DatesTHBuy = _tradeHistory.Where(x => x.Side == "Buy").Select(x => x.Date.DateTime).ToList(),
                THBuyValues = _tradeHistory.Where(x => x.Side == "Buy").Select(x => x.Volume).ToList(),

                DatesTHSell = _tradeHistory.Where(x => x.Side == "Sell").Select(x => x.Date.DateTime).ToList(),
                THSellValues = _tradeHistory.Where(x => x.Side == "Sell").Select(x => x.Volume).ToList()
            };

            return deltaOnTradeHistoryModel;
        }
    }
}
