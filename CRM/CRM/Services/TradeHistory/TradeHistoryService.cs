using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthApp.Controllers;
using Business;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public TradeHistoryModel LoadDataToChart(TradeHistoryFilter filter, HttpContext httpContext)
        {

            var token = httpContext.User.Identity.Name;
            //model.Deals = datavisioAPI.GetListDeals(token).Result;


            return null;
        }

        public async Task<TradeHistoryModel> LoadAsync(TradeHistoryFilter filter, HttpContext httpContext)
        {
            var model = new TradeHistoryModel();

            var token = httpContext.User.Identity.Name;
            var accountId = httpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            model.Deals = await datavisioAPI.GetListDeals(accountId, token);

            model.Deals.deals = model.Deals.deals.Where(x => x.@base == "BTC" || x.@base == "ETH").ToArray();

            if (filter.Coin != null)
                model.Deals.deals = model.Deals.deals.Where(x => x.@base == filter.Coin).ToArray();

            model.Deals.deals = model.Deals.deals.Where(x => x.opened >= filter.StartDate).Where(x => x.opened <= filter.EndDate).ToArray();


            var IgnoreIds = DropDownFields.GetIgnoreIds().ToList();

            foreach (var item in IgnoreIds)
            {
                var dealToRemove = model.Deals.deals.FirstOrDefault(x => x.id == item.Value);
                if (dealToRemove != null)
                {
                    var DealList = model.Deals.deals.ToList();
                    DealList.Remove(dealToRemove);
                    model.Deals.deals = DealList.ToArray();
                }
            }

            // Увеличение
            var UserName = httpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            if (UserName == "guest")
            {
                foreach (var deals in model.Deals.deals)
                {
                    deals.income *= 10;
                    deals.outcome *= 10;
                    deals.profit.clean.amount *= 10;
                    deals.profit.dirty.amount *= 10;
                    deals.fee *= 10;
                }
            }

            if (UserName != "guest")
                IgnoreIds = IgnoreIds.Where(x => Convert.ToInt32(x.Text) < 20).ToList();

            var ClosedDeals = model.Deals.deals.Where(x => x.outcome != 0).ToList();

            model.DepositProfit = 0;
            decimal Deposit = 0;

            if (UserName == "guest")
                Deposit = 10000;
            else
                Deposit = 1000;

            var cdToDeposit = ClosedDeals.Where(x => x.closed.Value >= new DateTime(2020, 09, 01)).ToList();
            Deposit += cdToDeposit.Sum(x => x.profit.clean.amount);
            var cdToProfit = ClosedDeals.Where(x => x.opened >= filter.StartDate).Where(x => x.closed <= filter.EndDate).ToList();
            var _profit = cdToProfit.Sum(x => x.profit.clean.amount);
            model.DepositProfit += (_profit / Deposit) * 100;


            

            UpdateTotalProfit(model);
            UpdateCountOfLossAndProfitOrders(model);
            UpdateSummOfLossAndProfitOrders(model);



            List<Deal> Deals = new List<Deal>();
            foreach (var deal in model.Deals.deals)
            {
                Deals.Add(await datavisioAPI.GetDeal(accountId, token, deal.id));
            }

            model.Deals.deals = Deals.OrderByDescending(x => x.opened).ToArray();



            model.CountOfElements = model.Deals.deals.Count();



            return model;
        }


        private void UpdateTotalProfit(TradeHistoryModel model)
        {
            model.TotalProfit = model.Deals.deals.Where(x => x.outcome != 0).Sum(x => x.profit.clean.amount);
            model.TotalProfitWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Sum(x => x.profit.dirty.amount);
        }

        private void UpdateCountOfLossAndProfitOrders(TradeHistoryModel model)
        {
            model.LossOrdersCount = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.clean.amount <= 0).Count();
            model.ProfitOrdersCount = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.clean.amount > 0).Count();

            model.LossOrdersCountWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.dirty.amount <= 0).Count();
            model.ProfitOrdersCountWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.dirty.amount > 0).Count();
        }

        private void UpdateSummOfLossAndProfitOrders(TradeHistoryModel model)
        {
            model.LossOrdersSumm = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.clean.amount <= 0).Sum(x => x.profit.clean.amount);
            model.ProfitOrdersSumm = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.clean.amount > 0).Sum(x => x.profit.clean.amount);

            model.LossOrdersSummWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.dirty.amount <= 0).Sum(x => x.profit.dirty.amount);
            model.ProfitOrdersSummWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.dirty.amount > 0).Sum(x => x.profit.dirty.amount);
        }

    }
}
