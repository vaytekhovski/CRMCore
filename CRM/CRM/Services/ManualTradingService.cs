using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Models.DataVisioAPI;
using Business.Models.Master;
using CRM.Services.Balances;
using CRM.ViewModels.ManualTrading;
using Newtonsoft.Json;

namespace CRM.Services
{
    public class ManualTradingService
    {
        private readonly BalancesService balancesService;

        public ManualTradingService()
        {

            balancesService = new BalancesService();
        }

        public async Task<ManualTradingModel> Load(ManualTradingModel ViewModel)
        {

            List<NeuralSignal> NeuralSignals = new List<NeuralSignal>();
            List<decimal> CoinPrices = new List<decimal>();

            var now = DateTime.Now.ToUniversalTime();

            /*
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

            var CountOf5m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-5)).Count();
            var CountOf15m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-15)).Count();
            var CountOf30m = NeuralSignals.Where(x => x.Time > now.AddMinutes(-30)).Count();
            var CountOf1h = NeuralSignals.Where(x => x.Time > now.AddHours(-1)).Count();
            var CountOf3h = NeuralSignals.Where(x => x.Time > now.AddHours(-3)).Count();


            ViewModel.Unit.PercentOfUnits5m = Math.Round((ViewModel.Unit.CountOfUnits5m / CountOf5m) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits15m = Math.Round((ViewModel.Unit.CountOfUnits15m / CountOf15m) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits30m = Math.Round((ViewModel.Unit.CountOfUnits30m / CountOf30m) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits1h = Math.Round((ViewModel.Unit.CountOfUnits1h / CountOf1h) * 100, 0).ToString(SeparateHelper.Separator);
            ViewModel.Unit.PercentOfUnits3h = Math.Round((ViewModel.Unit.CountOfUnits3h / CountOf3h) * 100, 0).ToString(SeparateHelper.Separator);

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

                CountOf5m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-5)).Where(x => x.Time < start).Count();
                CountOf15m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-15)).Where(x => x.Time < start).Count();
                CountOf30m = NeuralSignals.Where(x => x.Time > start.AddMinutes(-30)).Where(x => x.Time < start).Count();
                CountOf1h = NeuralSignals.Where(x => x.Time > start.AddHours(-1)).Where(x => x.Time < start).Count();
                CountOf3h = NeuralSignals.Where(x => x.Time > start.AddHours(-3)).Where(x => x.Time < start).Count();

                Unit.PercentOfUnits5m = Math.Round(Unit.CountOfUnits5m / CountOf5m * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits15m = Math.Round(Unit.CountOfUnits15m / CountOf15m * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits30m = Math.Round(Unit.CountOfUnits30m / CountOf30m * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits1h = Math.Round(Unit.CountOfUnits1h / CountOf1h * 100, 0).ToString(SeparateHelper.Separator);
                Unit.PercentOfUnits3h = Math.Round(Unit.CountOfUnits3h / CountOf3h * 100, 0).ToString(SeparateHelper.Separator);

                Unit.Time = start.ToString("HH:mm");

                ViewModel.Units.Add(Unit);
            }


            foreach (var item in CoinPrices)
            {
                ViewModel.CoinPrices.Add(item.ToString());

            }

            */

            

            ViewModel.balancesModel = await balancesService.LoadBalancesAsync(ViewModel.Account);

            

            return ViewModel;
        }


    }
}
