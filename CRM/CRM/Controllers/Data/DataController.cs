using Microsoft.AspNetCore.Mvc;
using CRM.Services.Data;
using Microsoft.AspNetCore.Authorization;
using CRM.ViewModels.Data;
using CRM.Services;
using System.Collections.Generic;
using CRM.Helpers;
using System;

namespace CRM.Controllers.Data
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly OrderBookService orderBook;
        private readonly TradeHistoryService tradeHistory;
        private readonly TradeDeltaService tradeDelta;

        public DataController()
        {
            orderBook = new OrderBookService();
            tradeHistory = new TradeHistoryService();
            tradeDelta = new TradeDeltaService();
        }


        [HttpGet]
        public ActionResult Index()
        {
            var model = new OrderBookViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                Show = new List<Models.Database.OrderBookModel>()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ShowOrderBookAsks()
        {
            var model = new OrderBookViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                Show = new List<Models.Database.OrderBookModel>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowOrderBookAsks(OrderBookViewModel model)
        {
            orderBook.Load("ask", model.Coin, model.Situation, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate));

            model.Show = orderBook.Show;
            model.SummVolume = orderBook.SummVolume;

            return View(model);
        }

        [HttpGet]
        public ActionResult ShowOrderBookBids()
        {
            var model = new OrderBookViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                Show = new List<Models.Database.OrderBookModel>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowOrderBookBids(OrderBookViewModel model)
        {
            orderBook.Load("bid", model.Coin, model.Situation, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate));

            model.Show = orderBook.Show;
            model.SummVolume = orderBook.SummVolume;

            return View(model);
        }

        [HttpGet]
        public ActionResult ShowTradeHistory()
        {
            var model = new TradeHistoryViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                Show = new List<Models.Database.TradeHistoryModel>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowTradeHistory(TradeHistoryViewModel model)
        {
            model.OrderType = model.OrderType == null ? "all" : model.OrderType;
            tradeHistory.Load(model.Coin, model.Situation, model.OrderType, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate));

            model.Show = tradeHistory.Show;
            model.SummVolume = tradeHistory.SummVolume;

            return View(model);
        }

        [HttpGet]
        public ActionResult ShowTradeDelta()
        {
            var model = new TradeDeltaViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                Show = new List<Models.Database.TradeDeltaModel>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowTradeDelta(TradeDeltaViewModel model)
        {
            model.NullDelta = model.NullDelta == null ? "all" : model.NullDelta;

            tradeDelta.Load(model.Coin, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate), model.NullDelta);

            model.Show = tradeDelta.Show;
            model.SummDelta = tradeDelta.SummDelta;
            return View(model);
        }
    }
}