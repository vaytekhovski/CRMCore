using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using CRM.Helpers;
using CRM.Services;
using CRM.Services.Balances;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly TradeHistoryService tradeHistoryService;
        private readonly DatavisioAPIService datavisioAPIService;
        private readonly BalancesService balancesService;



        public DashboardController(TradeHistoryService tradeHistoryService, DatavisioAPIService datavisioAPIService, BalancesService balancesService)
        {
            this.tradeHistoryService = tradeHistoryService;
            this.datavisioAPIService = datavisioAPIService;
            this.balancesService = balancesService;
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

        public async Task<IActionResult> TradeSettings(UserPanelModel model)
        {
            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            model.BalancesDebit = await balancesService.LoadBalancesAsync(accountId, token, "debit");
            model.BalancesMargin = await balancesService.LoadBalancesAsync(accountId, token, "margin");
            model.AccountData = await datavisioAPIService.ShowAccount(accountId, token);
            model.Accounts = await datavisioAPIService.ShowAccounts(token);
            foreach (var pair in model.AccountData.pairs)
            {
                pair.coin = pair.@base;
            }



            return View(model);
        }

        [HttpPost]
        public async Task EnableDisableTradePair(string pair)
        {
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            var token = HttpContext.User.Identity.Name;
            ShowAccount AccountData = await datavisioAPIService.ShowAccount(accountId, token);

            if(AccountData.pairs.First(x=>x.@base == pair).enabled == true)
            {
                await datavisioAPIService.DisablePair(accountId, token, pair);
            }
            else
            {
                await datavisioAPIService.EnablePair(accountId, token, pair);
            }

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

            string ChartData = "";
            string ChartDataLables = "";

            foreach (var item in viewModel.Deals.deals.OrderBy(x=>x.closed))
            {
                ChartData += "{\"meta\":\"" + Convert.ToDateTime(item.closed).ToString("g", CultureInfo.CreateSpecificCulture("en-US")) + "\",\"value\":\"" + item.profit.clean.amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "\"},\n";
                ChartDataLables += "\"" + Convert.ToDateTime(item.closed).ToString("M") + "\",";
            }
            ViewBag.ChartData = ChartData.Remove(ChartData.Length - 3,2) + "}";
            ViewBag.ChartDataLables = ChartDataLables.Remove(ChartDataLables.Length - 1, 1);
            ViewBag.ChartHigh = viewModel.Deals.deals.OrderByDescending(x => x.profit.clean.amount).Select(x => x.profit.clean.amount).First();
            ViewBag.ChartLow = viewModel.Deals.deals.OrderBy(x => x.profit.clean.amount).Select(x => x.profit.clean.amount).First();

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