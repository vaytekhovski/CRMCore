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

namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly TradeHistoryService _tradeHistoryService;
        private readonly DatavisioAPIService datavisioAPIService;

        public ChartsController()
        {
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
        public ActionResult ProfitChart(ProfitViewModel ViewModel)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                Coin = ViewModel.Coin,
                Account = ViewModel.Account,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddDays(1),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _tradeHistoryService.LoadDataToChart(filter, HttpContext);

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
        public ActionResult OrdersOnTimeHistory()
        {
            var viewModel = new OrdersOnTimeHistoryViewModel
            {
                PageName = "OrdersOnTimeHistory",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult OrdersOnTimeHistory(OrdersOnTimeHistoryViewModel ViewModel)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                Coin = ViewModel.Coin,
                Account = ViewModel.Account,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddDays(1),
            };
            

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Orders On TimeHistory";


            return View(ViewModel);
        }

        [HttpGet]
        public IActionResult Signals()
        {
            SignalsModel model = new SignalsModel() {
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr
            };
            

            var token = HttpContext.User.Identity.Name;

            var RaiseSignals = datavisioAPIService.GetSignals(token, "BTC", "raise").Result;

            var FallSignals = datavisioAPIService.GetSignals(token, "BTC", "fall").Result;

            var FirstDate = RaiseSignals.signals.Last().time > FallSignals.signals.Last().time ? RaiseSignals.signals.Last().time : FallSignals.signals.Last().time;

            model.StartDate = FirstDate.AddHours(3).ToString("yyyy-MM-ddTHH:mm");

            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
            };

            RaiseSignals.signals = RaiseSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            FallSignals.signals = FallSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            model.RaiseDates = RaiseSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            model.FallDates = FallSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            int i = 0;
            foreach (var signal in RaiseSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);
                model.signals.Add(new RaiseFallSignals
                {
                    Id = i,
                    Date = signal.time,
                    RaiseProba = signal.proba
                });
                i++;
            }

            foreach (var signal in FallSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);

                var sign = model.signals.FirstOrDefault(x => x.Date == signal.time);
                if (sign != null)
                {
                    sign.FallProba = signal.proba;
                }
                else
                {
                    model.signals.Add(new RaiseFallSignals
                    {
                        Id = i,
                        Date = signal.time,
                        FallProba = signal.proba
                    });
                    i++;
                }
            }


            model.RaiseValues = RaiseSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.FallValues = FallSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.PageName = "Signals";
            return View(model);
        }

        [HttpPost]
        public IActionResult Signals(SignalsModel model)
        {

            var token = HttpContext.User.Identity.Name;
            var RaiseSignals = datavisioAPIService.GetSignals(token, "BTC", "raise").Result;
            var FallSignals = datavisioAPIService.GetSignals(token, "BTC", "fall").Result;

            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
            }; 
            
            RaiseSignals.signals = RaiseSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            FallSignals.signals = FallSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();



            model.RaiseDates = RaiseSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            model.FallDates = FallSignals.signals.Select(x => x.time.AddHours(3)).ToList();

            foreach (var signal in RaiseSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);
                model.signals.Add(new RaiseFallSignals
                {
                    Date = signal.time,
                    RaiseProba = signal.proba
                });
            }

            foreach (var signal in FallSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);

                var sign = model.signals.FirstOrDefault(x => x.Date == signal.time);
                if(sign != null)
                {
                    sign.FallProba = signal.proba;
                }
                else
                {
                    model.signals.Add(new RaiseFallSignals
                    {
                        Date = signal.time,
                        FallProba = signal.proba
                    });
                }
            }

            

            model.RaiseValues = RaiseSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.FallValues = FallSignals.signals.Select(x => x.proba.ToString(SeparateHelper.Separator)).ToList();
            model.PageName = "Signals";
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
            var RaiseSignals = datavisioAPIService.GetSignals(token, "BTC", "raise").Result;
            var FallSignals = datavisioAPIService.GetSignals(token, "BTC", "fall").Result;

            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
            };

            RaiseSignals.signals = RaiseSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();


            FallSignals.signals = FallSignals.signals
                .Where(x => x.time >= filter.StartDate.AddHours(-3))
                .Where(x => x.time < filter.EndDate.AddHours(-3))
                .OrderBy(x => x.time)
                .ToArray();



            model.RaiseDates = RaiseSignals.signals.Select(x => x.time.AddHours(3)).ToList();
            model.FallDates = FallSignals.signals.Select(x => x.time.AddHours(3)).ToList();

            foreach (var signal in RaiseSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);
                model.signals.Add(new RaiseFallSignals
                {
                    Date = signal.time,
                    RaiseProba = signal.proba
                });
            }

            foreach (var signal in FallSignals.signals)
            {
                signal.proba = signal.value == 1 ? signal.proba : 1 - signal.proba;
                signal.time = signal.time.AddHours(3).AddSeconds(-signal.time.Second).AddMilliseconds(-signal.time.Millisecond);

                var sign = model.signals.FirstOrDefault(x => x.Date == signal.time);
                if (sign != null)
                {
                    sign.FallProba = signal.proba;
                }
                else
                {
                    model.signals.Add(new RaiseFallSignals
                    {
                        Date = signal.time,
                        FallProba = signal.proba
                    });
                }
            }


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("RaiseFallSignals" + DateTime.Now.ToJavascriptTicks());
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Date";
                worksheet.Cell(currentRow, 2).Value = "Raise Proba";
                worksheet.Cell(currentRow, 3).Value = "Fall Proba";
                
                foreach (var signal in model.signals)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = signal.Date;
                    worksheet.Cell(currentRow, 2).Value = signal.RaiseProba;
                    worksheet.Cell(currentRow, 3).Value = signal.FallProba;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "users.xlsx");
                }
            }
        }


    }
}