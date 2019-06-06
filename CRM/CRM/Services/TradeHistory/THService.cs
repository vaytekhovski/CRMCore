using System;
using System.Linq;
using CRM.Models.Binance;
using CRM.Models.Filters;
using CRM.Models.TradeHistory;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class THService
    {        
        public THService()
        {

        }

        public Models.TradeHistory.TradeHistoryModel Load(TradeHistoryFilter filter)
        {
            var model = new TradeHistoryModel();

            using (CRMContext context = new CRMContext())
            {
                var query = context.AccountTradeHistories
                    .Where(x => x.Time >= filter.StartDate && x.Time <= filter.EndDate)
                    .AsNoTracking();

                if (filter.Coin != "all")
                    query = query.Where(x => x.Pair == filter.Coin);

                if (filter.Account != "Все аккаунты")
                    query = query.Where(x => x.Account == filter.Account);


                UpdateTotalProfit(model, query);
                UpdateCountOfLossAndProfitOrders(model, query);
                UpdateSummOfLossAndProfitOrders(model, query);

                query = query.OrderByDescending(x => x.Time);

                model.AccountTradeHistories = query.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();
            }

            return model;
        }

        private void UpdateTotalProfit(TradeHistoryModel model, IQueryable<AccountTradeHistory> query)
        {
            model.TotalProfit = query.Where(x => x.Profit != 0).Sum(x => x.Profit);
            model.DesiredTotalProfit = query.Where(x => x.DesiredProfit != 0).Sum(x => x.DesiredProfit);
        }

        private void UpdateCountOfLossAndProfitOrders(TradeHistoryModel model, IQueryable<AccountTradeHistory> query)
        {
            model.LossOrdersCount = query.Where(x => x.Profit < 0).Count();
            model.ProfitOrdersCount = query.Where(x => x.Profit > 0).Count();

            model.DesiredLossOrdersCount = query.Where(x => x.DesiredProfit < 0).Count();
            model.DesiredProfitOrdersCount = query.Where(x => x.DesiredProfit > 0).Count();
        }

        private void UpdateSummOfLossAndProfitOrders(TradeHistoryModel model, IQueryable<AccountTradeHistory> query)
        {
            model.LossOrdersSumm = query.Where(x => x.Profit < 0).Sum(x => x.Profit);
            model.ProfitOrdersSumm = query.Where(x => x.Profit > 0).Sum(x => x.Profit);

            model.DesiredLossOrdersSumm = query.Where(x => x.DesiredProfit < 0).Sum(x => x.DesiredProfit);
            model.DesiredProfitOrdersSumm = query.Where(x => x.DesiredProfit > 0).Sum(x => x.DesiredProfit);
        }

    }
}
