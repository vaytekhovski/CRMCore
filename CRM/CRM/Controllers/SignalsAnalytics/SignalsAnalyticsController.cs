using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
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
        private readonly SignalsAnalyticsService signalsAnalyticsService;
        private readonly PaginationService paginationService;

        public SignalsAnalyticsController()
        {
            signalsAnalyticsService = new SignalsAnalyticsService();
            paginationService = new PaginationService();
        }


        [HttpGet]
        public ActionResult SignalsPrivate()
        {
            var model = new SignalsAnalyticsViewModel
            {
                Exchange = "",
                Coin = "",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                StartTime = "00:00",
                EndTime = "23:59",
                TradeHistoryDeltas = new List<Models.Master.TradeHistoryDelta>(),
                SignalsPrivates = new List<Models.Master.SignalsPrivate>()
            };

            model.CurrentPage = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult SignalsPrivate(SignalsAnalyticsViewModel model, string PageButton = "1")
        {
            int PageNumber = Convert.ToInt32(PageButton);
            if (signalsAnalyticsService.SignalsPrivates.Count == 0 || PageNumber == 0)
            {
                signalsAnalyticsService.LoadSignalsPrivate(model);
            }

            model.SignalsPrivates = signalsAnalyticsService.SignalsPrivates.Skip((PageNumber - 1) * 100).Take(100).ToList();
            model.CurrentPage = PageNumber;


            var pagination = paginationService.GetPaginationModel(PageNumber, signalsAnalyticsService.SignalsPrivates.Count);

            model.FirstVisiblePage = pagination.FirstVisiblePage;
            model.LastVisiblePage = pagination.LastVisiblePage;
            model.CountOfPages = pagination.CountOfPages;
            return View(model);
        }

        [HttpGet]
        public ActionResult TradeHistoryDeltas()
        {
            var model = new SignalsAnalyticsViewModel
            {
                Exchange = "",
                Coin = "",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                StartTime = "00:00",
                EndTime = "23:59",
                TradeHistoryDeltas = new List<Models.Master.TradeHistoryDelta>(),
                SignalsPrivates = new List<Models.Master.SignalsPrivate>()
            };

            model.CurrentPage = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult TradeHistoryDeltas(SignalsAnalyticsViewModel model, int PageNumber = 0)
        {
            if (signalsAnalyticsService.TradeHistoryDeltas.Count == 0 || PageNumber == 0)
            {
                signalsAnalyticsService.LoadTradeHistoryDelta(model);
            }
            model.CountOfPages = (int)Math.Ceiling((decimal)((double)signalsAnalyticsService.TradeHistoryDeltas.Count / 100));
            model.TradeHistoryDeltas = signalsAnalyticsService.TradeHistoryDeltas.Skip((PageNumber - 1) * 100).Take(100).ToList();
            model.CurrentPage = PageNumber;
            model.MaxAvailablePageNumber = PageNumber + 5 > model.CountOfPages ? model.CountOfPages : PageNumber + 5;
            return View(model);
        }


    }
}