using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Master;
using CRM.Models;
using CRM.Models.Binance;
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
                Coin = "all"
            };

            return View(model);
        }

        [HttpPost] //TODO: разметить везде, где GET, а где POST
        public ActionResult TradeHistory(TradeHistoryFilterModel model) //TODO: load with default values
        {
            THService tHService = new THService();

            tHService.Load(model.Account, model.Coin, model.StartDate, model.EndDate);

            model.Orders = tHService.AccountTradeHistories.OrderByDescending(x => x.Time).ToList();
            model.Profit = tHService.Profit;

            return View(model);
        }
    }
}