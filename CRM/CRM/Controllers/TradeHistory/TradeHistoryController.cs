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
    public class TradeHistoryController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult TradeHistory(string coin, string accounts, string startDate, string endDate)
        {
            BinanceAccount binanceAccount = new BinanceAccount();
#pragma warning disable CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до завершения вызова
            binanceAccount.LoadAsync(accounts, coin);
#pragma warning restore CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до завершения вызова

            while (!binanceAccount.isDone)
                continue;

            ViewBag.AllOrders = binanceAccount.AccountTradeHistories
                .OrderBy(x => x.Time);

            if (accounts != DropDownFields.Accounts.ToArray()[0].Value)
                DropDownFields.SwapAccounts();
            DropDownFields.SwapCoins(coin);

            return View();
        }
    }
}