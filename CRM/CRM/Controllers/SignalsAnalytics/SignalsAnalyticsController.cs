using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Models.SignalsAnalytics;
using CRM.Services.SignalsAnalytics;
using CRM.ViewModels.SignalsAnalytics;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.SignalsAnalytics
{
    public class SignalsAnalyticsController : Controller
    {
        [HttpGet]
        public ActionResult SignalsAnalytics()
        {
            var model = new SignalsAnalyticsViewModel
            {
                Exchange = "",
                Coin = "",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                SignalsAnalytics = new List<SignalsAnalyticsModel>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SignalsAnalytics(SignalsAnalyticsViewModel model)
        {
            if (DateTime.TryParse(model.StartDate, out var StartDate) && DateTime.TryParse(model.EndDate, out var EndDate))
            {
                model.SignalsAnalytics = SignalsAnalyticsService.Load(model.Exchange, model.Coin, StartDate, EndDate);

                return View(model);
            }

            ModelState.AddModelError("Date", "Dates invalid");
            return View(model);
        }
    }
}