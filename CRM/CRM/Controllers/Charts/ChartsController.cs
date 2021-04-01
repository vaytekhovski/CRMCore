using Business;
using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.ViewModels.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using System.Text;
using ClosedXML.Excel;
using System.IO;
using System.Threading.Tasks;

namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly TradeHistoryService _tradeHistoryService;
        private readonly DatavisioAPIService datavisioAPIService;

        public ChartsController()
        {
            //var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();
            _tradeHistoryService = new TradeHistoryService();

            datavisioAPIService = new DatavisioAPIService();
        }

        [HttpGet]
        public ActionResult Charts()
        {
            var model = new ProfitViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(model);
        }

        
        [HttpGet]
        public ActionResult ProfitChart()
        {
            var viewModel = new ProfitViewModel
            {
                PageName = "Profit",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProfitChart(ProfitViewModel ViewModel)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                Coin = ViewModel.Coin,
                Account = ViewModel.Account,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddDays(1),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = await _tradeHistoryService.LoadAsync(filter, HttpContext);

            ViewModel.Dates = model.Deals.deals.Select(x => x.closed.ToJavascriptTicks()).ToList();
            ViewModel.Values = model.Deals.deals.Select(x => x.profit.clean.amount.ToString(SeparateHelper.Separator)).ToList();

            ViewModel.CountOfZero = model.Deals.deals.Where(x => x.profit.clean.amount == 0).Count();
            ViewModel.CountOfMore = model.Deals.deals.Where(x => x.profit.clean.amount > 0).Count();
            ViewModel.CountOfLess = model.Deals.deals.Where(x => x.profit.clean.amount < 0).Count();

            ViewModel.VolumeOfMore = model.ProfitOrdersSumm.ToString(SeparateHelper.Separator);
            ViewModel.VolumeOfLess = (model.LossOrdersSumm * -1).ToString(SeparateHelper.Separator);

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Profit";
            return View(ViewModel);
        }

        

        [HttpGet]
        public IActionResult Signals()
        {
            SignalsModel model = new SignalsModel() {
                //StartDate = DateTime.Parse(DatesHelper.CurrentDateTimeStr).AddDays(-1).ToString("yyyy-MM-ddTHH:mm"),
                StartDate = DateTime.UtcNow.AddDays(-5).ToString("yyyy-MM-ddTHH:mm"),
            //EndDate = DatesHelper.CurrentDateTimeStr
            EndDate = DateTime.UtcNow.AddDays(-4).ToString("yyyy-MM-ddTHH:mm"),
            };

            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
                Coin = "BTC"
            };


            var token = HttpContext.User.Identity.Name;

            var gradSignals = datavisioAPIService.GetSignals(token, filter.Coin, "grad").Result;

            var logtwoSignals = datavisioAPIService.GetSignals(token, filter.Coin, "logtwo").Result;

            var gradEMA = datavisioAPIService.GetGraphs(token, filter.Coin, DateTime.Parse(model.StartDate).AddHours(-3), DateTime.Parse(model.EndDate).AddHours(-3), "grad").Result;
            var logtwoEMA = datavisioAPIService.GetGraphs(token, filter.Coin, DateTime.Parse(model.StartDate).AddHours(-3), DateTime.Parse(model.EndDate).AddHours(-3), "logtwo").Result;

            //var FirstDate = gradSignals.signals.Last().time > logtwoSignals.signals.Last().time ? gradSignals.signals.Last().time : logtwoSignals.signals.Last().time;

            //model.StartDate = FirstDate.AddHours(3).ToString("yyyy-MM-ddTHH:mm");

            

            gradSignals.signals = gradSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            logtwoSignals.signals = logtwoSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            model.gradDates = gradSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            model.logtwoDates = logtwoSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            int i = 0;
            foreach (var signal in gradSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);
                model.signals.Add(new gradlogtwoSignals
                {
                    Id = i,
                    Date = signal.time,
                    gradProba = signal.proba
                });
                i++;
            }

            model.BBL = gradSignals.signals.Select(x => x.indicators).Select(x => x.bbl_proba.ToString(SeparateHelper.Separator)).ToList();
            model.BBM = gradSignals.signals.Select(x => x.indicators).Select(x => x.ma_proba.ToString(SeparateHelper.Separator)).ToList();
            model.BBU = gradSignals.signals.Select(x => x.indicators).Select(x => x.bbu_proba.ToString(SeparateHelper.Separator)).ToList();

            foreach (var signal in logtwoSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);

                var sign = model.signals.FirstOrDefault(x => x.Date == signal.time);
                if (sign != null)
                {
                    sign.logtwoProba = signal.proba;
                }
                else
                {
                    model.signals.Add(new gradlogtwoSignals
                    {
                        Id = i,
                        Date = signal.time,
                        logtwoProba = signal.proba
                    });
                    i++;
                }
            }


            model.gradValues = gradSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.logtwoValues = logtwoSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.gradEMA = gradEMA.Select(x => x.ema.ToString(SeparateHelper.Separator)).ToList();
            model.logtwoEMA = logtwoEMA.Select(x => x.ema.ToString(SeparateHelper.Separator)).ToList(); 
            model.PageName = "Signals";
            ViewBag.Coins = DropDownFields.GetCoins();

            return View(model);
        }

        [HttpPost]
        public IActionResult Signals(SignalsModel model)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
                Coin = model.Coin
            };

            var token = HttpContext.User.Identity.Name;
            var gradSignals = datavisioAPIService.GetSignals(token, filter.Coin, "grad").Result;
            var logtwoSignals = datavisioAPIService.GetSignals(token, filter.Coin, "logtwo").Result;


            var gradEMA = new List<Graph>();
            var logtwoEMA = new List<Graph>();


            try
            {
                gradEMA = datavisioAPIService.GetGraphs(token, filter.Coin, DateTime.Parse(model.StartDate).AddHours(-3), DateTime.Parse(model.EndDate).AddHours(-3), "grad").Result;

            }
            catch {
            }

            try
            {
                logtwoEMA = datavisioAPIService.GetGraphs(token, filter.Coin, DateTime.Parse(model.StartDate).AddHours(-3), DateTime.Parse(model.EndDate).AddHours(-3), "logtwo").Result;
            }
            catch {
            }



            gradSignals.signals = gradSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            logtwoSignals.signals = logtwoSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();



            model.gradDates = gradSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            model.logtwoDates = logtwoSignals.signals.Select(x => x.time.AddHours(3)).ToList();

            foreach (var signal in gradSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);
                model.signals.Add(new gradlogtwoSignals
                {
                    Date = signal.time,
                    gradProba = signal.proba
                });

            }

            model.BBL = gradSignals.signals.Select(x => x.indicators).Select(x => x.bbl_proba.ToString(SeparateHelper.Separator)).ToList();
            model.BBM = gradSignals.signals.Select(x => x.indicators).Select(x => x.ma_proba.ToString(SeparateHelper.Separator)).ToList();
            model.BBU = gradSignals.signals.Select(x => x.indicators).Select(x => x.bbu_proba.ToString(SeparateHelper.Separator)).ToList();

            foreach (var signal in logtwoSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);

                var sign = model.signals.FirstOrDefault(x => x.Date == signal.time);
                if(sign != null)
                {
                    sign.logtwoProba = signal.proba;
                }
                else
                {
                    model.signals.Add(new gradlogtwoSignals
                    {
                        Date = signal.time,
                        logtwoProba = signal.proba
                    });
                }
            }

            

            model.gradValues = gradSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.logtwoValues = logtwoSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.gradEMA = gradEMA.Select(x => x.ema.ToString(SeparateHelper.Separator)).ToList();
            model.logtwoEMA = logtwoEMA.Select(x => x.ema.ToString(SeparateHelper.Separator)).ToList();
            model.PageName = "Signals";
            ViewBag.Coins = DropDownFields.GetCoins();

            return View(model);
        }


        public IActionResult ExportToCSV(DateTime start, DateTime end)
        {
            SignalsModel model = new SignalsModel()
            {
                StartDate = start.ToString(),
                EndDate = end.ToString()
            };

            var token = HttpContext.User.Identity.Name;
            var gradSignals = datavisioAPIService.GetSignals(token, "BTC", "grad").Result;
            var logtwoSignals = datavisioAPIService.GetSignals(token, "BTC", "logtwo").Result;

            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
            };

            gradSignals.signals = gradSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            logtwoSignals.signals = logtwoSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();



            model.gradDates = gradSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            model.logtwoDates = logtwoSignals.signals.Select(x => x.time.AddHours(3)).ToList();

            foreach (var signal in gradSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);
                model.signals.Add(new gradlogtwoSignals
                {
                    Date = signal.time,
                    gradProba = signal.proba
                });
            }

            foreach (var signal in logtwoSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);

                var sign = model.signals.FirstOrDefault(x => x.Date == signal.time);
                if (sign != null)
                {
                    sign.logtwoProba = signal.proba;
                }
                else
                {
                    model.signals.Add(new gradlogtwoSignals
                    {
                        Date = signal.time,
                        logtwoProba = signal.proba
                    });
                }
            }


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("gradlogtwoSignals" + DateTime.Now.ToJavascriptTicks());
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Date";
                worksheet.Cell(currentRow, 2).Value = "grad Proba";
                worksheet.Cell(currentRow, 3).Value = "logtwo Proba";
                
                foreach (var signal in model.signals)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = signal.Date;
                    worksheet.Cell(currentRow, 2).Value = signal.gradProba;
                    worksheet.Cell(currentRow, 3).Value = signal.logtwoProba;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "gradlogtwoSignals" + DateTime.Now.ToJavascriptTicks() + ".xlsx");
                }
            }
        }


    }
}