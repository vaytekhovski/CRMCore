using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business;
using Business.Models.DataVisioAPI;
using CRM.Helpers;
using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly TradeHistoryService tradeHistoryService;

        public DashboardController(TradeHistoryService tradeHistoryService)
        {
            this.tradeHistoryService = tradeHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TradeHistoryFilterModel
            {
                Coin = null,
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr
            };

            return View(await LoadTradeHistory(viewModel));
        }

        [HttpPost]
        public async Task<IActionResult> Index(TradeHistoryFilterModel viewModel)
        {
            return View(await LoadTradeHistory(viewModel));
        }


        public async Task<TradeHistoryFilterModel> LoadTradeHistory(TradeHistoryFilterModel viewModel)
        {
            var filter = new TradeHistoryFilter
            {
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate)
            };

            TradeHistoryModel Model = await tradeHistoryService.LoadAsync(filter, HttpContext);
            viewModel = Converter(Model, viewModel);

            var UserName = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            ViewBag.Coins = DropDownFields.GetCoins().Where(x => HttpContext.User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == "Boss" ? x.Value == "BTC" || x.Value == "ETH" : true);
            ViewBag.FilterStartDate = UserName == "guest" ? "2020-09-01T00:00" : "2019-01-01T00:00";
            ViewBag.FilterEndDate = UserName == "guest" ? "2020-09-01T23:59" : "2019-01-01T23:59";

            return viewModel;
        }
        public TradeHistoryFilterModel Converter(TradeHistoryModel Model, TradeHistoryFilterModel viewModel)
        {
            var ClosedDeals = Model.Deals.deals.Where(x => x.outcome != 0).ToList();
            foreach (var item in Model.Deals.deals)
            {
                item.coin = item.@base;
                item.opened = item.opened.AddHours(3);
                item.closed = item.closed != null ? item.closed.Value.AddHours(3) : new DateTime(1999, 01, 01);
                item.fee = Math.Abs(item.profit.dirty.amount - item.profit.clean.amount);
            }

            viewModel.TotalProfit = Math.Truncate(Model.TotalProfit).ToString();
            if (Model.TotalProfit > 0)
                viewModel.TotalProfit = "+" + viewModel.TotalProfit;
            viewModel.LossOrdersSumm = Math.Abs(Math.Truncate(Model.LossOrdersSumm)).ToString();
            viewModel.ProfitOrdersSumm = Math.Truncate(Model.ProfitOrdersSumm).ToString();
            viewModel.DepositProfit = Model.DepositProfit.ToString("#.##");
            if (Model.DepositProfit > 0)
                viewModel.DepositProfit = "+" + viewModel.DepositProfit;

            /*
             * value = 3.16
             * valueWhole = truncate(value) == 3
             * valueDecimal = valueWhole - value == 0.16
             * valueDecimal = valueDecimal.Substring(2) == 16
             */

            viewModel.TotalProfitAfterDecimal = (Model.TotalProfit - Math.Truncate(Model.TotalProfit)).ToString().Substring(3, 2);
            viewModel.LossOrdersSummAfterDecimal = (Model.LossOrdersSumm - Math.Truncate(Model.LossOrdersSumm)).ToString().Substring(3, 2);
            viewModel.ProfitOrdersSummAfterDecimal = (Model.ProfitOrdersSumm - Math.Truncate(Model.ProfitOrdersSumm)).ToString().Substring(3, 2);

            viewModel.Deals = new ListDeals();
            viewModel.Deals.page = Model.Deals.page;
            viewModel.Deals.deals = new Deal[] { };

            viewModel.Deals.deals = ClosedDeals.OrderByDescending(x => x.opened).ToArray();
            return viewModel;
        }
    }
}