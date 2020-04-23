using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.Contexts;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using Business.Models.Master;
using CRM.Helpers;
using CRM.Services;
using CRM.Services.Balances;
using CRM.ViewModels.ManualTrading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.ManualTrading
{
    public class ManualTradingController : Controller
    {
        private readonly ManualTradingService manualTradingService;
        private readonly DatavisioAPIService datavisioAPIService;


        public ManualTradingController()
        {
            manualTradingService = new ManualTradingService();
            datavisioAPIService = new DatavisioAPIService();
        }

        [HttpGet]
        public async Task<ActionResult> Trade()
        {
            SeparateHelper.Separator.NumberDecimalSeparator = ".";


            ManualTradingModel ViewModel = new ManualTradingModel();

            ViewModel.Account = "556c8663-5706-4112-9440-c6ac965cfa26";
            ViewModel.Coin = "BTC";

            ViewModel = manualTradingService.Load(ViewModel, HttpContext).Result;

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
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Trade(ManualTradingModel ViewModel)
        {

            ViewModel = manualTradingService.Load(ViewModel, HttpContext).Result;

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
            return View(ViewModel);
        }

        public IActionResult Buy(ManualTradingModel ViewModel)
        {

            var response = datavisioAPIService.PlaceOrder(HttpContext, new PlaceOrderRequest()
            {
                type = "market",
                side = "buy",
                @base = ViewModel.Coin,
                quote = "USDT",
                amount = Convert.ToDouble(ViewModel.BuyAmount)
            }).Result;

            ViewModel.BuyAmount = "";
            ViewModel.PlaceOrderResponse = response;

            return RedirectToAction("Trade", "ManualTrading", new { ViewModel = ViewModel});
        }

        public IActionResult Sell(ManualTradingModel ViewModel)
        {

            var response = datavisioAPIService.PlaceOrder(HttpContext ,new PlaceOrderRequest()
            {
                type = "market",
                side = "sell",
                @base = ViewModel.Coin,
                quote = "USDT",
                amount = Convert.ToDouble(ViewModel.SellAmount)
            }).Result;

            ViewModel.SellAmount = "";
            ViewModel.PlaceOrderResponse = response;

            return RedirectToAction("Trade", "ManualTrading", new { ViewModel = ViewModel });
        }
    }
}