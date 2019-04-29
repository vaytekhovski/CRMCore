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

            model = LoadTradeHistory(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult TradeHistory(TradeHistoryFilterModel model)
        {
            model = LoadTradeHistory(model, 0);

            return View(model);
        }

        public ActionResult PaginalOutPut(TradeHistoryFilterModel model, string PageButton)
        {
            model = GetTradeHistory(model, Convert.ToInt32(PageButton));

            return View("TradeHistory", model);
        }

        private static THService tHService;

        private TradeHistoryFilterModel LoadTradeHistory(TradeHistoryFilterModel model, int PageNumber = 0)
        {
            tHService = new THService();

            DateTime StartDate = DateTime.Parse(model.StartDate);
            DateTime EndDate = DateTime.Parse(model.EndDate).AddDays(1);

            tHService.Load(model.Account, model.Coin, StartDate, EndDate);

            model.Orders = tHService.AccountTradeHistories;

            model.CountOfPages = tHService.CountOfPages;

            model.Orders = model.Orders.Take(100).Skip(PageNumber * 100).ToList();

            model.TotalProfit = tHService.TotalProfit;
            model.TotalPercentProfit = tHService.TotalPercentProfit;

            return model;
        }

        private TradeHistoryFilterModel GetTradeHistory(TradeHistoryFilterModel model, int PageNumber)
        {
            model.Orders = tHService.AccountTradeHistories
                .Skip(PageNumber * 100)
                .Take(100)
                .ToList();


            model.CountOfPages = tHService.CountOfPages;

            model.TotalProfit = tHService.TotalProfit;
            model.TotalPercentProfit = tHService.TotalPercentProfit;

            return model;
        }


        
    }
}