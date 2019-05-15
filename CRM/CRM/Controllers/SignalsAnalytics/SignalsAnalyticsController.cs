using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Services.SignalsAnalytics;
using CRM.ViewModels.SignalsAnalytics;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.SignalsAnalytics
{
    [Authorize]
    public class SignalsAnalyticsController : Controller
    {
        private static SignalsAnalyticsService _SignalsAnalyticsService;

        [HttpGet]
        public ActionResult SignalsPrivate()
        {
            var model = new SignalsAnalyticsViewModel
            {
                Exchange = "",
                Coin = "",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                TradeHistoryDeltas = new List<Models.Master.TradeHistoryDelta>(),
                SignalsPrivates = new List<Models.Master.SignalsPrivate>()
            };

            _SignalsAnalyticsService = new SignalsAnalyticsService();
            model.CurrentPage = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult SignalsPrivate(SignalsAnalyticsViewModel model, int PageNumber = 0)
        {
            if (DateTime.TryParse(model.StartDate, out var StartDate) && DateTime.TryParse(model.EndDate, out var EndDate))
            {
                if (_SignalsAnalyticsService.SignalsPrivates.Count == 0 || PageNumber == 0)
                {
                    model = _SignalsAnalyticsService.LoadSignalsPrivate(model);
                }

                model.CountOfPages = (int)Math.Ceiling((decimal)((double)_SignalsAnalyticsService.SignalsPrivates.Count / 100));
                model.SignalsPrivates = _SignalsAnalyticsService.SignalsPrivates.Skip((PageNumber - 1) * 100).Take(100).ToList();
                model.CurrentPage = PageNumber;
                model.MaxAvailablePageNumber = PageNumber + 5 > model.CountOfPages ? model.CountOfPages : PageNumber + 5;
                return View(model);
            }

            ModelState.AddModelError("Date", "Dates invalid");
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
                TradeHistoryDeltas = new List<Models.Master.TradeHistoryDelta>(),
                SignalsPrivates = new List<Models.Master.SignalsPrivate>()
            };

            model.CurrentPage = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult TradeHistoryDeltas(SignalsAnalyticsViewModel model, int PageNumber = 0)
        {
            if (DateTime.TryParse(model.StartDate, out var StartDate) && DateTime.TryParse(model.EndDate, out var EndDate))
            {
                if (_SignalsAnalyticsService.TradeHistoryDeltas.Count == 0 || PageNumber == 0)
                {
                    model = _SignalsAnalyticsService.LoadTradeHistoryDelta(model);
                }

                model.CountOfPages = (int)Math.Ceiling((decimal)((double)_SignalsAnalyticsService.TradeHistoryDeltas.Count / 100));
                model.TradeHistoryDeltas = _SignalsAnalyticsService.TradeHistoryDeltas.Skip((PageNumber - 1) * 100).Take(100).ToList();
                model.CurrentPage = PageNumber;
                model.MaxAvailablePageNumber = PageNumber + 5 > model.CountOfPages ? model.CountOfPages : PageNumber + 5;
                return View(model);
            }

            ModelState.AddModelError("Date", "Dates invalid");
            return View(model);
        }


    }
}