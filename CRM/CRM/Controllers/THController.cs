using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Master;
using CRM.Models;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class THController : Controller
    {
        public ActionResult TradeHistory(string coin, string accounts, string startDate, string endDate) //TODO: load with default values
        {
            THService tHService = new THService();

            tHService.Load(accounts,coin);

            ViewBag.Orders = tHService.AccountTradeHistories.OrderByDescending(x => x.Time);
            ViewBag.Profit = tHService.Profit;

            DropDownFields.SwapCoins(coin);
            DropDownFields.SwapAccounts(accounts);
            return View();
        }
    }
}