using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CRM.Models;
using CRM.Services;
using CRM.Services.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers.Charts
{
    public class ChartsController : Controller
    {
        [Authorize]
        public ActionResult Charts()
        {
            return View();
        }

        [Authorize]
        public ActionResult AsksOnBids(string coin, string startDate = "", string endDate = "")
        {
            AsksOnBidsService asksOnBids = new AsksOnBidsService();
            asksOnBids.Load(coin, startDate, endDate);

            ViewBag.datesAsks = asksOnBids.DatesAsks;
            ViewBag.datesBids = asksOnBids.DatesBids;

            ViewBag.asksValues = asksOnBids.AsksValues;
            ViewBag.bidsValues = asksOnBids.BidsValues;

            return View();
        }

        [Authorize]
        public ActionResult DeltaOnTradeHistory(string coin, string startDate, string endDate)
        {
            DeltaOnTradeHistoryService deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            deltaOnTradeHistory.Load(coin, startDate, endDate);
            ViewBag.datesDelta = deltaOnTradeHistory.DatesDelta; // TODO: [COMPLETE] свойства с большой буквы, поля с маленькой, поля не должны быть публичными
            ViewBag.deltaValues = deltaOnTradeHistory.DeltaValues;

            ViewBag.datesTHBuy = deltaOnTradeHistory.DatesTHBuy;
            ViewBag.THBuyValues = deltaOnTradeHistory.THBuyValues;

            ViewBag.datesTHSell = deltaOnTradeHistory.DatesTHSell;
            ViewBag.THSellValues = deltaOnTradeHistory.THSellValues;


            return View();
        }
    }
}