using Business;
using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.ViewModels.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly TradeHistoryService _tradeHistoryService;

        public ChartsController()
        {
            _tradeHistoryService = new TradeHistoryService();
        }

        [HttpGet]
        public ActionResult Charts()
        {
            var model = new AskOnBidViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(model);
        }

        
        [HttpGet]
        public ActionResult ProfitChart()
        {
            var viewModel = new ProfitViewModel
            {
                PageName = "Profit",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ProfitChart(ProfitViewModel ViewModel, HttpContext httpContext)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                Coin = ViewModel.Coin,
                Account = ViewModel.Account,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddDays(1),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _tradeHistoryService.LoadDataToChart(filter, httpContext);

            ViewModel.Dates = model.Deals.deals.Select(x => x.closed.ToJavascriptTicks()).ToList();
            ViewModel.Values = model.Deals.deals.Select(x => x.profit.clean.amount.ToString(SeparateHelper.Separator)).ToList();

            ViewModel.CountOfZero = model.Deals.deals.Where(x => x.profit.clean.amount == 0).Count();
            ViewModel.CountOfMore = model.Deals.deals.Where(x => x.profit.clean.amount > 0).Count();
            ViewModel.CountOfLess = model.Deals.deals.Where(x => x.profit.clean.amount < 0).Count();

            ViewModel.VolumeOfMore = model.ProfitOrdersSumm.ToString(SeparateHelper.Separator);
            ViewModel.VolumeOfLess = (model.LossOrdersSumm * -1).ToString(SeparateHelper.Separator);

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Profit";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult OrdersOnTimeHistory()
        {
            var viewModel = new OrdersOnTimeHistoryViewModel
            {
                PageName = "OrdersOnTimeHistory",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult OrdersOnTimeHistory(OrdersOnTimeHistoryViewModel ViewModel)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                Coin = ViewModel.Coin,
                Account = ViewModel.Account,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddDays(1),
            };
            

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Orders On TimeHistory";


            return View(ViewModel);
        }


    }
}