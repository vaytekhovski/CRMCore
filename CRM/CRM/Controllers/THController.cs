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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TradeHistory(string coin, string accounts, string startDate, string endDate)
        {
            THService tHService = new THService();

            tHService.Load(accounts,coin);

            ViewBag.Orders = tHService.AccountTradeHistories.OrderByDescending(x => x.Time);

            DropDownFields.SwapCoins(coin);
            DropDownFields.SwapAccounts(accounts);
            return View();
        }
    }
}