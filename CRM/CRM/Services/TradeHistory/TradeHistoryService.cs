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
                model.Deals.deals = model.Deals.deals
                    .Where(x => x.@base == filter.Coin)
                    .Where(x => x.quote == filter.Quote)
                    .ToArray();


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





            List<Deal> Deals = new List<Deal>();
            foreach (var deal in model.Deals.deals)
            {
                Deals.Add(await datavisioAPI.GetDeal(accountId, token, deal.id));
            }

            model.Deals.deals = Deals.OrderByDescending(x => x.opened).ToArray();


            UpdateDealsValues(model);


            model.CountOfElements = model.Deals.deals.Count();

            ManualProfitCalculating(model);
            UpdateTotalProfit(model);
            UpdateCountOfLossAndProfitOrders(model);
            UpdateSummOfLossAndProfitOrders(model);



            return model;
        }

        private void ManualProfitCalculating(TradeHistoryModel model)
        {
            foreach (var deal in model.Deals.deals)
            {
                if(deal.profit.clean.percent == -100 && deal.closed != null)
                {
                    deal.profit.clean.amount = Convert.ToDecimal(deal.outcome - deal.income);
                    deal.profit.clean.percent = Convert.ToDecimal((deal.outcome / deal.income - 1) * 100);
                }
            }
        }

        private void UpdateDealsValues(TradeHistoryModel model)
        {
            if (model.Deals.deals.FirstOrDefault(x => x.id == "361727ee-38f8-4aaf-b636-38a6442a274a") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "361727ee-38f8-4aaf-b636-38a6442a274a").closed = new DateTime(2021, 04, 11, 21, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "361727ee-38f8-4aaf-b636-38a6442a274a").outcome = 1098.25m;
                model.Deals.deals.FirstOrDefault(x => x.id == "361727ee-38f8-4aaf-b636-38a6442a274a").orders.FirstOrDefault().price = 59575.99m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "084a22c6-743c-4f05-bd0f-e96c759998b0") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "084a22c6-743c-4f05-bd0f-e96c759998b0").closed = new DateTime(2021, 04, 11, 21, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "084a22c6-743c-4f05-bd0f-e96c759998b0").outcome = 3293.70m;
                model.Deals.deals.FirstOrDefault(x => x.id == "084a22c6-743c-4f05-bd0f-e96c759998b0").orders.FirstOrDefault().price = 59565.40m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "10ab18ac-e562-4eac-96fe-41228857c0ca") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "10ab18ac-e562-4eac-96fe-41228857c0ca").closed = new DateTime(2021, 04, 12, 14, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "10ab18ac-e562-4eac-96fe-41228857c0ca").outcome = 1098.02m;
                model.Deals.deals.FirstOrDefault(x => x.id == "10ab18ac-e562-4eac-96fe-41228857c0ca").orders.FirstOrDefault().price = 2135.10m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "f7962d2f-c6c1-4017-89cb-cd4f053211a0") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "f7962d2f-c6c1-4017-89cb-cd4f053211a0").closed = new DateTime(2021, 04, 13, 23, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "f7962d2f-c6c1-4017-89cb-cd4f053211a0").outcome = 1045.03m;
                model.Deals.deals.FirstOrDefault(x => x.id == "f7962d2f-c6c1-4017-89cb-cd4f053211a0").orders.FirstOrDefault().price = 63320.19m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "ce635dad-fc70-4ad2-a6c1-f834df2ec753") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "ce635dad-fc70-4ad2-a6c1-f834df2ec753").closed = new DateTime(2021, 04, 13, 23, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "ce635dad-fc70-4ad2-a6c1-f834df2ec753").outcome = 1042.32m;
                model.Deals.deals.FirstOrDefault(x => x.id == "ce635dad-fc70-4ad2-a6c1-f834df2ec753").orders.FirstOrDefault().price = 63300.27m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "d53acb99-2e72-41a9-a555-5cfb16355d66") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "d53acb99-2e72-41a9-a555-5cfb16355d66").closed = new DateTime(2021, 04, 14, 8, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "d53acb99-2e72-41a9-a555-5cfb16355d66").outcome = 1095.37m;
                model.Deals.deals.FirstOrDefault(x => x.id == "d53acb99-2e72-41a9-a555-5cfb16355d66").orders.FirstOrDefault().price = 2372.18m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "a8cf9725-ab82-486a-9240-45c427beb891") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "a8cf9725-ab82-486a-9240-45c427beb891").closed = new DateTime(2021, 04, 14, 8, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "a8cf9725-ab82-486a-9240-45c427beb891").outcome = 1098.64m;
                model.Deals.deals.FirstOrDefault(x => x.id == "a8cf9725-ab82-486a-9240-45c427beb891").orders.FirstOrDefault().price = 2374.65m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "a4821bf3-553d-4e83-842a-cdd83561a8b8") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "a4821bf3-553d-4e83-842a-cdd83561a8b8").closed = new DateTime(2021, 04, 15, 18, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "a4821bf3-553d-4e83-842a-cdd83561a8b8").outcome = 993.21m;
                model.Deals.deals.FirstOrDefault(x => x.id == "a4821bf3-553d-4e83-842a-cdd83561a8b8").orders.FirstOrDefault().price = 2455.91m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "6104b1b7-10e6-4477-97d3-5d7e94e396d9") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "6104b1b7-10e6-4477-97d3-5d7e94e396d9").closed = new DateTime(2021, 04, 15, 18, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "6104b1b7-10e6-4477-97d3-5d7e94e396d9").outcome = 991.69m;
                model.Deals.deals.FirstOrDefault(x => x.id == "6104b1b7-10e6-4477-97d3-5d7e94e396d9").orders.FirstOrDefault().price = 2452.05m;
            }
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
