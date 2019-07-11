using CRM.Helpers;
using CRM.Models;
using CRM.Models.Filters;
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

        public ChartsController()
        {
            _asksOnBids = new AsksOnBidsService();
            _deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            _indicatorPointsService = new IndicatorPointsService();
            _tradeHistoryOnTradeHistoryDeltaService = new TradeHistoryOnTradeHistoryDeltaService();
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
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult DeltaOnTradeHistory()
        {
            var model = new DeltaOnTradeHistoryViewModel
            {
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
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult IndicatorPoints()
        {
            var viewModel = new IndicatorPointsViewModel
            {
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
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult TradeHistoryOnTradeHistoryDelta()
        {
            var viewModel = new TradeHistoryOnTradeHistoryDeltaViewModel
            {
                CalculatingStartDate = DatesHelper.MinDateStr,
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TradeHistoryOnTradeHistoryDelta(TradeHistoryOnTradeHistoryDeltaViewModel ViewModel)
        {
            DateTime dateValue;
            if (DateTime.TryParse(ViewModel.CalculatingStartDate, out dateValue))
                dateValue = dateValue;
            else
                dateValue = DateTime.Parse(ViewModel.StartDate);

            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Base,
                CalculatingStartDate = dateValue,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _tradeHistoryOnTradeHistoryDeltaService.Load(filter);

            ViewModel.DatesTH = model.DatesTH.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.DatesTHD = model.DatesTHD.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.THValues = model.THValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.THDValues = model.THDValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(ViewModel);
        }
    }
}