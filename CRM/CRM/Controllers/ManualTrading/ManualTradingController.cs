using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ManualTradingModel ViewModel = new ManualTradingModel();

            ViewModel.Account = "First";
            ViewModel.Coin = "BTC";
            ViewModel.Unit = new Unit
            {
                CountOfUnits5m = 5,
                CountOfUnits15m = 14,
                CountOfUnits30m = 27,
                CountOfUnits1h = 29,
                CountOfUnits3h = 78
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";


            ViewModel.Unit.PercentOfUnits5m = Math.Round(ViewModel.Unit.CountOfUnits5m / 5 * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits15m = Math.Round(ViewModel.Unit.CountOfUnits15m / 15 * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits30m = Math.Round(ViewModel.Unit.CountOfUnits30m / 30 * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits1h = Math.Round(ViewModel.Unit.CountOfUnits1h / 60 * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits3h = Math.Round(ViewModel.Unit.CountOfUnits3h / 180 * 100, 0).ToString(SeparateHelper.Separator);

            Random random = new Random();

            DateTime time = DateTime.Now;

            ViewModel.Units = new List<Unit>();
            ViewModel.CoinPrices = new List<string>();

            var mid = 30;

            for (int i = 0; i < 60; i++)
            {
                if (mid > 100)
                    mid = 100;

                ViewModel.Units.Add(new Unit
                {
                    PercentOfUnits5m = random.Next(mid - 5, mid + 5).ToString(),
                    PercentOfUnits15m = random.Next(mid - 5, mid + 5).ToString(),
                    PercentOfUnits30m = random.Next(mid - 5, mid + 5).ToString(),
                    PercentOfUnits1h = random.Next(mid - 5, mid + 5).ToString(),
                    PercentOfUnits3h = random.Next(mid - 5, mid + 5).ToString(),
                    Time = time.AddMinutes(-(60-i)).ToString("HH:mm"),
                }) ;

                mid += 1;

                if (random.Next(0, 100) > 95)
                    mid += random.Next(8, 12);

                var price = 6800;

                ViewModel.CoinPrices.Add(random.Next(price - 10, price + 10).ToString());

                var chance = random.Next(0, 100);

                price += random.Next(300, 1000);

                if (chance > 98)
                {
                    price += random.Next(30, 60);
                }
                else if (chance < 2)
                {
                    price -= random.Next(30, 60);
                }

                

            }



            return View(ViewModel);
        }
    }
}