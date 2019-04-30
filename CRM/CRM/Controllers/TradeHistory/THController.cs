using System;
using System.Linq;
using CRM.Helpers;
using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class THController : Controller
    {
        private static THService tHService;

        [HttpGet]
        public ActionResult TradeHistory()
        {
            var model = new TradeHistoryFilterModel
            {
                Account = "/",
                Coin = "all",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };

            tHService = new THService();

            return View(model);
        }

        [HttpPost]
        public ActionResult TradeHistory(TradeHistoryFilterModel model, string PageButton = "0")
        {
            model = LoadTradeHistory(model, Convert.ToInt32(PageButton));

            return View(model);
        }

        private TradeHistoryFilterModel LoadTradeHistory(TradeHistoryFilterModel model, int PageNumber = 0)
        {
            DateTime StartDate = DateTime.Parse(model.StartDate);
            DateTime EndDate = DateTime.Parse(model.EndDate).AddDays(1);

            if (tHService.AccountTradeHistories.Count == 0)
            {
                tHService.Load(model.Account, model.Coin, StartDate, EndDate);
            }

            model.CountOfPages = tHService.CountOfPages;

            model.Orders = tHService.AccountTradeHistories.Skip((PageNumber - 1) * 100).Take(100).ToList();

            model.TotalProfit = tHService.TotalProfit;
            model.TotalPercentProfit = tHService.TotalPercentProfit;

            return model;
        }


        
    }
}