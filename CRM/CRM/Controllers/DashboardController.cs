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
using CRM.ViewModels.Dashboard;
using CRM.ViewModels.ManualTrading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
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

        [HttpGet]
        public async Task<IActionResult> Statistic()
        {
            var viewModel = new DashboardStatisticViewModel
            {
                Coin = null,
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr
            };

            var filter = new TradeHistoryFilter
            {
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate)
            };

            if (filter.Coin != null)
            {
                var token = HttpContext.User.Identity.Name;

                viewModel.signals = datavisioAPIService
                    .GetSignals(token, filter.Coin, "grad_" + filter.Coin.ToLower())
                    .Result.signals
                    .Where(x => x.time > filter.StartDate && x.time < filter.EndDate)
                    .OrderBy(x => x.time)
                    .ToArray();
            }

            return CalculateStatistic(await tradeHistoryService.LoadAsync(filter, HttpContext), viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Statistic(DashboardStatisticViewModel viewModel)
        {
            var filter = new TradeHistoryFilter
            {
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate)
            };

            if (filter.Coin != null)
            {
                var token = HttpContext.User.Identity.Name;

                viewModel.signals = datavisioAPIService
                    .GetSignals(token, filter.Coin, "grad_" + filter.Coin.ToLower())
                    .Result.signals
                    .Where(x => x.time > filter.StartDate && x.time < filter.EndDate)
                    .OrderBy(x => x.time)
                    .ToArray();
            }

            return CalculateStatistic(await tradeHistoryService.LoadAsync(filter, HttpContext), viewModel);

        }

        public async Task<IActionResult> Deal(string DealId)
        {
            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            var response = await datavisioAPIService.GetDeal(accountId, token, DealId);
            response.coin = response.@base;

            GetDealModel Model = new GetDealModel()
            {
                Deal = response,
                balancesModel = await balancesService.LoadBalancesAsync(accountId, token)
            };


            var _candles = await datavisioAPIService.GetCandles(token, Model.Deal.coin);
            var candles = _candles.ToList();

            Model.LastPrice = candles.Last().c;

            var UserName = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            foreach (var item in Model.Deal.orders)
            {
                item.created = item.created.AddHours(3);
                item.closed = item.closed != null ? item.closed.Value.AddHours(3) : new DateTime(1999, 01, 01);

                if (UserName == "guest")
                {
                    // Увеличение
                    item.amount *= 10;
                    item.filled *= 10;
                }
            }
            Model.Deal.orders = Model.Deal.orders.OrderByDescending(x => x.created).ToArray();

            return View(Model);
        }


        public IActionResult CalculateStatistic(TradeHistoryModel Model, DashboardStatisticViewModel viewModel)
        {
            var ClosedDeals = Model.Deals.deals.Where(x => x.outcome != 0).ToList();

            viewModel.TotalProfit = Model.TotalProfit.ToString();

            viewModel.LossOrdersCount = Model.LossOrdersCount;
            viewModel.LossOrdersSumm = Model.LossOrdersSumm.ToString();

            viewModel.ProfitOrdersCount = Model.ProfitOrdersCount;
            viewModel.ProfitOrdersSumm = Model.ProfitOrdersSumm.ToString();
            viewModel.DepositProfit = Model.DepositProfit.ToString();

            if (ClosedDeals.Count() > 0 && (decimal)Model.LossOrdersCount != 0)
            {
                viewModel.RPL = (decimal)Model.ProfitOrdersCount / (decimal)Model.LossOrdersCount;
                viewModel.AP = Model.ProfitOrdersCount > 0 ? Model.ProfitOrdersSumm / Model.ProfitOrdersCount : 0;
                viewModel.AL = Model.LossOrdersSumm / Model.LossOrdersCount;
                viewModel.AR = Model.TotalProfit / (Model.ProfitOrdersCount + Model.LossOrdersCount);
                viewModel.RAPAL = viewModel.AP / Math.Abs(viewModel.AL);

                var TroughValue = ClosedDeals.Min(x => x.income);
                var PeakValue = ClosedDeals.Max(x => x.income);
                viewModel.MIDD = TroughValue - PeakValue / PeakValue;
                viewModel.Dmin = viewModel.MIDD + 100;

                viewModel.R = Model.TotalProfit / viewModel.Dmin;
                viewModel.RF = Model.TotalProfit / viewModel.MIDD;
                viewModel.PF = Model.ProfitOrdersSumm / Math.Abs(Model.LossOrdersSumm);
                viewModel.APF = (Model.ProfitOrdersSumm - ClosedDeals.Max(x => x.profit.clean.amount) / Math.Abs(Model.LossOrdersSumm));

                var MidPercentProfit = ClosedDeals.Sum(x => x.profit.clean.percent) / ClosedDeals.Count();

                double StandardDeviation = 0;
                decimal _CompoundInterest = 1;
                decimal _CompoundInterestWithoutFee = 1;

                foreach (var coin in ClosedDeals.Select(x => x.@base).Distinct())
                {
                    foreach (var percentProfit in ClosedDeals.Where(x => x.@base == coin).Where(x => x.profit.clean.percent != 0).OrderBy(x => x.opened).Select(x => x.profit.clean.percent).ToList())
                    {
                        StandardDeviation += Math.Pow((double)percentProfit - (double)MidPercentProfit, 2);
                        if (StandardDeviation == 0)
                            StandardDeviation = 1;
                        _CompoundInterest *= 1 + (percentProfit / 100);
                    }
                    //viewModel.CompoundInterest += (_CompoundInterest - 1) * 100;
                    _CompoundInterest = 1;

                    foreach (var percentProfitWithoutFee in ClosedDeals.Where(x => x.@base == coin).Where(x => x.profit.dirty.percent != 0).Where(x => x.@base == coin).OrderBy(x => x.opened).Select(x => x.profit.dirty.percent).ToList())
                    {
                        _CompoundInterestWithoutFee *= 1 + (percentProfitWithoutFee / 100);
                    }

                    //viewModel.CompoundInterestWithoutFee += (_CompoundInterestWithoutFee - 1) * 100; ;
                    _CompoundInterestWithoutFee = 1;
                }

                StandardDeviation /= ClosedDeals.Count();
                StandardDeviation = Math.Sqrt(StandardDeviation);

                viewModel.SharpeRatio = (MidPercentProfit - 0.05m) / (decimal)StandardDeviation;


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
            if (Model.TotalProfit > 0)
                viewModel.TotalProfitAfterDecimal = (Model.TotalProfit - Math.Truncate(Model.TotalProfit)).ToString().Substring(3, 2);

            if (Model.LossOrdersSumm > 0)
                viewModel.LossOrdersSummAfterDecimal = (Model.LossOrdersSumm - Math.Truncate(Model.LossOrdersSumm)).ToString().Substring(3, 2);

            if (Model.ProfitOrdersSumm > 0)
                viewModel.ProfitOrdersSummAfterDecimal = (Model.ProfitOrdersSumm - Math.Truncate(Model.ProfitOrdersSumm)).ToString().Substring(3, 2);

            var THviewModel = new TradeHistoryFilterModel();
            THviewModel = Converter(Model, THviewModel);
            string ChartData = "";
            string ChartDataLables = "";

            var DealsToChart = THviewModel.Deals.deals
                .Where(x => x.closed > new DateTime(2019, 01, 01))
                .OrderBy(x => x.closed)
                .ToList();

            foreach (var item in DealsToChart)
            {
                ChartData += "{\"meta\":\"" + Convert.ToDateTime(item.closed)
                    .ToString("g", CultureInfo.CreateSpecificCulture("en-US")) + "\",\"value\":\"" + item.profit.clean.amount
                    .ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "\"},\n";
                ChartDataLables += "\"" + Convert.ToDateTime(item.closed).ToString("M") + "\",";
            }
            if (ChartData.Length != 0) { 
                ViewBag.ChartData = ChartData.Remove(ChartData.Length - 3, 2) + "}";
                ViewBag.ChartDataLables = ChartDataLables.Remove(ChartDataLables.Length - 1, 1);
                ViewBag.ChartHigh = DealsToChart.OrderByDescending(x => x.profit.clean.amount).Select(x => x.profit.clean.amount).First();
                ViewBag.ChartLow = DealsToChart.OrderBy(x => x.profit.clean.amount).Select(x => x.profit.clean.amount).First();
            }
            var UserName = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            ViewBag.Coins = DropDownFields.GetCoins().Where(x => HttpContext.User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == "Boss" ? x.Value == "BTC" || x.Value == "ETH" : true);
            ViewBag.FilterStartDate = UserName == "guest" ? "2020-09-01T00:00" : "2019-01-01T00:00";
            ViewBag.FilterEndDate = UserName == "guest" ? "2020-09-01T23:59" : "2019-01-01T23:59";

            return View(viewModel);
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

            

            var UserName = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            ViewBag.Coins = DropDownFields.GetCoins().Where(x => HttpContext.User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == "Boss" ? x.Value == "BTC" || x.Value == "ETH" : true);
            ViewBag.FilterStartDate = UserName == "guest" ? "2020-09-01T00:00" : "2019-01-01T00:00";
            ViewBag.FilterEndDate = UserName == "guest" ? "2020-09-01T23:59" : "2019-01-01T23:59";

            return viewModel;
        }
        public TradeHistoryFilterModel Converter(TradeHistoryModel Model, TradeHistoryFilterModel viewModel)
        {
            var ClosedDeals = Model.Deals.deals.ToList();
            foreach (var item in Model.Deals.deals)
            {
                item.coin = item.@base;
                item.opened = item.opened.AddHours(3);
                item.closed = item.closed != null ? item.closed.Value.AddHours(3) : new DateTime(1999, 01, 01);
                item.fee = Math.Abs(item.profit.dirty.amount - item.profit.clean.amount);
            }

            viewModel.TotalProfit = Model.TotalProfit.ToString("#.##");
            viewModel.ProfitOrdersSumm = Model.ProfitOrdersSumm.ToString("#.##");
            viewModel.LossOrdersSumm = Model.LossOrdersSumm.ToString("#.##");
            

            viewModel.Deals = new ListDeals();
            viewModel.Deals.page = Model.Deals.page;
            viewModel.Deals.deals = new Deal[] { };

            viewModel.Deals.deals = ClosedDeals.OrderByDescending(x => x.opened).ToArray();
            return viewModel;
        }
    }
}