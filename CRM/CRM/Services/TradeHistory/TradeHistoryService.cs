using System;
using System.Linq;
using Business;
using Business.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class TradeHistoryService
    {        
        public TradeHistoryService()
        {

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

        public TradeHistoryModel Load(TradeHistoryFilter filter)
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

            using (MySQLContext sQLContext = new MySQLContext())
            {
                if (filter.Coin == null)
                {
                    model.ProbaBuyBTC = sQLContext.NeuralSignals.Where(x => x.Base == "BTC").OrderByDescending(x => x.Time).FirstOrDefault().ProbaBuy * 100M;
                    model.ProbaBuyLTC = sQLContext.NeuralSignals.Where(x => x.Base == "LTC").OrderByDescending(x => x.Time).FirstOrDefault().ProbaBuy * 100M;
                    model.ProbaBuyETH = sQLContext.NeuralSignals.Where(x => x.Base == "ETH").OrderByDescending(x => x.Time).FirstOrDefault().ProbaBuy * 100M;
                }
                else if(filter.Coin == "BTC")
                {
                    model.ProbaBuyBTC = sQLContext.NeuralSignals.Where(x => x.Base == "BTC").OrderByDescending(x => x.Time).FirstOrDefault().ProbaBuy * 100M;
                }
                else if(filter.Coin == "LTC")
                {
                    model.ProbaBuyLTC = sQLContext.NeuralSignals.Where(x => x.Base == "LTC").OrderByDescending(x => x.Time).FirstOrDefault().ProbaBuy * 100M;
                }
                else if(filter.Coin == "ETH")
                {
                    model.ProbaBuyETH = sQLContext.NeuralSignals.Where(x => x.Base == "ETH").OrderByDescending(x => x.Time).FirstOrDefault().ProbaBuy * 100M;
                }
            }



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
