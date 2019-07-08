using Microsoft.AspNetCore.Mvc;
using CRM.Services.Data;
using Microsoft.AspNetCore.Authorization;
using CRM.ViewModels.Data;
using CRM.Services;
using System.Collections.Generic;
using CRM.Helpers;
using System;
using CRM.Services.Pagination;
using CRM.Models.Filters;
using System.Linq;

namespace CRM.Controllers.Data
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly OrderBookService _orderBook;
        private readonly Services.Data.TradeHistoryService _tradeHistory;
        private readonly TradeDeltaService _tradeDelta;
        private readonly PaginationService _paginationService;

        public DataController()
        {
            _orderBook = new OrderBookService();
            _tradeHistory = new Services.Data.TradeHistoryService();
            _tradeDelta = new TradeDeltaService();
            _paginationService = new PaginationService();
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
                Id = "OrderBook",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                Show = new List<Models.Database.OrderBookModel>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowOrderBookAsks(OrderBookViewModel ViewModel, string PageButton = "1")
        {
            var filter = new DataFilter
            {
                BookType = "ask",
                Situation = ViewModel.Situation,
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };

            var model = _orderBook.Load(filter);

            ViewModel.Show = model.Show;
            ViewModel.SummVolume = model.SummVolume;
            var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, model.CountOfElements);
            ViewModel.CurrentPage = filter.CurrentPage;
            ViewModel.CountOfPages = pagination.CountOfPages;
            ViewModel.Action = "Data/ShowOrderBookAsks";
            ViewModel.TypeOfDate = "date";

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
        public ActionResult ShowOrderBookBids(OrderBookViewModel ViewModel, string PageButton = "1")
        {
            var filter = new DataFilter
            {
                BookType = "bid",
                Situation = ViewModel.Situation,
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };
            var model = _orderBook.Load(filter);

            ViewModel.Show = model.Show;
            ViewModel.SummVolume = model.SummVolume;
            var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, model.Show.Count());
            ViewModel.CurrentPage = filter.CurrentPage;
            ViewModel.CountOfPages = pagination.CountOfPages;
            ViewModel.Action = "Data/ShowOrderBookBids";
            ViewModel.TypeOfDate = "date";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult ShowTradeHistory()
        {
            var model = new TradeHistoryViewModel
            {
                Id = "TradeHistoryDatas",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                Show = new List<Models.Database.TradeHistoryModel>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowTradeHistory(TradeHistoryViewModel ViewModel, string PageButton = "1")
        {
            var filter = new DataFilter
            {
                OrderType = ViewModel.OrderType,
                Situation = ViewModel.Situation,
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };
            var model = _tradeHistory.Load(filter);

            ViewModel.Show = model.Show;
            ViewModel.SummVolume = model.SummVolume;
            var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, model.CountOfElements);
            ViewModel.CurrentPage = filter.CurrentPage;
            ViewModel.CountOfPages = pagination.CountOfPages;
            ViewModel.Action = "Data/ShowTradeHistory";
            ViewModel.TypeOfDate = "date";

            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult ShowTradeDelta()
        {
            var model = new TradeDeltaViewModel
            {
                Id = "Delta",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                Show = new List<Models.Database.TradeDeltaModel>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowTradeDelta(TradeDeltaViewModel ViewModel, string PageButton = "1")
        {
            var filter = new DataFilter
            {
                nulldelta = ViewModel.NullDelta,
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };
            var model = _tradeDelta.Load(filter);

            ViewModel.Show = model.Show;
            ViewModel.SummDelta = model.SummDelta;

            var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, model.CountOfElements);
            ViewModel.CurrentPage = filter.CurrentPage;
            ViewModel.CountOfPages = pagination.CountOfPages;
            ViewModel.Action = "Data/ShowTradeDelta";
            ViewModel.TypeOfDate = "date";

            return View(ViewModel);
        }
    }
}