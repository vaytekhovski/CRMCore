using CRM.Models.Binance;
using CRM.Models.Filters;
using CRM.Models.Statistics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Statistics
{
    public class StatisticsService
    {
        public StatisticsService()
        {

        }

        public StatisticsModel Load(StatisticsFilter filter)
        {
            var model = new StatisticsModel();

            using (CRMContext context = new CRMContext())
            {
                var query = context.AccountTradeHistories
                    .Where(x => x.Time >= filter.StartDate && x.Time <= filter.EndDate)
                    .AsNoTracking();

                if (filter.Coin != "all")
                    query = query.Where(x => x.Pair == filter.Coin);

                if (filter.Account != "Все аккаунты")
                    query = query.Where(x => x.Account == filter.Account);



                var statistics = UpdateStatistics(query);
                statistics = UpdateTotalProfitInStatistics(statistics);

                statistics = statistics.OrderByDescending(x => x.Date).ToList();

                model.Statistics = statistics.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();
            }

            return model;
        }

        private List<StatisticsElement> UpdateStatistics(IQueryable<AccountTradeHistory> query)
        {
            var statistics = new List<StatisticsElement>();

            var lastDate = query.LastOrDefault().Time.Date;

            foreach (var date in query.Select(x => x.Time.Date).Distinct())
            {
                statistics.Add(new StatisticsElement
                {
                    Date = date,
                    ProfitOfDay = query.Where(x => x.Time.Date == date).Sum(x => x.DesiredProfit),
                    TotalProfit = 0
                });
            }


            return statistics;
        }

        private List<StatisticsElement> UpdateTotalProfitInStatistics(List<StatisticsElement> statistics)
        {
            decimal totalProfit = 0;

            foreach (var item in statistics)
            {
                totalProfit += item.ProfitOfDay;
                item.TotalProfit = totalProfit;
            }

            return statistics;
        }

    }
}
