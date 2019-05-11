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
                EndDate = DatesHelper.CurrentDateStr
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AsksOnBids(AskOnBidViewModel model)
        {
            SeparateHelper.Separator.NumberDecimalSeparator = ".";
            AsksOnBidsService asksOnBids = new AsksOnBidsService();
            if (DateTime.TryParse(model.StartDate, out var startDate) && DateTime.TryParse(model.EndDate, out var endDate))
            {
                asksOnBids.Load(model.Coin, startDate, endDate);

                model.DatesAsks = asksOnBids.DatesAsks.Select(x => x.ToJavascriptTicks()).ToList();
                model.DatesBids = asksOnBids.DatesBids.Select(x => x.ToJavascriptTicks()).ToList();
                // TODO: [COMPLETE] instead of replace use this: https://stackoverflow.com/a/6587281/571203
                model.AsksValues = asksOnBids.AsksValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
                model.BidsValues = asksOnBids.BidsValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

                return View(model);
            }

            ModelState.AddModelError("Date", "Dates invalid"); //TODO: [COMPLETE] применить везде такой паттерн работы с датами + перенести инициализацию списков в конструкторы
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
            DeltaOnTradeHistoryService deltaOnTradeHistory = new DeltaOnTradeHistoryService();
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