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
    [Authorize]
    public class DataController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ShowOrderBookAsks(string coin, string situation, string startDate = "", string endDate = "")
        {
            OrderBookService orderBook = new OrderBookService();
            orderBook.Load("ask", coin, situation, startDate, endDate);

            ViewBag.show = orderBook.Show; // TODO: использовать модели, без необходимости ViewBag и ViewData не использовать
            ViewBag.summVolume = orderBook.SummVolume;

            return View();
        }
        
        public ActionResult ShowOrderBookBids(string coin, string situation, string startDate = "", string endDate = "")
        {
            OrderBookService orderBook = new OrderBookService();
            orderBook.Load("bid", coin, situation, startDate, endDate);

            ViewBag.show = orderBook.Show;
            ViewBag.summVolume = orderBook.SummVolume;

            return View();
        }
        
        public ActionResult ShowTradeHistory(string coin, string situation, string orderType, string startDate = "", string endDate = "")
        {
            TradeHistoryService tradeHistory = new TradeHistoryService();
            tradeHistory.Load(coin, situation, orderType, startDate, endDate);

            ViewBag.show = tradeHistory.Show;
            ViewBag.summVolume = tradeHistory.SummVolume;

            return View();
        }
        
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