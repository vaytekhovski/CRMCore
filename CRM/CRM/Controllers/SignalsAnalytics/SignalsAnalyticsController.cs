using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
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
                EndDate = DatesHelper.CurrentDateStr
            };
            return View();
        }

        [HttpPost]
        public ActionResult SignalsAnalytics(SignalsAnalyticsViewModel model)
        {
            return View(model);
        }
    }
}