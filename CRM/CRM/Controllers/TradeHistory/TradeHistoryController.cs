using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Services.Binance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.TradeHistory
{
    [Authorize]
    public class TradeHistoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TradeHistory(string coin, string accounts, string startDate, string endDate)
        {
            BinanceAccountService binanceAccount = new BinanceAccountService();
#pragma warning disable CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до завершения вызова
            binanceAccount.LoadAsync(accounts, coin);
#pragma warning restore CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до завершения вызова

            while (!binanceAccount.isDone)
                continue;

            ViewBag.AllOrders = binanceAccount.AccountTradeHistories.OrderBy(x => x.Time);

            if (accounts != DropDownFields.Accounts.First().Value)
            {
                DropDownFields.SwapAccounts();
            }
            DropDownFields.SwapCoins(coin);

            return View();
        }
    }
}