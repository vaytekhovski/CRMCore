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


                query = query.OrderByDescending(x => x.Time);

                model.CountOfElements = query.Count();

                model.AccountTradeHistories = query.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();
                
            }

            using (MySQLContext sQLContext = new MySQLContext())
            {
                var lst = sQLContext.SignalsPrivate.ToList();


                var signals = sQLContext.SignalsPrivate
                    .Where(x => x.SourceTime >= filter.StartDate && x.SourceTime <= filter.EndDate)
                    .OrderBy(x => x.SourceTime)
                    .ToList();

                foreach (var item in signals)
                {
                    item.SourceTime = item.SourceTime.AddSeconds(-item.SourceTime.Second).AddMilliseconds(-item.SourceTime.Millisecond);
                }

                foreach (var item in model.AccountTradeHistories)
                {
                    item.Time = item.Time.AddSeconds(-item.Time.Second).AddMilliseconds(-item.Time.Millisecond);
                    var signal = signals.FirstOrDefault(x => x.SourceTime == item.Time);
                    // 30 12 20:47
                    if (signal != null)
                    {
                        var percent = signal.TotalToDecide * 100;
                        percent = (int)percent;
                        item.DecidePercent = percent.ToString();
                    }
                }

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
