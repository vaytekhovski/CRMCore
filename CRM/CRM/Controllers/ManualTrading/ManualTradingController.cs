using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.Contexts;
using Business.Models.Master;
using CRM.Helpers;
using CRM.Services.Balances;
using CRM.ViewModels.ManualTrading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.ManualTrading
{
    public class ManualTradingController : Controller
    {
        private readonly BalancesService _balancesService;


        public ManualTradingController()
        {
            _balancesService = new BalancesService();
        }

        [HttpGet]
        public async Task<ActionResult> Trade()
        {
            SeparateHelper.Separator.NumberDecimalSeparator = ".";


            ManualTradingModel ViewModel = new ManualTradingModel();

            ViewModel.Account = "556c8663-5706-4112-9440-c6ac965cfa26";
            ViewModel.Coin = "ETH";


            List<NeuralSignal> NeuralSignals = new List<NeuralSignal>();
            List<decimal> CoinPrices = new List<decimal>();

            var now = DateTime.Now.ToUniversalTime();


            using (MySQLContext db = new MySQLContext())
            {
                NeuralSignals = db.NeuralSignals.Where(x => x.Base == ViewModel.Coin).Where(x => x.Time > now.AddHours(-4)).ToList();
                CoinPrices = db.ChartPoints.Where(x => x.Base == ViewModel.Coin).Where(x => x.Time > now.AddHours(-1)).OrderBy(x => x.Time).Select(x => x.Close).ToList();
            }

            ViewModel.Unit.CountOfUnits5m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-5)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits15m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-15)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits30m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-30)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits1h = NeuralSignals.Where(x => x.Time > now.AddHours(-1)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits3h = NeuralSignals.Where(x => x.Time > now.AddHours(-3)).Where(x => x.Value == 1).Count();

            ViewModel.Unit.PercentOfUnits5m = Math.Round((ViewModel.Unit.CountOfUnits5m / 5) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits15m = Math.Round((ViewModel.Unit.CountOfUnits15m / 15) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits30m = Math.Round((ViewModel.Unit.CountOfUnits30m / 30) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits1h = Math.Round((ViewModel.Unit.CountOfUnits1h / 60) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits3h = Math.Round((ViewModel.Unit.CountOfUnits3h / 180) * 100, 0).ToString(SeparateHelper.Separator);


            for (var start = now.AddHours(-1); start <= now; start = start.AddMinutes(1))
            {
                ViewModels.ManualTrading.Unit Unit = new Unit
                {
                    CountOfUnits5m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-5)).Where(x => x.Time < start).Where(x => x.Value == 1).Count(),
                    CountOfUnits15m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-15)).Where(x => x.Time < start).Where(x => x.Value == 1).Count(),
                    CountOfUnits30m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-30)).Where(x => x.Time < start).Where(x => x.Value == 1).Count(),
                    CountOfUnits1h = NeuralSignals.Where(x => x.Time > start.AddHours(-1)).Where(x => x.Time < start).Where(x => x.Value == 1).Count(),
                    CountOfUnits3h = NeuralSignals.Where(x => x.Time > start.AddHours(-3)).Where(x => x.Time < start).Where(x => x.Value == 1).Count()
                };

                Unit.PercentOfUnits5m = Math.Round(Unit.CountOfUnits5m / 5 * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits15m = Math.Round(Unit.CountOfUnits15m / 15 * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits30m = Math.Round(Unit.CountOfUnits30m / 30 * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits1h = Math.Round(Unit.CountOfUnits1h / 60 * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits3h = Math.Round(Unit.CountOfUnits3h / 180 * 100, 0).ToString(SeparateHelper.Separator);

                Unit.Time = start.ToString("HH:mm");

                ViewModel.Units.Add(Unit);
            }


            foreach (var item in CoinPrices)
            {
                ViewModel.CoinPrices.Add(item.ToString());

            }

            if (CoinPrices.Count != 0)
            {
                ViewBag.MinCourse = Convert.ToInt32(CoinPrices.Min());
                ViewBag.MaxCourse = Convert.ToInt32(CoinPrices.Max());
            }
            else
            {
                ViewBag.MinCourse = 100;
                ViewBag.MaxCourse = 50;
            }

            ViewModel.balancesModel = await _balancesService.LoadBalancesAsync(ViewModel.Account);



            ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Trade(ManualTradingModel ViewModel)
        {
            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            List<NeuralSignal> NeuralSignals = new List<NeuralSignal>();
            List<decimal> CoinPrices = new List<decimal>();

            var now = DateTime.Now.ToUniversalTime();


            using (MySQLContext db = new MySQLContext())
            {
                NeuralSignals = db.NeuralSignals.Where(x => x.Base == ViewModel.Coin).Where(x => x.Time > now.AddHours(-4)).ToList();
                CoinPrices = db.ChartPoints.Where(x => x.Base == ViewModel.Coin).Where(x => x.Time > now.AddHours(-1)).OrderBy(x => x.Time).Select(x => x.Close).ToList();
            }

            ViewModel.Unit.CountOfUnits5m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-5)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits15m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-15)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits30m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-30)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits1h = NeuralSignals.Where(x => x.Time > now.AddHours(-1)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits3h = NeuralSignals.Where(x => x.Time > now.AddHours(-3)).Where(x => x.Value == 1).Count();

            ViewModel.Unit.PercentOfUnits5m = Math.Round((ViewModel.Unit.CountOfUnits5m / 5) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits15m = Math.Round((ViewModel.Unit.CountOfUnits15m / 15) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits30m = Math.Round((ViewModel.Unit.CountOfUnits30m / 30) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits1h = Math.Round((ViewModel.Unit.CountOfUnits1h / 60) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits3h = Math.Round((ViewModel.Unit.CountOfUnits3h / 180) * 100, 0).ToString(SeparateHelper.Separator);

            for (var start = now.AddHours(-1); start <= now; start = start.AddMinutes(1))
            {
                ViewModels.ManualTrading.Unit Unit = new Unit
                {
                    CountOfUnits5m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-5)).Where(x => x.Time < start).Where(x => x.Value == 1).Count(),
                    CountOfUnits15m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-15)).Where(x => x.Time < start).Where(x => x.Value == 1).Count(),
                    CountOfUnits30m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-30)).Where(x => x.Time < start).Where(x => x.Value == 1).Count(),
                    CountOfUnits1h = NeuralSignals.Where(x => x.Time > start.AddHours(-1)).Where(x => x.Time < start).Where(x => x.Value == 1).Count(),
                    CountOfUnits3h = NeuralSignals.Where(x => x.Time > start.AddHours(-3)).Where(x => x.Time < start).Where(x => x.Value == 1).Count()
                };

                Unit.PercentOfUnits5m = Math.Round(Unit.CountOfUnits5m / 5 * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits15m = Math.Round(Unit.CountOfUnits15m / 15 * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits30m = Math.Round(Unit.CountOfUnits30m / 30 * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits1h = Math.Round(Unit.CountOfUnits1h / 60 * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits3h = Math.Round(Unit.CountOfUnits3h / 180 * 100, 0).ToString(SeparateHelper.Separator);

                Unit.Time = start.ToString("HH:mm");

                ViewModel.Units.Add(Unit);
            }

            foreach (var item in CoinPrices)
            {
                ViewModel.CoinPrices.Add(item.ToString());

            }

            if (CoinPrices.Count != 0)
            {
                ViewBag.MinCourse = Convert.ToInt32(CoinPrices.Min());
                ViewBag.MaxCourse = Convert.ToInt32(CoinPrices.Max());
            }
            else
            {
                ViewBag.MinCourse = 100;
                ViewBag.MaxCourse = 50;
            }

            ViewModel.balancesModel = await _balancesService.LoadBalancesAsync(ViewModel.Account);



            ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(ViewModel);
        }
    }
}