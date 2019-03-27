using CRMCore.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CRMCore.Models.Database;
using System.Linq;
using System;
using CRMCore.Models;
using CRMCore.Services.Data;

namespace CRMCore.Controllers.Data
{
    public class DataController : Controller
    {
        public ActionResult Index()
        {
        //    Session["SD"] = "";
        //    Session["ED"] = "";
            return View();
        }


        public ActionResult ShowOrderBookAsks(string coin, string situation, string startDate = "", string endDate = "")
        {
            if (Models.User.isAutorized)
            {
                OrderBookAsks orderBookAsks = new OrderBookAsks(coin, situation, startDate, endDate);

                ViewBag.show = orderBookAsks.Show;
                ViewBag.summVolume = orderBookAsks.summVolume;

                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");
        }

       

        public ActionResult ShowOrderBookBids(string coin, string situation, string startDate = "", string endDate = "")
        {
            if (Models.User.isAutorized)
            {
                OrderBookBids orderBookBids = new OrderBookBids(coin, situation, startDate, endDate);
                
                ViewBag.show = orderBookBids.Show;
                ViewBag.summVolume = orderBookBids.summVolume;

                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");

        }

        public ActionResult ShowTradeHistory(string coin, string situation, string orderType, string startDate = "", string endDate = "")
        {
            if (Models.User.isAutorized)
            {
                TradeHistory tradeHistory = new TradeHistory(coin, situation, orderType, startDate, endDate);
                
                ViewBag.show = tradeHistory.Show;
                ViewBag.summVolume = tradeHistory.summVolume;

                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");

        }
        public ActionResult ShowTradeDelta(string coin, string startDate = "", string endDate = "", string nulldelta = "all")
        {
            if (Models.User.isAutorized)
            {
                TradeDelta tradeDelta = new TradeDelta(coin, startDate, endDate, nulldelta);

                ViewBag.show = tradeDelta.Show;
                ViewBag.summDelta = tradeDelta.summDelta;
                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");

        }
    }
}