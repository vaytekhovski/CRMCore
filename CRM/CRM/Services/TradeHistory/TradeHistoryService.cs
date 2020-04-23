using System;
using System.Linq;
using Business;
using Business.Contexts;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class TradeHistoryService
    {
        private readonly DatavisioAPIService datavisioAPI;

        public TradeHistoryService()
        {
            datavisioAPI = new DatavisioAPIService();

        }

        public TradeHistoryModel LoadDataToChart(TradeHistoryFilter filter)
        {
            var model = new TradeHistoryModel();

            using(BasicContext db = new BasicContext())
            {
                var query = db.AccountTradeHistories
                    .Where(x => x.Time >= filter.StartDate && x.Time <= filter.EndDate)
                    .Where(x => x.Profit != 0)
                    .AsNoTracking();

                if (filter.Coin != null)
                    query = query.Where(x => x.Pair == filter.Coin);

                if (filter.Account != null)
                    query = query.Where(x => x.Account == filter.Account);

                UpdateSummOfLossAndProfitOrders(model, query);
                query = query.OrderByDescending(x => x.Time);

                model.AccountTradeHistories = query.ToList();
            }

            return model;
        }

        public TradeHistoryModel Load(TradeHistoryFilter filter, HttpContext httpContext)
        {
            var model = new TradeHistoryModel();

            using (BasicContext context = new BasicContext())
            {
                var query = context.AccountTradeHistories
                    .Where(x => x.Time >= filter.StartDate && x.Time <= filter.EndDate)
                    .AsNoTracking();

                if (filter.Coin != null)
                    query = query.Where(x => x.Pair == filter.Coin);

                if (filter.Account != null)
                    query = query.Where(x => x.Account == filter.Account);

                UpdateTotalProfit(model, query);
                UpdateCountOfLossAndProfitOrders(model, query);
                UpdateSummOfLossAndProfitOrders(model, query);
                UpdateTotalEnterTax(model, query);


                query = query.OrderByDescending(x => x.Time);

                model.CountOfElements = query.Count();

                model.AccountTradeHistories = query.ToList();
            }

            Signals Signals = datavisioAPI.GetSignals(httpContext, "BTC").Result;
            Signal signalBTC = Signals.signals.FirstOrDefault();
            model.ProbaBuyBTC = signalBTC.value == 1 ? signalBTC.proba : 1m - signalBTC.proba;
            model.ProbaBuyBTC *= 100m;
            
            Signals = datavisioAPI.GetSignals(httpContext,"ETH").Result;
            Signal signalETH = Signals.signals.FirstOrDefault();
            model.ProbaBuyETH = signalETH.value == 1 ? signalETH.proba : 1m - signalETH.proba;
            model.ProbaBuyETH *= 100m;
            
            Signals = datavisioAPI.GetSignals(httpContext,"LTC").Result;
            Signal signalLTC = Signals.signals.FirstOrDefault();
            model.ProbaBuyLTC = signalLTC.value == 1 ? signalLTC.proba : 1m - signalLTC.proba;
            model.ProbaBuyLTC *= 100m;

            Signals = datavisioAPI.GetSignals(httpContext,"XRP").Result;
            Signal signalXRP = Signals.signals.FirstOrDefault();
            model.ProbaBuyXRP = signalXRP.value == 1 ? signalXRP.proba : 1m - signalXRP.proba;
            model.ProbaBuyXRP *= 100m;



            return model;
        }

        private void UpdateTotalEnterTax(TradeHistoryModel model, IQueryable<AccountTradeHistory> query)
        {
            model.TotalEnterTax = query.Where(x => x.Fee != 0).Sum(x => x.Fee);
        }

        private void UpdateTotalProfit(TradeHistoryModel model, IQueryable<AccountTradeHistory> query)
        {
            model.TotalProfit = query.Where(x => x.Profit != 0).Sum(x => x.Profit);
            model.TotalProfitWithoutFee = query.Where(x => x.ProfitWithoutFee != 0).Sum(x => x.ProfitWithoutFee);
        }

        private void UpdateCountOfLossAndProfitOrders(TradeHistoryModel model, IQueryable<AccountTradeHistory> query)
        {
            model.LossOrdersCount = query.Where(x => x.Profit < 0).Count();
            model.ProfitOrdersCount = query.Where(x => x.Profit > 0).Count();

            model.LossOrdersCountWithoutFee = query.Where(x => x.ProfitWithoutFee < 0).Count();
            model.ProfitOrdersCountWithoutFee = query.Where(x => x.ProfitWithoutFee > 0).Count();
        }

        private void UpdateSummOfLossAndProfitOrders(TradeHistoryModel model, IQueryable<AccountTradeHistory> query)
        {
            model.LossOrdersSumm = query.Where(x => x.Profit < 0).Sum(x => x.Profit);
            model.ProfitOrdersSumm = query.Where(x => x.Profit > 0).Sum(x => x.Profit);

            model.LossOrdersSummWithoutFee = query.Where(x => x.ProfitWithoutFee < 0).Sum(x => x.ProfitWithoutFee);
            model.ProfitOrdersSummWithoutFee = query.Where(x => x.ProfitWithoutFee > 0).Sum(x => x.ProfitWithoutFee);
        }

    }
}
