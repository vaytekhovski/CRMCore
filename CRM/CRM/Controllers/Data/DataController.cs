using Microsoft.AspNetCore.Mvc;
using CRM.Services.Data;
using Microsoft.AspNetCore.Authorization;
using CRM.ViewModels.Data;
using CRM.Services;
using System.Collections.Generic;

namespace CRM.Controllers.Data
{
    [Authorize]
    public class DataController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            string minDate = "2019-04-05";
            var model = new OrderBookViewModel
            {
                StartDate = minDate,
                EndDate = Dates.CurrentDate(),
                Show = new List<Models.Database.OrderBookModel>()

            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ShowOrderBookAsks()
        {
            string minDate = "2019-04-05";
            var model = new OrderBookViewModel
            {
                StartDate = minDate,
                EndDate = Dates.CurrentDate(),
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
            string minDate = "2019-04-05";
            var model = new OrderBookViewModel
            {
                StartDate = minDate,
                EndDate = Dates.CurrentDate(),
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
            string minDate = "2019-04-05";
            var model = new TradeHistoryViewModel
            {
                StartDate = minDate,
                EndDate = Dates.CurrentDate(),
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
            string minDate = "2019-04-05";
            var model = new TradeDeltaViewModel
            {
                StartDate = minDate,
                EndDate = Dates.CurrentDate(),
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