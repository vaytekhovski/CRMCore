using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AuthApp.Controllers;
using Business;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using Business.Models.Master;
using CRM.Helpers;
using CRM.Services;
using CRM.Services.Balances;
using CRM.ViewModels.ManualTrading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.ManualTrading
{
    [Authorize]
    public class ManualTradingController : Controller
    {
        private readonly ManualTradingService manualTradingService;
        private readonly DatavisioAPIService datavisioAPIService;
        private readonly BalancesService balancesService;


        public ManualTradingController()
        {
            manualTradingService = new ManualTradingService();

            datavisioAPIService = new DatavisioAPIService();
            balancesService = new BalancesService();

        }

        [HttpGet]
        public async Task<ActionResult> Trade()
        {
            SeparateHelper.Separator.NumberDecimalSeparator = ".";


            ManualTradingModel ViewModel = new ManualTradingModel();

            ViewModel.Account = "556c8663-5706-4112-9440-c6ac965cfa26";
            ViewModel.Coin = "BTC";
            //ViewModel.StartDate = DateTime.UtcNow.AddHours(2);
            ViewModel.StartDate = DateTime.UtcNow.AddDays(-4);
            ViewModel.StartDate = ViewModel.StartDate.AddMinutes(-ViewModel.StartDate.Minute);
            //ViewModel.EndDate = DateTime.UtcNow.AddHours(3);
            ViewModel.EndDate = DateTime.UtcNow.AddDays(-3);
            ViewModel.TimeRange = 1;

            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();


            ViewModel = manualTradingService.Load(accountId, ViewModel, token).Result;

            if (ViewModel.CoinPrices.Count != 0)
            {
                ViewBag.MinCourse = Convert.ToInt32(ViewModel.CoinPrices.Min());
                ViewBag.MaxCourse = Convert.ToInt32(ViewModel.CoinPrices.Max());
            }
            else
            {
                ViewBag.MinCourse = 100;
                ViewBag.MaxCourse = 50;
            }


            //ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.TimeRanges = DropDownFields.GetTimeRages();
            ViewBag.TradingViewSymbol = $"BINANCE:{ViewModel.Coin}USDT";
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Trade(ManualTradingModel ViewModel)
        {
            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            ViewModel = manualTradingService.Load(accountId, ViewModel, token).Result;

            if (ViewModel.CoinPrices.Count != 0)
            {
                ViewBag.MinCourse = Convert.ToInt32(ViewModel.CoinPrices.Min());
                ViewBag.MaxCourse = Convert.ToInt32(ViewModel.CoinPrices.Max());
            }
            else
            {
                ViewBag.MinCourse = 100;
                ViewBag.MaxCourse = 50;
            }

            //ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.TimeRanges = DropDownFields.GetTimeRages();

            ViewBag.TradingViewSymbol = $"BINANCE:{ViewModel.Coin}USDT";

            return View(ViewModel);
        }

        public IActionResult Buy(ManualTradingModel ViewModel)
        {
            var token = HttpContext.User.Identity.Name;

            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            var amount = Convert.ToDouble(ViewModel.BuyAmount.ToString().Replace(',', '.').Replace(" " + ViewModel.Coin, ""));
            var response = datavisioAPIService.EnterDeal(accountId, token, new PlaceOrderRequest()
            {
                exchange = "binance",
                @base = ViewModel.Coin,
                quote = "USDT",
                amount = amount
            }).Result;

            ViewModel.BuyAmount = "";
            ViewModel.PlaceOrderResponse = response;

            return RedirectToAction("Trade", "ManualTrading", new { ViewModel = ViewModel});
        }

        public IActionResult Sell(string DealId, decimal amount, string @base, string quoute = "usdt")
        {
            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            var response = datavisioAPIService.LeaveDeal(accountId, token, DealId, amount).Result;
            var disableResponse = datavisioAPIService.DisablePair(accountId, token, @base, quoute);
            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> GetDeal(string DealId)
        {
            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            var response = datavisioAPIService.GetDeal(accountId, token, DealId).Result;
            response.coin = response.@base;

            GetDealModel Model = new GetDealModel()
            {
                Deal = response,
                balancesModel = await balancesService.LoadBalancesAsync(accountId, token)
            };

            
            var candles = datavisioAPIService.GetCandles(token, Model.Deal.coin, Model.Deal.quote).Result.ToList();
            
            Model.LastPrice = candles.Last().c;

            var UserName = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            foreach (var item in Model.Deal.orders)
            {
                item.created = item.created.AddHours(3);
                item.closed = item.closed != null ? item.closed.Value.AddHours(3) : new DateTime(1999, 01, 01);

                if (UserName == "guest")
                {
                    // Увеличение
                    item.amount *= 10;
                    item.filled *= 10;
                }
            }
            Model.Deal.orders = Model.Deal.orders.OrderByDescending(x => x.created).ToArray();
            
            return View(Model);
        }

        public IActionResult TradeDeal(GetDealModel Model)
        {
            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            double amount = Convert.ToDouble(Model.BuyAmount.ToString().Replace(',', '.').Replace(" " + Model.Deal.coin, ""));

            var response = datavisioAPIService.TradeDeal(accountId, token, Model.Deal.id, amount).Result;

            return RedirectToAction("GetDeal", "ManualTrading", new {DealId = Model.Deal.id });
        }

    }
}