using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CRMCore.Models;
using CRMCore.Services.Binance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMCore.Controllers.TradeHistory
{
    public class TradeHistoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TradeHistory(string coin, string accounts, string startDate, string endDate)
        {
            BinanceAccount binanceAccount = new BinanceAccount();
#pragma warning disable CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до завершения вызова
            binanceAccount.LoadAsync(accounts, coin);
#pragma warning restore CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до завершения вызова

            while (binanceAccount.Trades == null)
                Debug.WriteLine("wait");

            ViewBag.AllOrders = binanceAccount.Trades
                .Where(x => x.Time >= DateTime.Parse(startDate) && 
                x.Time <= DateTime.Parse(endDate))
                .OrderByDescending(x => x.Time);

            DropDownFields.Swap(coin);

            return View();
        }
    }
}