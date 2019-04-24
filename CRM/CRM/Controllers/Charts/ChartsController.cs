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
                EndDate = DatesHelper.CurrentDateStr,
                DatesAsks = new List<long>(),
                DatesBids = new List<long>(),
                AsksValues = new List<string>(),
                BidsValues = new List<string>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AsksOnBids(AskOnBidViewModel model)
        {
            AsksOnBidsService asksOnBids = new AsksOnBidsService();
            asksOnBids.Load(model.Coin, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate));

            model.DatesAsks = asksOnBids.DatesAsks.Select(x => x.ToJavascriptTicks()).ToList(); // TODO: ToJavascriptTicks, ToString должен вызываться здесь, а не в сервисе. Поправить здесь и ниже.
            model.DatesBids = asksOnBids.DatesBids.Select(x => x.ToJavascriptTicks()).ToList();

            model.AsksValues = asksOnBids.AsksValues.Select(x => x.ToString().Replace(',', '.')).ToList();
            model.BidsValues = asksOnBids.BidsValues.Select(x => x.ToString().Replace(',', '.')).ToList();

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
            DeltaOnTradeHistoryService deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            deltaOnTradeHistory.Load(model.Coin, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate));

            model.DatesDelta = deltaOnTradeHistory.DatesDelta.Select(x => x.ToJavascriptTicks()).ToList();
            model.DeltaValues = deltaOnTradeHistory.DeltaValues.Select(x => x.ToString().Replace(',', '.')).ToList();

            model.DatesTHBuy = deltaOnTradeHistory.DatesTHBuy.Select(x => x.ToJavascriptTicks()).ToList();
            model.THBuyValues = deltaOnTradeHistory.THBuyValues.Select(x => x.ToString().Replace(',', '.')).ToList();

            model.DatesTHSell = deltaOnTradeHistory.DatesTHSell.Select(x => x.ToJavascriptTicks()).ToList();
            model.THSellValues = deltaOnTradeHistory.THSellValues.Select(x => x.ToString().Replace(',', '.')).ToList();


            return View(model);
        }
    }
}