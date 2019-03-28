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
                OrderBookAsksService orderBookAsks = new OrderBookAsksService();
                orderBookAsks.Load(coin, situation, startDate, endDate);

                ViewBag.show = orderBookAsks.Show; // TODO: использовать модели, без необходимости ViewBag и ViewData не использовать
                ViewBag.summVolume = orderBookAsks.SummVolume;

                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");
        }

       

        public ActionResult ShowOrderBookBids(string coin, string situation, string startDate = "", string endDate = "")
        {
            if (Models.User.isAutorized)
            {
                OrderBookBidsService orderBookBids = new OrderBookBidsService();
                orderBookBids.Load(coin, situation, startDate, endDate);
                
                ViewBag.show = orderBookBids.Show;
                ViewBag.summVolume = orderBookBids.SummVolume;

                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");

        }

        public ActionResult ShowTradeHistory(string coin, string situation, string orderType, string startDate = "", string endDate = "")
        {
            if (Models.User.isAutorized)
            {
                TradeHistoryService tradeHistory = new TradeHistoryService();
                tradeHistory.Load(coin, situation, orderType, startDate, endDate);
                
                ViewBag.show = tradeHistory.Show;
                ViewBag.summVolume = tradeHistory.SummVolume;

                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");

        }
        public ActionResult ShowTradeDelta(string coin, string startDate = "", string endDate = "", string nulldelta = "all")
        {
            if (Models.User.isAutorized)
            {
                TradeDeltaService tradeDelta = new TradeDeltaService();
                tradeDelta.Load(coin, startDate, endDate, nulldelta);

                ViewBag.show = tradeDelta.Show;
                ViewBag.summDelta = tradeDelta.SummDelta;
                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");

        }
    }
}