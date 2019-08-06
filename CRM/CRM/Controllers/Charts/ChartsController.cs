using Business;
using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.Services.Charts;
using CRM.Services.IndicatorPoints;
using CRM.ViewModels.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly AsksOnBidsService _asksOnBids;

        private readonly DeltaOnTradeHistoryService _deltaOnTradeHistory;

        private readonly IndicatorPointsService _indicatorPointsService;

        private readonly TradeHistoryOnTradeHistoryDeltaService _tradeHistoryOnTradeHistoryDeltaService;

        private readonly TradeHistoryService _tradeHistoryService;

        public ChartsController()
        {
            _asksOnBids = new AsksOnBidsService();
            _deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            _indicatorPointsService = new IndicatorPointsService();
            _tradeHistoryOnTradeHistoryDeltaService = new TradeHistoryOnTradeHistoryDeltaService();
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
        public ActionResult AsksOnBids()
        {
            var model = new AskOnBidViewModel
            {
                PageName = "Asks on Bids",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(model);
        }

        [HttpPost]
        public ActionResult AsksOnBids(AskOnBidViewModel ViewModel)
        {
            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
            };

            var model = _asksOnBids.Load(filter);

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            ViewModel.DatesAsks = model.DatesAsks.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.AsksValues = model.AsksValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.DatesBids = model.DatesBids.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.BidsValues = model.BidsValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Asks on Bids";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult DeltaOnTradeHistory()
        {
            var model = new DeltaOnTradeHistoryViewModel
            {
                PageName = "Delta on TradeHistory",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                DatesDelta = new List<long>(),
                DeltaValues = new List<string>(),
                DatesTHBuy = new List<long>(),
                THBuyValues = new List<string>(),
                DatesTHSell = new List<long>(),
                THSellValues = new List<string>()
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(model);
        }

        [HttpPost]
        public ActionResult DeltaOnTradeHistory(DeltaOnTradeHistoryViewModel ViewModel)
        {
            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
            };

            var model = _deltaOnTradeHistory.Load(filter);

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            ViewModel.DatesDelta = model.DatesDelta.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.DeltaValues = model.DeltaValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

            ViewModel.DatesTHBuy = model.DatesTHBuy.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.THBuyValues = model.THBuyValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

            ViewModel.DatesTHSell = model.DatesTHSell.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.THSellValues = model.THSellValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Delta on TradeHistory";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult IndicatorPoints()
        {
            var viewModel = new IndicatorPointsViewModel
            {
                PageName = "Indicator Points",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult IndicatorPoints(IndicatorPointsViewModel ViewModel)
        {
            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Base,
                Exchange = ViewModel.Exchange,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _indicatorPointsService.Load(filter);

            ViewModel.Dates = model.Dates.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.MACDValues = model.MACDValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.SIGValues = model.SIGValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Indicator Points";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult TradeHistoryOnTradeHistoryDelta()
        {
            var viewModel = new TradeHistoryOnTradeHistoryDeltaViewModel
            {
                PageName = "Volumes and Asks",
                FirstDate = DatesHelper.MinDateTimeStr,
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TradeHistoryOnTradeHistoryDelta(TradeHistoryOnTradeHistoryDeltaViewModel ViewModel)
        {
            
            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Base ?? "BTC",
                CalculatingStartDate = DateTime.Parse(ViewModel.FirstDate).AddHours(-3),
                StartDate = DateTime.Parse(ViewModel.StartDate).AddHours(-3),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddHours(-3),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _tradeHistoryOnTradeHistoryDeltaService.Load(filter);

            ViewModel.DatesTH = model.DatesTH.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.DatesTHD = model.DatesTHD.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.THValues = model.THValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.THDValues = model.THDValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Volumes and Asks";
            return View(ViewModel);
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
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
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

            var model = _tradeHistoryService.LoadDataToChart(filter);

            ViewModel.Dates = model.AccountTradeHistories.Select(x => x.Time.ToJavascriptTicks()).ToList();
            ViewModel.Values = model.AccountTradeHistories.Select(x => x.DesiredProfit.ToString(SeparateHelper.Separator)).ToList();

            ViewModel.CountOfZero = model.AccountTradeHistories.Where(x => x.DesiredProfit == 0).Count();
            ViewModel.CountOfMore = model.AccountTradeHistories.Where(x => x.DesiredProfit > 0).Count();
            ViewModel.CountOfLess = model.AccountTradeHistories.Where(x => x.DesiredProfit < 0).Count();

            ViewModel.VolumeOfMore = model.DesiredProfitOrdersSumm.ToString(SeparateHelper.Separator);
            ViewModel.VolumeOfLess = (model.DesiredLossOrdersSumm * -1).ToString(SeparateHelper.Separator);

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
            ViewModel.PageName = "Profit";
            return View(ViewModel);
        }
    }
}