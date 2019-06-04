using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.Services.Charts;
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

        public ChartsController()
        {
            asksOnBids = new AsksOnBidsService();
            deltaOnTradeHistory = new DeltaOnTradeHistoryService();
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
        public ActionResult AsksOnBids(AskOnBidViewModel model)
        {
            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            asksOnBids.Load(model.Coin, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate));

            model.DatesAsks = asksOnBids.DatesAsks.Select(x => x.ToJavascriptTicks()).ToList();
            model.DatesBids = asksOnBids.DatesBids.Select(x => x.ToJavascriptTicks()).ToList();
            model.AsksValues = asksOnBids.AsksValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            model.BidsValues = asksOnBids.BidsValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

            return View(model);
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
        public ActionResult DeltaOnTradeHistory(DeltaOnTradeHistoryViewModel model)
        {
            SeparateHelper.Separator.NumberDecimalSeparator = ".";
            deltaOnTradeHistory.Load(model.Coin, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate));

            model.DatesDelta = deltaOnTradeHistory.DatesDelta.Select(x => x.ToJavascriptTicks()).ToList();
            model.DeltaValues = deltaOnTradeHistory.DeltaValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

            model.DatesTHBuy = deltaOnTradeHistory.DatesTHBuy.Select(x => x.ToJavascriptTicks()).ToList();
            model.THBuyValues = deltaOnTradeHistory.THBuyValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

            model.DatesTHSell = deltaOnTradeHistory.DatesTHSell.Select(x => x.ToJavascriptTicks()).ToList();
            model.THSellValues = deltaOnTradeHistory.THSellValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

            return View(model);
        }
    }
}