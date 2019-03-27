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
                AsksOnBids asksOnBids = new AsksOnBids(coin, startDate, endDate);

                ViewBag.datesAsks = asksOnBids.datesAsks;
                ViewBag.datesBids = asksOnBids.datesBids;

                ViewBag.asksValues = asksOnBids.asksValues;
                ViewBag.bidsValues = asksOnBids.bidsValues;
                
                return View();
            }
            return View("~/Views/Authorization/Login.cshtml");
        }

        

        public ActionResult DeltaOnTradeHistory(string coin, string startDate, string endDate)
        {
            if (Models.User.isAutorized)
            {
                DeltaOnTradeHistory deltaOnTradeHistory = new DeltaOnTradeHistory(coin, startDate, endDate);
                
                ViewBag.datesDelta = deltaOnTradeHistory.datesDelta;
                ViewBag.deltaValues = deltaOnTradeHistory.deltaValues;

                ViewBag.datesTHBuy = deltaOnTradeHistory.datesTHBuy;
                ViewBag.THBuyValues = deltaOnTradeHistory.THBuyValues;

                ViewBag.datesTHSell = deltaOnTradeHistory.datesTHSell;
                ViewBag.THSellValues = deltaOnTradeHistory.THSellValues;


                return View();
            }
            return View("~/Views/Authorization/Login.cshtml");

        }
    }
}