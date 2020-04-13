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

            

            ViewModel.Units = new List<Unit>();

            return View(ViewModel);
        }
    }
}