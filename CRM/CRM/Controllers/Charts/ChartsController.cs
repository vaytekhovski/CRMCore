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
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;

namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly TradeHistoryService _tradeHistoryService;
        private readonly DatavisioAPIService datavisioAPIService;

        public ChartsController()
        {
            _tradeHistoryService = new TradeHistoryService();
            datavisioAPIService = new DatavisioAPIService();
        }

        [HttpGet]
        public ActionResult Charts()
        {
            var model = new ProfitViewModel
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
        public ActionResult ProfitChart(ProfitViewModel ViewModel)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                Coin = ViewModel.Coin,
                Account = ViewModel.Account,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddDays(1),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _tradeHistoryService.LoadDataToChart(filter, HttpContext);

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

        [HttpGet]
        public IActionResult Signals()
        {
            SignalsModel model = new SignalsModel() {
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr
            };
            

            var token = HttpContext.User.Identity.Name;

            var RaiseSignals = datavisioAPIService.GetSignals(token, "BTC", "raise").Result;

            var FallSignals = datavisioAPIService.GetSignals(token, "BTC", "fall").Result;

            var FirstDate = RaiseSignals.signals.Last().time > FallSignals.signals.Last().time ? RaiseSignals.signals.Last().time : FallSignals.signals.Last().time;

            model.StartDate = FirstDate.AddHours(3).ToString("yyyy-MM-ddTHH:mm");

            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
            };

            RaiseSignals.signals = RaiseSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            FallSignals.signals = FallSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            model.RaiseDates = RaiseSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            model.FallDates = FallSignals.signals.Select(x => x.time.AddHours(3)).ToList();

            foreach (var signal in RaiseSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
            }

            foreach (var signal in FallSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
            }


            model.RaiseValues = RaiseSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.FallValues = FallSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.PageName = "Signals";
            return View(model);
        }

        [HttpPost]
        public IActionResult Signals(SignalsModel model)
        {

            var token = HttpContext.User.Identity.Name;
            var RaiseSignals = datavisioAPIService.GetSignals(token, "BTC", "raise").Result;
            var FallSignals = datavisioAPIService.GetSignals(token, "BTC", "fall").Result;

            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
            }; 
            
            RaiseSignals.signals = RaiseSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            FallSignals.signals = FallSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();



            model.RaiseDates = RaiseSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            model.FallDates = FallSignals.signals.Select(x => x.time.AddHours(3)).ToList();

            foreach (var signal in RaiseSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3);
            }

            foreach (var signal in FallSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3);

            }


            model.RaiseValues = RaiseSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.FallValues = FallSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.PageName = "Signals";
            return View(model);
        }


    }
}