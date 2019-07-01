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
        private readonly AsksOnBidsService asksOnBids;

        private readonly DeltaOnTradeHistoryService deltaOnTradeHistory;

        private readonly IndicatorPointsService indicatorPointsService;

        public ChartsController()
        {
            asksOnBids = new AsksOnBidsService();
            deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            indicatorPointsService = new IndicatorPointsService();
        }

        [HttpGet]
        public ActionResult Charts()
        {
            var model = new AskOnBidViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
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
            return View(model);
        }

        [HttpPost]
        public ActionResult AsksOnBids(AskOnBidViewModel ViewModel)
        {
            var model = asksOnBids.Load(ViewModel.Coin, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate));

            ViewModel.DatesAsks = model.DatesAsks;
            ViewModel.AsksValues = model.AsksValues;
            ViewModel.DatesBids = model.DatesBids;
            ViewModel.BidsValues = model.BidsValues;

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
            return View(model);
        }

        [HttpPost]
        public ActionResult DeltaOnTradeHistory(DeltaOnTradeHistoryViewModel ViewModel)
        {
            var model = deltaOnTradeHistory.Load(ViewModel.Coin, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate));

            ViewModel.DatesDelta = model.DatesDelta;
            ViewModel.DeltaValues = model.DeltaValues;

            ViewModel.DatesTHBuy = model.DatesTHBuy;
            ViewModel.THBuyValues = model.THBuyValues;

            ViewModel.DatesTHSell = model.DatesTHSell;
            ViewModel.THSellValues = model.THSellValues;

            return View(model);
        }

        [HttpGet]
        public ActionResult IndicatorPoints()
        {
            var viewModel = new IndicatorPointsViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult IndicatorPoints(IndicatorPointsViewModel viewModel)
        {
            IndicatorPointsFilter filter = new IndicatorPointsFilter
            {
                Coin = viewModel.Base,
                Exchange = viewModel.Exchange,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate),
            };

            var model = indicatorPointsService.Load(filter);

            viewModel.Dates = model.Dates;
            viewModel.MACDValues = model.MACDValues;
            viewModel.SIGValues = model.SIGValues;

            return View(viewModel);
        }
    }
}