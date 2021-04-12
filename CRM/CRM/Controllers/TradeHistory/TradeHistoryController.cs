using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthApp.Controllers;
using Business;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.Services.Pagination;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class TradeHistoryController : Controller
    {
        private readonly TradeHistoryService _tradeHistoryService;
        private readonly PaginationService _paginationService;
        private readonly DatavisioAPIService datavisioAPIService;


        public TradeHistoryController()
        {
            _tradeHistoryService = new TradeHistoryService();
            _paginationService = new PaginationService();

            datavisioAPIService = new DatavisioAPIService();

        }


        public ActionResult ErrorOrders()
        {
            List<Order> orders = new List<Order>();

            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            var deals = datavisioAPIService.GetListDeals(accountId, token).Result.deals.ToList();

            foreach (var deal in deals)
            {
                List<Order> Buf = datavisioAPIService.GetDeal(accountId, token, deal.id).Result.orders.Where(x => x.status == "failed").ToList();
                foreach (var order in Buf)
                {
                    order.coin = deal.@base;
                }
                orders.AddRange(Buf);
            }

            return View(orders);
        }
        
        [HttpGet]
        public async Task<ActionResult> TradeHistory()
        {
            var viewModel = new TradeHistoryFilterModel
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

            TradeHistoryModel Model = await _tradeHistoryService.LoadAsync(filter, HttpContext);
            
            viewModel = MoveDataFromModelToViewModel(Model, viewModel);

            //viewModel.Deals.deals = viewModel.Deals.deals.Skip((filter.CurrentPage - 1) * 100).Take(100).ToArray();


            //var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, Model.CountOfElements);

            //viewModel.CurrentPage = filter.CurrentPage;
            //viewModel.CountOfPages = pagination.CountOfPages;


            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            //viewModel.AccountData = datavisioAPIService.ShowAccount(accountId, token).Result;
            //viewModel.Accounts = datavisioAPIService.ShowAccounts(token).Result;

            ViewBag.Coins = DropDownFields.GetCoins();
            var UserName = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            ViewBag.FilterStartDate = UserName == "guest" ? "2020-09-01T00:00" : "2019-01-01T00:00";
            ViewBag.FilterEndDate = UserName == "guest" ? "2020-09-01T23:59" : "2019-01-01T23:59";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> TradeHistory(TradeHistoryFilterModel viewModel, string PageButton = "1")
        {
            // TODO: [COMPLETE] использовать такой паттерн везде
            //var model = service.Load(parameter1, parameter2, ...); 
            //var viewModel = new ViewModel();
            //viewModel.Items = model.Items.Select(x => ...);


            var filter = new TradeHistoryFilter
            {
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };

            TradeHistoryModel Model = await _tradeHistoryService.LoadAsync(filter, HttpContext);
            

            var last = Model.Deals.deals.FirstOrDefault();

            viewModel = MoveDataFromModelToViewModel(Model, viewModel);

            viewModel.Deals.deals = viewModel.Deals.deals.Skip((filter.CurrentPage - 1) * 100).Take(100).ToArray();


            var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, Model.CountOfElements);



            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            //viewModel.AccountData = datavisioAPIService.ShowAccount(accountId, token).Result;
            //viewModel.Accounts = datavisioAPIService.ShowAccounts(token).Result;

            ViewBag.Coins = DropDownFields.GetCoins();
            var UserName = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            ViewBag.FilterStartDate = UserName == "guest" ? "2020-09-01T00:00" : "2019-01-01T00:00";
            ViewBag.FilterEndDate = UserName == "guest" ? "2020-09-01T23:59" : "2019-01-01T23:59";
            return View(viewModel);
        }

        private TradeHistoryFilterModel MoveDataFromModelToViewModel(TradeHistoryModel Model, TradeHistoryFilterModel viewModel)
        {
            //viewModel.Deals = Model.Deals;
            var ClosedDeals = Model.Deals.deals.Where(x => x.outcome != 0).ToList();
            foreach (var item in Model.Deals.deals)
            {
                item.coin = item.@base;
                item.opened = item.opened.AddHours(3);
                item.closed = item.closed != null ? item.closed.Value.AddHours(3) : new DateTime(1999, 01, 01);
                item.fee = Math.Abs(item.profit.dirty.amount - item.profit.clean.amount);
            }

            //viewModel.TotalProfit = Model.TotalProfit;
            //viewModel.TotalProfitWithoutFee = Model.TotalProfitWithoutFee;

            //viewModel.LossOrdersCount = Model.LossOrdersCount;
            //viewModel.LossOrdersSumm = Model.LossOrdersSumm;

            //viewModel.ProfitOrdersCount = Model.ProfitOrdersCount;
            //viewModel.ProfitOrdersSumm = Model.ProfitOrdersSumm;

            //viewModel.ProfitOrdersCountWithoutFee = Model.ProfitOrdersCountWithoutFee;
            //viewModel.LossOrdersCountWithoutFee = Model.LossOrdersCountWithoutFee;

            //viewModel.ProfitOrdersSummWithoutFee = Model.ProfitOrdersSummWithoutFee;
            //viewModel.LossOrdersSummWithoutFee = Model.LossOrdersSummWithoutFee;

            //viewModel.TotalEnterTax = Model.TotalEnterTax;

            //viewModel.DepositProfit = Model.DepositProfit;

            //if (ClosedDeals.Count() > 0 && (decimal)Model.LossOrdersCount != 0)
            //{
            //    viewModel.RPL = (decimal)Model.ProfitOrdersCount / (decimal)Model.LossOrdersCount;
            //    viewModel.AP = Model.ProfitOrdersCount > 0 ? Model.ProfitOrdersSumm / Model.ProfitOrdersCount : 0;
            //    viewModel.AL = Model.LossOrdersSumm / Model.LossOrdersCount;
            //    viewModel.AR = Model.TotalProfit / (Model.ProfitOrdersCount + Model.LossOrdersCount);
            //    viewModel.RAPAL = viewModel.AP / Math.Abs(viewModel.AL);

            //    var TroughValue = ClosedDeals.Min(x => x.income);
            //    var PeakValue = ClosedDeals.Max(x => x.income);
            //    viewModel.MIDD = TroughValue - PeakValue / PeakValue;
            //    viewModel.Dmin = viewModel.MIDD + 100;

            //    viewModel.R = Model.TotalProfit / viewModel.Dmin;
            //    viewModel.RF = Model.TotalProfit / viewModel.MIDD;
            //    viewModel.PF = Model.ProfitOrdersSumm / Math.Abs(Model.LossOrdersSumm);
            //    viewModel.APF = (Model.ProfitOrdersSumm - ClosedDeals.Max(x => x.profit.clean.amount) / Math.Abs(Model.LossOrdersSumm));
            //    viewModel.CompoundInterest = 0;
            //    viewModel.CompoundInterestWithoutFee = 0;

            //    var MidPercentProfit = ClosedDeals.Sum(x => x.profit.clean.percent) / ClosedDeals.Count();

            //    double StandardDeviation = 0;
            //    decimal _CompoundInterest = 1;
            //    decimal _CompoundInterestWithoutFee = 1;

            //    foreach (var coin in ClosedDeals.Select(x=>x.@base).Distinct())
            //    {
            //        foreach (var percentProfit in ClosedDeals.Where(x => x.@base == coin).Where(x => x.profit.clean.percent != 0).OrderBy(x => x.opened).Select(x => x.profit.clean.percent).ToList())
            //        {
            //            StandardDeviation += Math.Pow((double)percentProfit - (double)MidPercentProfit, 2);
            //            if (StandardDeviation == 0)
            //                StandardDeviation = 1;
            //            _CompoundInterest *= 1 + (percentProfit / 100);
            //        }
            //        viewModel.CompoundInterest += (_CompoundInterest - 1) * 100;
            //        _CompoundInterest = 1;

            //        foreach (var percentProfitWithoutFee in ClosedDeals.Where(x => x.@base == coin).Where(x => x.profit.dirty.percent != 0).Where(x => x.@base == coin).OrderBy(x => x.opened).Select(x => x.profit.dirty.percent).ToList())
            //        {
            //            _CompoundInterestWithoutFee *= 1 + (percentProfitWithoutFee / 100);
            //        }

            //        viewModel.CompoundInterestWithoutFee += (_CompoundInterestWithoutFee - 1) * 100; ;
            //        _CompoundInterestWithoutFee = 1;
            //    }

            //    StandardDeviation /= ClosedDeals.Count();
            //    StandardDeviation = Math.Sqrt(StandardDeviation);

            //    viewModel.SharpeRatio = (MidPercentProfit - 0.05m) / (decimal)StandardDeviation;
            //    try
            //    {
            //        viewModel.ProfitAverage = ClosedDeals.Where(x => x.profit.clean.amount > 0).Select(x => x.profit.clean.percent).Average();
            //        viewModel.LossAverage = ClosedDeals.Where(x => x.profit.clean.amount <= 0).Select(x => x.profit.clean.percent).Average();
            //    }
            //    catch
            //    {
            //        viewModel.ProfitAverage = 0;
            //        viewModel.LossAverage = 0;
            //    }

                
            //}

            //SeparateHelper.Separator.NumberDecimalSeparator = ".";

            //viewModel.ProbaBuyBTCstr = Math.Round(Convert.ToDouble(Model.ProbaBuyBTC), 2).ToString(SeparateHelper.Separator);
            //viewModel.ProbaBuyLTCstr = Math.Round(Convert.ToDouble(Model.ProbaBuyLTC), 2).ToString(SeparateHelper.Separator);
            //viewModel.ProbaBuyETHstr = Math.Round(Convert.ToDouble(Model.ProbaBuyETH), 2).ToString(SeparateHelper.Separator);
            //viewModel.ProbaBuyXRPstr = Math.Round(Convert.ToDouble(Model.ProbaBuyXRP), 2).ToString(SeparateHelper.Separator);


            //viewModel.ProbaBuyBTC = Model.ProbaBuyBTC;
            //viewModel.ProbaBuyLTC = Model.ProbaBuyLTC;
            //viewModel.ProbaBuyETH = Model.ProbaBuyETH;
            //viewModel.ProbaBuyXRP = Model.ProbaBuyXRP;

            //viewModel.CompoundInterest /= 2;
            //viewModel.CompoundInterestWithoutFee /= 2;

            var OpendedDeals = Model.Deals.deals.Where(x => x.outcome == 0).ToList();
            if (OpendedDeals != null)
            {
                ClosedDeals.AddRange(OpendedDeals);
            }

            viewModel.Deals = new Business.Models.DataVisioAPI.ListDeals();
            viewModel.Deals.page = Model.Deals.page;
            viewModel.Deals.deals = new Deal[] { };

            viewModel.Deals.deals = ClosedDeals.OrderByDescending(x=>x.opened).ToArray();


            return viewModel;
        }




    }
}