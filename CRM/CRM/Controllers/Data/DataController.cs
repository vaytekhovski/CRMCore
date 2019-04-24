using Microsoft.AspNetCore.Mvc;
using CRM.Services.Data;
using Microsoft.AspNetCore.Authorization;
using CRM.ViewModels.Data;
using CRM.Services;
using System.Collections.Generic;
using CRM.Helpers;

namespace CRM.Controllers.Data
{
    [Authorize]
    public class DataController : Controller
    {
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
            OrderBookService orderBook = new OrderBookService();
            orderBook.Load("ask", model.Coin, model.Situation, model.StartDate, model.EndDate);

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
            OrderBookService orderBook = new OrderBookService();
            orderBook.Load("bid", model.Coin, model.Situation, model.StartDate, model.EndDate);

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
            TradeHistoryService tradeHistory = new TradeHistoryService();
            model.OrderType = model.OrderType == null ? "all" : model.OrderType;
            tradeHistory.Load(model.Coin, model.Situation, model.OrderType, model.StartDate, model.EndDate);

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
            TradeDeltaService tradeDelta = new TradeDeltaService();

            model.NullDelta = model.NullDelta == null ? "all" : model.NullDelta;

            tradeDelta.Load(model.Coin, model.StartDate, model.EndDate, model.NullDelta);

            model.Show = tradeDelta.Show;
            model.SummDelta = tradeDelta.SummDelta;
            return View(model);
        }
    }
}