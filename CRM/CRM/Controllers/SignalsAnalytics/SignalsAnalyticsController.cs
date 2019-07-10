using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.Pagination;
using CRM.Services.SignalsAnalytics;
using CRM.ViewModels.SignalsAnalytics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.SignalsAnalytics
{
    [Authorize]
    public class SignalsAnalyticsController : Controller
    {
        private readonly SignalsAnalyticsService _signalsAnalyticsService;
        private readonly PaginationService _paginationService;

        public SignalsAnalyticsController()
        {
            _signalsAnalyticsService = new SignalsAnalyticsService();
            _paginationService = new PaginationService();
        }


        [HttpGet]
        public ActionResult SignalsPrivate()
        {
            var model = new SignalsAnalyticsViewModel
            {
                Id = "Signals",
                Exchange = "",
                Coin = "",
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr,
                TradeHistoryDeltas = new List<Models.Master.TradeHistoryDelta>(),
                SignalsPrivates = new List<Models.Master.SignalsPrivate>()
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Exchanges = DropDownFields.GetExchanges();
            ViewBag.Nulls = DropDownFields.GetNulls();
            model.CurrentPage = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult SignalsPrivate(SignalsAnalyticsViewModel ViewModel, string PageButton = "1")
        {
            int PageNumber = Convert.ToInt32(PageButton);

            var model = _signalsAnalyticsService.LoadSignalsPrivate(ViewModel);

            ViewModel.SignalsPrivates = model.SignalsPrivates.Skip((PageNumber - 1) * 100).Take(100).ToList();
            
            var pagination = _paginationService.GetPaginationModel(PageNumber, model.SignalsPrivates.Count);
            ViewModel.CurrentPage = PageNumber;
            ViewModel.FirstVisiblePage = pagination.FirstVisiblePage;
            ViewModel.LastVisiblePage = pagination.LastVisiblePage;
            ViewModel.CountOfPages = pagination.CountOfPages;
            ViewModel.Action = "SignalsAnalytics/SignalsPrivate";
            ViewModel.TypeOfDate = "datetime-local";
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Exchanges = DropDownFields.GetExchanges();
            ViewBag.Nulls = DropDownFields.GetNulls();
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult TradeHistoryDeltas()
        {
            var model = new SignalsAnalyticsViewModel
            {
                Id = "Signals",
                Exchange = "",
                Coin = "",
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr,
                TradeHistoryDeltas = new List<Models.Master.TradeHistoryDelta>(),
                SignalsPrivates = new List<Models.Master.SignalsPrivate>()
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Exchanges = DropDownFields.GetExchanges();
            ViewBag.Nulls = DropDownFields.GetNulls();
            model.CurrentPage = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult TradeHistoryDeltas(SignalsAnalyticsViewModel ViewModel, string PageButton = "1")
        {
            int PageNumber = Convert.ToInt32(PageButton);

            var model = _signalsAnalyticsService.LoadTradeHistoryDelta(ViewModel);

            ViewModel.TradeHistoryDeltas = model.TradeHistoryDeltas.Skip((PageNumber - 1) * 100).Take(100).ToList();

            var pagination = _paginationService.GetPaginationModel(PageNumber, model.TradeHistoryDeltas.Count);
            ViewModel.CurrentPage = PageNumber;
            ViewModel.CountOfPages = pagination.CountOfPages;
            ViewModel.Action = "SignalsAnalytics/TradeHistoryDeltas";
            ViewModel.TypeOfDate = "datetime-local";
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Exchanges = DropDownFields.GetExchanges();
            ViewBag.Nulls = DropDownFields.GetNulls();
            ViewBag.Situations = DropDownFields.GetSituations();
            return View(ViewModel);
        }


    }
}