using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using Business.Models.Master;
using CRM.Helpers;
using CRM.Services.Balances;
using CRM.ViewModels.ManualTrading;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CRM.Services
{
    public class ManualTradingService
    {
        private readonly DatavisioAPIService datavisioAPI;
        private readonly BalancesService balancesService;

        public ManualTradingService()
        {
            datavisioAPI = new DatavisioAPIService();
            balancesService = new BalancesService();
        }

        public async Task<ManualTradingModel> Load(ManualTradingModel ViewModel, HttpContext httpContext)
        {
            var now = DateTime.UtcNow;

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            Signals signals = datavisioAPI.GetSignals(httpContext, ViewModel.Coin).Result;

            ViewModel.Unit.CountOfUnits5m = signals.signals.Where(x => x.time > now.AddMinutes(-5)).Where(x => x.value == 1).Count();
            ViewModel.Unit.CountOfUnits15m = signals.signals.Where(x => x.time > now.AddMinutes(-15)).Where(x => x.value == 1).Count();
            ViewModel.Unit.CountOfUnits30m = signals.signals.Where(x => x.time > now.AddMinutes(-30)).Where(x => x.value == 1).Count();
            ViewModel.Unit.CountOfUnits1h = signals.signals.Where(x => x.time > now.AddHours(-1)).Where(x => x.value == 1).Count();
            ViewModel.Unit.CountOfUnits3h = signals.signals.Where(x => x.time > now.AddHours(-3)).Where(x => x.value == 1).Count();

            var CountOf5m = signals.signals.Where(x => x.time > now.AddMinutes(-5)).Count();
            var CountOf15m = signals.signals.Where(x => x.time > now.AddMinutes(-15)).Count();
            var CountOf30m = signals.signals.Where(x => x.time > now.AddMinutes(-30)).Count();
            var CountOf1h = signals.signals.Where(x => x.time > now.AddHours(-1)).Count();
            var CountOf3h = signals.signals.Where(x => x.time > now.AddHours(-3)).Count();


            ViewModel.Unit.PercentOfUnits5m = Math.Round((ViewModel.Unit.CountOfUnits5m / CountOf5m) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits15m = Math.Round((ViewModel.Unit.CountOfUnits15m / CountOf15m) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits30m = Math.Round((ViewModel.Unit.CountOfUnits30m / CountOf30m) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits1h = Math.Round((ViewModel.Unit.CountOfUnits1h / CountOf1h) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits3h = Math.Round((ViewModel.Unit.CountOfUnits3h / CountOf3h) * 100, 0).ToString(SeparateHelper.Separator);

            for (var start = now.AddHours(-3); start <= now; start = start.AddMinutes(1))
            {
                ViewModels.ManualTrading.Unit Unit = new Unit
                {
                    CountOfUnits5m = signals.signals.Where(x => x.time > start.AddMinutes(-5)).Where(x => x.time < start).Where(x => x.value == 1).Count(),
                    CountOfUnits15m = signals.signals.Where(x => x.time > start.AddMinutes(-15)).Where(x => x.time < start).Where(x => x.value == 1).Count(),
                    CountOfUnits30m = signals.signals.Where(x => x.time > start.AddMinutes(-30)).Where(x => x.time < start).Where(x => x.value == 1).Count(),
                    CountOfUnits1h = signals.signals.Where(x => x.time > start.AddHours(-1)).Where(x => x.time < start).Where(x => x.value == 1).Count(),
                    CountOfUnits3h = signals.signals.Where(x => x.time > start.AddHours(-3)).Where(x => x.time < start).Where(x => x.value == 1).Count()
                };

                CountOf5m = signals.signals.Where(x => x.time > start.AddMinutes(-5)).Where(x => x.time < start).Count();
                CountOf15m = signals.signals.Where(x => x.time > start.AddMinutes(-15)).Where(x => x.time < start).Count();
                CountOf30m = signals.signals.Where(x => x.time > start.AddMinutes(-30)).Where(x => x.time < start).Count();
                CountOf1h = signals.signals.Where(x => x.time > start.AddHours(-1)).Where(x => x.time < start).Count();
                CountOf3h = signals.signals.Where(x => x.time > start.AddHours(-3)).Where(x => x.time < start).Count();

                Unit.PercentOfUnits5m = Math.Round(Unit.CountOfUnits5m / CountOf5m * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits15m = Math.Round(Unit.CountOfUnits15m / CountOf15m * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits30m = Math.Round(Unit.CountOfUnits30m / CountOf30m * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits1h = Math.Round(Unit.CountOfUnits1h / CountOf1h * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits3h = Math.Round(Unit.CountOfUnits3h / CountOf3h * 100, 0).ToString(SeparateHelper.Separator);

                Unit.Time = start.ToString("HH:mm");

                ViewModel.Units.Add(Unit);
            }


            var candles = datavisioAPI.GetCandles( httpContext,ViewModel.Coin).Result.ToList();


            foreach (var item in candles)
            {
                ViewModel.CoinPrices.Add(item.c);
            }

            ViewModel.balancesModel = await balancesService.LoadBalancesAsync(httpContext);


            ViewModel.Graphs = datavisioAPI.GetGraphs(ViewModel.Coin, ViewModel.StartDate, ViewModel.EndDate).Result;

            foreach (var item in ViewModel.Graphs)
            {
                item.rsi /= 100;
                item.Time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(item.time);
                item.time_str = item.Time.ToString();
            }

            ViewModel.Graphs = ViewModel.Graphs.Where(x => x.Time > ViewModel.StartDate && x.Time < ViewModel.EndDate).ToList();

            return ViewModel;
        }


    }
}
