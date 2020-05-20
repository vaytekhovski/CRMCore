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

            for (var start = ViewModel.StartDate.AddHours(-3); start <= ViewModel.EndDate.AddHours(-3); start = start.AddMinutes(1))
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

                Unit.Time = start.AddHours(3);

                ViewModel.Units.Add(Unit);
            }

            if(ViewModel.TimeRange != 1)
            {
                DateTime[] times;
                List<DateTime> Times = new List<DateTime>();
                Times.Add(ViewModel.Units.FirstOrDefault().Time);
                for (DateTime i = ViewModel.Units.Where(x=>x.Time > Times.FirstOrDefault()).FirstOrDefault(x =>x.Time.Minute % ViewModel.TimeRange == 0).Time; i <= ViewModel.Units.LastOrDefault().Time.AddMinutes(ViewModel.TimeRange); i = i.AddMinutes(ViewModel.TimeRange))
                {
                    Times.Add(i);
                }

                times = Times.ToArray();

                var BufUnits = new List<Unit>();
                int step = 0;
                int timeStep = 0;
                Unit[] BufArr;
                double[] BufArr5m;
                double[] BufArr15m;
                double[] BufArr30m;
                double[] BufArr1h;
                double[] BufArr3h;

                int lenght = ViewModel.Units.Count - 1;
                do
                {
                    BufArr = ViewModel.Units
                        .OrderBy(x=>x.Time)
                        .Where(x=>x.Time > times[timeStep])
                        .Where(x=>x.Time <= times[timeStep + 1])
                        .ToArray();


                    lenght -= BufArr.Length;

                    BufArr5m = BufArr.OrderBy(x => x.PercentOfUnits5m).Select(x => Convert.ToDouble(x.PercentOfUnits5m)).ToArray();
                    BufArr15m = BufArr.OrderBy(x => x.PercentOfUnits5m).Select(x => Convert.ToDouble(x.PercentOfUnits5m)).ToArray();
                    BufArr30m = BufArr.OrderBy(x => x.PercentOfUnits5m).Select(x => Convert.ToDouble(x.PercentOfUnits5m)).ToArray();
                    BufArr1h = BufArr.OrderBy(x => x.PercentOfUnits5m).Select(x => Convert.ToDouble(x.PercentOfUnits5m)).ToArray();
                    BufArr3h = BufArr.OrderBy(x => x.PercentOfUnits5m).Select(x => Convert.ToDouble(x.PercentOfUnits5m)).ToArray();

                    BufUnits.Add(new Unit
                    {
                        Time = BufArr.LastOrDefault().Time,
                        PercentOfUnits5m = BufArr5m[BufArr5m.Length / 2].ToString(),
                        PercentOfUnits15m = BufArr5m[BufArr15m.Length / 2].ToString(),
                        PercentOfUnits30m = BufArr5m[BufArr30m.Length / 2].ToString(),
                        PercentOfUnits1h = BufArr5m[BufArr1h.Length / 2].ToString(),
                        PercentOfUnits3h = BufArr5m[BufArr3h.Length / 2].ToString()
                    }) ;

                    step++;
                    timeStep++;

                } while (lenght != 0);

                ViewModel.Units.Clear();
                ViewModel.Units.AddRange(BufUnits);
            }


           var candles = datavisioAPI.GetCandles( httpContext,ViewModel.Coin).Result.ToList();


            foreach (var item in candles)
            {
                ViewModel.CoinPrices.Add(item.c);
            }

            ViewModel.balancesModel = await balancesService.LoadBalancesAsync(httpContext);


            ViewModel.Graphs = datavisioAPI.GetGraphs(ViewModel.Coin, ViewModel.StartDate.AddHours(-3), ViewModel.EndDate.AddHours(-3)).Result;

            foreach (var item in ViewModel.Graphs)
            {
                item.rsi /= 100;
                item.reg /= 100;
                item.Time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(item.time).AddHours(3);
                item.Time = item.Time.AddSeconds(-item.Time.Second);
            }

            ViewModel.Graphs = ViewModel.Graphs.Where(x => x.Time >= ViewModel.StartDate.AddHours(-3)).Where(x => x.Time <= ViewModel.EndDate.AddHours(-3)).ToList();

            if(ViewModel.TimeRange != 1)
            {

                DateTime[] timesG;
                List<DateTime> Times = new List<DateTime>();
                Times.Add(ViewModel.Graphs.FirstOrDefault().Time);
                for (DateTime i = ViewModel.Graphs.Where(x => x.Time > Times.FirstOrDefault()).FirstOrDefault(x => x.Time.Minute % ViewModel.TimeRange == 0).Time; i <= ViewModel.Graphs.LastOrDefault().Time.AddMinutes(ViewModel.TimeRange); i = i.AddMinutes(ViewModel.TimeRange))
                {
                    Times.Add(i);
                }

                timesG = Times.ToArray();

                var BufGraphs = new List<Graph>();
                int step = 0;
                int timeStep = 0;

                int lenght = ViewModel.Graphs.Count - 1;
                Graph[] BufArr;

                decimal[] rsi;
                decimal[] reg;
                decimal[] lir;

                do
                {
                    BufArr = ViewModel.Graphs
                        .OrderBy(x => x.Time)
                        .Where(x => x.Time > timesG[timeStep])
                        .Where(x => x.Time <= timesG[timeStep + 1])
                        .ToArray();

                    rsi = BufArr.OrderBy(x => x.rsi).Select(x => x.rsi).ToArray();
                    reg = BufArr.OrderBy(x => x.reg).Select(x => x.reg).ToArray();
                    lir = BufArr.OrderBy(x => x.lir).Select(x => x.lir).ToArray();

                    lenght -= BufArr.Length;

                    BufGraphs.Add(new Graph
                    {
                        rsi = rsi[rsi.Length / 2],
                        reg = reg[reg.Length / 2],
                        lir = lir[lir.Length / 2],
                        Time = BufArr.LastOrDefault().Time,
                    });

                    step++;
                    timeStep++;
                } while (lenght != 0);

                ViewModel.Graphs.Clear();
                ViewModel.Graphs.AddRange(BufGraphs);
            }



            

            //ViewModel.Graphs = ViewModel.Graphs.Where(x => x.Time > ViewModel.StartDate && x.Time < ViewModel.EndDate).ToList();

            return ViewModel;
        }


    }
}
