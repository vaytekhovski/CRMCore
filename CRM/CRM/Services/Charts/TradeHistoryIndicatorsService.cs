using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.Contexts;
using Business.Models.Charts;
using CRM.Services.Data;

namespace CRM.Services.Charts
{
    public class TradeHistoryIndicatorsService
    {
        private readonly TradeHistoryService _tradeHistoryService;
        public TradeHistoryIndicatorsService()
        {
            _tradeHistoryService = new TradeHistoryService();

        }

        public TradeHistoryIndicatorsModel Load(ChartsFilter _filter)
        {
            var filter = new TradeHistoryFilter
            {
                Coin = _filter.Coin,
                StartDate = _filter.StartDate,
                EndDate = _filter.EndDate,
                CurrentPage = 1
            };

            List<DateTime> Dates = new List<DateTime>();

            TradeHistoryIndicatorsModel model = new TradeHistoryIndicatorsModel();
            TradeHistoryModel TH = new TradeHistoryModel();

            using (BasicContext context = new BasicContext())
            {
                TH = _tradeHistoryService.LoadDataToChart(filter);
            }

            TH.AccountTradeHistories = TH.AccountTradeHistories.OrderBy(x => x.Time).ToList();
            Dates = TH.AccountTradeHistories.Select(x => x.Time.Date).Distinct().ToList();

            

            foreach (var date in Dates)
            {
                var indicator = new Indicator();
                indicator.Date = date;
                indicator.TP = TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Where(x => x.Profit > 0).Sum(x => x.Profit);
                indicator.TL = TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Where(x => x.Profit < 0).Sum(x => x.Profit);
                indicator.TR = TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Sum(x => x.Profit);

                indicator.NP = TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Where(x => x.Profit > 0).Count();
                indicator.NL = TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Where(x => x.Profit < 0).Count();
                indicator.N = TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Count();

                indicator.RPL = indicator.NL != 0 ? indicator.NP / indicator.NL : 0;
                indicator.AP = indicator.NP != 0 ? indicator.TP / indicator.NP : 0;
                indicator.AL = indicator.NL != 0 ? indicator.TL / indicator.NL : 0;
                indicator.AR = indicator.N != 0 ? indicator.TR / indicator.N : 0;

                indicator.RAPAL = indicator.AL != 0 ? indicator.AP / Math.Abs(indicator.AL) : 0;

                var TroughValue = TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Min(x => x.DollarQuantity);
                var PeakValue = TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Max(x => x.DollarQuantity);
                indicator.MIDD = TroughValue - PeakValue / PeakValue;
                indicator.Dmin = indicator.MIDD + 100;
                indicator.R = indicator.Dmin != 0 ? indicator.TR / indicator.Dmin : 0;
                indicator.RF = indicator.MIDD != 0 ? indicator.TR / indicator.MIDD : 0;
                indicator.PF = indicator.TL != 0 ? indicator.TP / Math.Abs(indicator.TL) : 0;
                indicator.APF = indicator.TL != 0 ? (indicator.TP - TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Max(x => x.Profit) / Math.Abs(indicator.TL)) : 0;

                var MidPercentProfit = indicator.N != 0 ? TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Sum(x => x.PercentProfit) / indicator.N : 0;
                double StandardDeviation = 0;

                foreach (var item in TH.AccountTradeHistories.Where(x => x.Time.Date == date.Date).Select(x => x.PercentProfit).ToList())
                {
                    StandardDeviation += Math.Pow((double)item - (double)MidPercentProfit, 2);
                }

                StandardDeviation /= indicator.N != 0 ? (double)indicator.N : 1;
                StandardDeviation = Math.Sqrt(StandardDeviation);

                indicator.SharpeRatio = StandardDeviation != 0 ? (MidPercentProfit - 0.05m) / (decimal)StandardDeviation : 0;

                model.Indicators.Add(indicator);
            }


            return model;
        }
    }

    
}
