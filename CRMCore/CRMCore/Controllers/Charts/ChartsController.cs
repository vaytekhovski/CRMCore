using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CRMCore.Models;
using CRMCore.Services;
using CRMCore.Services.Charts;
using Microsoft.AspNetCore.Mvc;


namespace CRMCore.Controllers.Charts
{
    public class ChartsController : Controller
    {
        public ActionResult Charts()
        {
        //    Session["SD"] = "";
        //    Session["ED"] = "";
            return View();
        }

        public ActionResult AsksOnBids(string coin, string startDate = "", string endDate = "")
        {
            if (Models.User.isAutorized)
            {
                AsksOnBidsService asksOnBids = new AsksOnBidsService();
                asksOnBids.Load(coin, startDate, endDate);

                ViewBag.datesAsks = asksOnBids.DatesAsks;
                ViewBag.datesBids = asksOnBids.DatesBids;

                ViewBag.asksValues = asksOnBids.AsksValues;
                ViewBag.bidsValues = asksOnBids.BidsValues;
                
                return View();
            }
            return View("~/Views/Authorization/Login.cshtml");
        }

        

        public ActionResult DeltaOnTradeHistory(string coin, string startDate, string endDate)
        {
            if (Models.User.isAutorized)
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
            return View("~/Views/Authorization/Login.cshtml");

        }
    }
}