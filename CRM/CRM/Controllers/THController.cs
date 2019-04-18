using System;
using System.Linq;
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
                Account = "all",
                Coin = "all",
                StartDate = Dates.MinDate,
                EndDate = Dates.CurrentDate()
            };

            model = LoadTradeHistory(model);

            return View(model);
        }
        
        [HttpPost] //TODO: [COMPLETE]  разметить везде, где GET, а где POST
        public ActionResult TradeHistory(TradeHistoryFilterModel model) 
        {
            model = LoadTradeHistory(model);

            return View(model);
        }



        private TradeHistoryFilterModel LoadTradeHistory(TradeHistoryFilterModel model)
        {
            THService tHService = new THService();

            DateTime StartDate = DateTime.Parse(model.StartDate);
            DateTime EndDate = DateTime.Parse(model.EndDate).AddDays(1);

            tHService.Load(model.Account, model.Coin, StartDate, EndDate);


            model.Orders = tHService.AccountTradeHistories
                .OrderByDescending(x => x.Time)
                .Where(x => x.Time >= StartDate && x.Time <= EndDate)
                .ToList();

            model.TotalProfit = tHService.TotalProfit;
            model.TotalPercentProfit = tHService.TotalPercentProfit;

            return model;
        }

        
    }
}