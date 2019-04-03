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
    [Authorize]
    public class ChartsController : Controller
    {
        public ActionResult Charts()
        {
            return View();
        }
        
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
        
        public ActionResult DeltaOnTradeHistory(string coin, string startDate, string endDate)
        {
            DeltaOnTradeHistoryService deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            deltaOnTradeHistory.Load(coin, startDate, endDate);
            ViewBag.datesDelta = deltaOnTradeHistory.DatesDelta;
            ViewBag.deltaValues = deltaOnTradeHistory.DeltaValues;

            ViewBag.datesTHBuy = deltaOnTradeHistory.DatesTHBuy;
            ViewBag.THBuyValues = deltaOnTradeHistory.THBuyValues;

            ViewBag.datesTHSell = deltaOnTradeHistory.DatesTHSell;
            ViewBag.THSellValues = deltaOnTradeHistory.THSellValues;


            return View();
        }
    }
}