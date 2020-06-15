using AuthApp.Controllers;
using Business;
using Business.DataVisioAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Statistics
{
    public class StatisticsService
    {
        private readonly DatavisioAPIService datavisioAPI;

        public StatisticsService()
        {
            datavisioAPI = new DatavisioAPIService();

        }

        public StatisticsModel Load(StatisticsFilter filter, HttpContext httpContext)
        {
            var model = new StatisticsModel();

            var token = AccountController.GetAuthorizationKey(httpContext, datavisioAPI).Result;
            if (token == "")
            {
                return null;
            }
            Business.Models.DataVisioAPI.ListDeals deals = datavisioAPI.GetListDeals(token).Result;

            if (filter.Coin != null)
                deals.deals = deals.deals.Where(x => x.@base == filter.Coin).ToArray();

            var statistics = UpdateStatistics(deals);
            statistics = UpdateTotalProfitInStatistics(statistics);

            statistics = statistics.OrderByDescending(x => x.Date).ToList();

            model.Statistics = statistics.Skip((filter.CurrentPage - 1) * 100).Take(100).ToList();

            return model;
        }

        private List<StatisticsElement> UpdateStatistics(Business.Models.DataVisioAPI.ListDeals deals)
        {
            var statistics = new List<StatisticsElement>();
            deals.deals = deals.deals.Where(x => x.closed != null).ToArray();

            foreach (var date in deals.deals.Select(x => x.closed.Value.Date).Distinct())
            {
                statistics.Add(new StatisticsElement
                {
                    Date = date,
                    ProfitOfDay = deals.deals.Where(x => x.closed.Value.Date == date).Sum(x => x.profit.clean.amount),
                    TotalProfit = 0
                });
            }


            return statistics;
        }

        private List<StatisticsElement> UpdateTotalProfitInStatistics(List<StatisticsElement> statistics)
        {
            decimal totalProfit = 0;

            foreach (var item in statistics.OrderBy(x => x.Date))
            {
                totalProfit += item.ProfitOfDay;
                item.TotalProfit = totalProfit;
            }

            return statistics;
        }

    }
}
