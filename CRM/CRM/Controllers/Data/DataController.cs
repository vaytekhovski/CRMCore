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
        public ActionResult ShowOrderBookAsks(OrderBookViewModel ViewModel)
        {
            var model = orderBook.Load("ask", ViewModel.Coin, ViewModel.Situation, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate));

            ViewModel.Show = model.Show;
            ViewModel.SummVolume = model.SummVolume;

            return View(ViewModel);
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
        public ActionResult ShowOrderBookBids(OrderBookViewModel ViewModel)
        {
            var model = orderBook.Load("bid", ViewModel.Coin, ViewModel.Situation, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate));

            ViewModel.Show = model.Show;
            ViewModel.SummVolume = model.SummVolume;

            return View(ViewModel);
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
        public ActionResult ShowTradeHistory(TradeHistoryViewModel ViewModel)
        {
            ViewModel.OrderType = ViewModel.OrderType == null ? "all" : ViewModel.OrderType;

            var model = tradeHistory.Load(ViewModel.Coin, ViewModel.Situation, ViewModel.OrderType, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate));

            ViewModel.Show = model.Show;
            ViewModel.SummVolume = model.SummVolume;

            return View(ViewModel);
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
        public ActionResult ShowTradeDelta(TradeDeltaViewModel ViewModel)
        {
            ViewModel.NullDelta = ViewModel.NullDelta == null ? "all" : ViewModel.NullDelta;

            var model = tradeDelta.Load(ViewModel.Coin, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate), ViewModel.NullDelta);

            ViewModel.Show = model.Show;
            ViewModel.SummDelta = model.SummDelta;

            return View(ViewModel);
        }
    }
}