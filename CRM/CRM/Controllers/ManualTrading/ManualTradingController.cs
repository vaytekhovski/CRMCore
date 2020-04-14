using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Contexts;
using Business.Models.Master;
using CRM.Helpers;
using CRM.ViewModels.ManualTrading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.ManualTrading
{
    public class ManualTradingController : Controller
    {
        public IActionResult Trade()
        {
            SeparateHelper.Separator.NumberDecimalSeparator = ".";


            ManualTradingModel ViewModel = new ManualTradingModel();

            ViewModel.Account = "First";
            ViewModel.Coin = "BTC";
            ViewModel.Unit = new Unit();
            ViewModel.Units = new List<Unit>();
            ViewModel.CoinPrices = new List<string>();


            List<NeuralSignal> NeuralSignals = new List<NeuralSignal>();

            var now = DateTime.Now.ToUniversalTime();


            using (MySQLContext db = new MySQLContext())
            {
                NeuralSignals = db.NeuralSignals.Where(x=>x.Base =="BTC").Where(x => x.Time > now.AddHours(-4)).ToList();
            }

            ViewModel.Unit.CountOfUnits5m = NeuralSignals.Where(x=>x.Time > now.AddMinutes(-5)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits15m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-15)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits30m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-30)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits1h = NeuralSignals.Where(x => x.Time > now.AddHours(-1)).Where(x => x.Value == 1).Count();
            ViewModel.Unit.CountOfUnits3h = NeuralSignals.Where(x => x.Time > now.AddHours(-3)).Where(x => x.Value == 1).Count();

            ViewModel.Unit.PercentOfUnits5m = Math.Round(ViewModel.Unit.CountOfUnits5m / 5 * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits15m = Math.Round(ViewModel.Unit.CountOfUnits15m / 15 * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits30m = Math.Round(ViewModel.Unit.CountOfUnits30m / 30 * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits1h = Math.Round(ViewModel.Unit.CountOfUnits1h / 60 * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits3h = Math.Round(ViewModel.Unit.CountOfUnits3h / 180 * 100, 0).ToString(SeparateHelper.Separator);


            for (var start = now.AddHours(-1); start < now;start = start.AddMinutes(1))
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

            return View(ViewModel);
        }
    }
}