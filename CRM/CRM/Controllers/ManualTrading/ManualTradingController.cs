using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ViewModel.StartDate = DateTime.UtcNow.AddHours(2);
            ViewModel.StartDate = ViewModel.StartDate.AddMinutes(-ViewModel.StartDate.Minute);
            ViewModel.EndDate = DateTime.UtcNow.AddHours(3);
            ViewModel.TimeRange = 1;

            var token = HttpContext.User.Identity.Name;


            ViewModel = manualTradingService.Load(ViewModel, token).Result;

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

            ViewModel = manualTradingService.Load(ViewModel, token).Result;

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

            var response = datavisioAPIService.EnterDeal(token, new PlaceOrderRequest()
            {
                exchange = "binance",
                @base = ViewModel.Coin,
                quote = "USDT",
                amount = Convert.ToDouble(ViewModel.BuyAmount.ToString().Replace(',','.').Replace(" " + ViewModel.Coin, ""))
            }).Result;

            ViewModel.BuyAmount = "";
            ViewModel.PlaceOrderResponse = response;

            return RedirectToAction("Trade", "ManualTrading", new { ViewModel = ViewModel});
        }

        public IActionResult Sell(string DealId)
        {
            var token = HttpContext.User.Identity.Name;

            var response = datavisioAPIService.LeaveDeal(token, DealId).Result;

            return RedirectToAction("Trade", "ManualTrading");
        }

        public async Task<IActionResult> GetDeal(string DealId)
        {
            var token = HttpContext.User.Identity.Name;

            var response = datavisioAPIService.GetDeal(token, DealId).Result;
            response.coin = response.@base;

            GetDealModel Model = new GetDealModel()
            {
                Deal = response,
                balancesModel = await balancesService.LoadBalancesAsync(token)
            };

            var candles = datavisioAPIService.GetCandles(token, Model.Deal.coin).Result.ToList();
            Model.LastPrice = candles.Last().c;

            foreach (var item in Model.Deal.orders)
            {
                item.created = item.created.AddHours(3);
                item.closed = item.closed != null ? item.closed.Value.AddHours(3) : new DateTime(1999, 01, 01);
            }
            Model.Deal.orders = Model.Deal.orders.OrderByDescending(x => x.created).ToArray();
            return View(Model);
        }

        public IActionResult TradeDeal(GetDealModel Model)
        {
            var token = HttpContext.User.Identity.Name;
            double amount = Convert.ToDouble(Model.BuyAmount.ToString().Replace(',', '.').Replace(" " + Model.Deal.coin, ""));

            var response = datavisioAPIService.TradeDeal(token, Model.Deal.id, amount).Result;

            return RedirectToAction("GetDeal", "ManualTrading", new {DealId = Model.Deal.id });
        }

    }
}