using CRM.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CRM.Models.Database;
using System.Linq;
using System;
using CRM.Models;
using CRM.Services.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Controllers.Data
{
    public class DataController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ShowOrderBookAsks(string coin, string situation, string startDate = "", string endDate = "")
        {
            OrderBookAsksService orderBookAsks = new OrderBookAsksService();
            orderBookAsks.Load(coin, situation, startDate, endDate);

            ViewBag.show = orderBookAsks.Show; // TODO: использовать модели, без необходимости ViewBag и ViewData не использовать
            ViewBag.summVolume = orderBookAsks.SummVolume;

            return View();
        }

        [Authorize]
        public ActionResult ShowOrderBookBids(string coin, string situation, string startDate = "", string endDate = "")
        {
            OrderBookBidsService orderBookBids = new OrderBookBidsService();
            orderBookBids.Load(coin, situation, startDate, endDate);

            ViewBag.show = orderBookBids.Show;
            ViewBag.summVolume = orderBookBids.SummVolume;

            return View();
        }

        [Authorize]
        public ActionResult ShowTradeHistory(string coin, string situation, string orderType, string startDate = "", string endDate = "")
        {
            TradeHistoryService tradeHistory = new TradeHistoryService();
            tradeHistory.Load(coin, situation, orderType, startDate, endDate);

            ViewBag.show = tradeHistory.Show;
            ViewBag.summVolume = tradeHistory.SummVolume;

            return View();
        }

        [Authorize]
        public ActionResult ShowTradeDelta(string coin, string startDate = "", string endDate = "", string nulldelta = "all")
        {
            TradeDeltaService tradeDelta = new TradeDeltaService();
            tradeDelta.Load(coin, startDate, endDate, nulldelta);

            ViewBag.show = tradeDelta.Show;
            ViewBag.summDelta = tradeDelta.SummDelta;
            return View();
        }
    }
}