﻿using Microsoft.AspNetCore.Mvc;
using CRM.Services.Data;
using Microsoft.AspNetCore.Authorization;
using CRM.ViewModels.Data;
using CRM.Services;
using System.Collections.Generic;
using CRM.Helpers;
using System;
using CRM.Services.Pagination;

namespace CRM.Controllers.Data
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly OrderBookService _orderBook;
        private readonly Services.Data.TradeHistoryService _tradeHistory;
        private readonly TradeDeltaService _tradeDelta;

        public DataController()
        {
            _orderBook = new OrderBookService();
            _tradeHistory = new Services.Data.TradeHistoryService();
            _tradeDelta = new TradeDeltaService();
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
            var model = _orderBook.Load("ask", ViewModel.Coin, ViewModel.Situation, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate));

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
            var model = _orderBook.Load("bid", ViewModel.Coin, ViewModel.Situation, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate));

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
            var model = _tradeHistory.Load(ViewModel.Coin, ViewModel.Situation, ViewModel.OrderType, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate));

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
            var model = _tradeDelta.Load(ViewModel.Coin, DateTime.Parse(ViewModel.StartDate), DateTime.Parse(ViewModel.EndDate), ViewModel.NullDelta);

            ViewModel.Show = model.Show;
            ViewModel.SummDelta = model.SummDelta;

            return View(ViewModel);
        }
    }
}