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
    public class SignalsAnalyticsController : Controller
    {
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
            return View(model);
        }

        [HttpPost]
        public ActionResult SignalsPrivate(SignalsAnalyticsViewModel model)
        {
            if (DateTime.TryParse(model.StartDate, out var StartDate) && DateTime.TryParse(model.EndDate, out var EndDate))
            {
                model.SignalsPrivates = SignalsAnalyticsService.LoadSignalsPrivate(model.Exchange, model.Coin, StartDate, EndDate);

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
            return View(model);
        }

        [HttpPost]
        public ActionResult TradeHistoryDeltas(SignalsAnalyticsViewModel model)
        {
            if (DateTime.TryParse(model.StartDate, out var StartDate) && DateTime.TryParse(model.EndDate, out var EndDate))
            {
                model.TradeHistoryDeltas = SignalsAnalyticsService.LoadTradeHistoryDelta(model.Exchange, model.Coin, StartDate, EndDate);

                return View(model);
            }

            ModelState.AddModelError("Date", "Dates invalid");
            return View(model);
        }


    }
}