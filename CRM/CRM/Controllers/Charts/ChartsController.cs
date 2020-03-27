using Business;
using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.Services.Charts;
using CRM.Services.IndicatorPoints;
using CRM.ViewModels.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using Business.Models.Charts.ProfitOnTimeHistory;
using Business.Contexts;
using Business.Models.Master;

namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly AsksOnBidsService _asksOnBids;

        private readonly DeltaOnTradeHistoryService _deltaOnTradeHistory;

        private readonly IndicatorPointsService _indicatorPointsService;

        private readonly TradeHistoryOnTradeHistoryDeltaService _tradeHistoryOnTradeHistoryDeltaService;

        private readonly TradeHistoryService _tradeHistoryService;

        private readonly BollingerBandsService _bollingerBandsService;

        private readonly TradeHistoryIndicatorsService _tradeHistoryIndicatorsService;

        public ChartsController()
        {
            _asksOnBids = new AsksOnBidsService();
            _deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            _indicatorPointsService = new IndicatorPointsService();
            _tradeHistoryOnTradeHistoryDeltaService = new TradeHistoryOnTradeHistoryDeltaService();
            _tradeHistoryService = new TradeHistoryService();
            _bollingerBandsService = new BollingerBandsService();
            _tradeHistoryIndicatorsService = new TradeHistoryIndicatorsService();
        }

        [HttpGet]
        public ActionResult Charts()
        {
            var model = new AskOnBidViewModel
            {
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(model);
        }

        [HttpGet]
        public ActionResult AsksOnBids()
        {
            var model = new AskOnBidViewModel
            {
                PageName = "Asks on Bids",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(model);
        }

        [HttpPost]
        public ActionResult AsksOnBids(AskOnBidViewModel ViewModel)
        {
            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
            };

            var model = _asksOnBids.Load(filter);

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            ViewModel.DatesAsks = model.DatesAsks.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.AsksValues = model.AsksValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.DatesBids = model.DatesBids.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.BidsValues = model.BidsValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Asks on Bids";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult DeltaOnTradeHistory()
        {
            var model = new DeltaOnTradeHistoryViewModel
            {
                PageName = "Delta on TradeHistory",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr,
                DatesDelta = new List<long>(),
                DeltaValues = new List<string>(),
                DatesTHBuy = new List<long>(),
                THBuyValues = new List<string>(),
                DatesTHSell = new List<long>(),
                THSellValues = new List<string>()
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(model);
        }

        [HttpPost]
        public ActionResult DeltaOnTradeHistory(DeltaOnTradeHistoryViewModel ViewModel)
        {
            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
            };

            var model = _deltaOnTradeHistory.Load(filter);

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            ViewModel.DatesDelta = model.DatesDelta.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.DeltaValues = model.DeltaValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

            ViewModel.DatesTHBuy = model.DatesTHBuy.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.THBuyValues = model.THBuyValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();

            ViewModel.DatesTHSell = model.DatesTHSell.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.THSellValues = model.THSellValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Delta on TradeHistory";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult IndicatorPoints()
        {
            var viewModel = new IndicatorPointsViewModel
            {
                PageName = "Indicator Points",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult IndicatorPoints(IndicatorPointsViewModel ViewModel)
        {
            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Base,
                Exchange = ViewModel.Exchange,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _indicatorPointsService.Load(filter);

            ViewModel.Dates = model.Dates.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.MACDValues = model.MACDValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.SIGValues = model.SIGValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Indicator Points";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult TradeHistoryOnTradeHistoryDelta()
        {
            var viewModel = new TradeHistoryOnTradeHistoryDeltaViewModel
            {
                PageName = "Volumes and Asks",
                FirstDate = DatesHelper.MinDateTimeStr,
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TradeHistoryOnTradeHistoryDelta(TradeHistoryOnTradeHistoryDeltaViewModel ViewModel)
        {
            
            ChartsFilter filter = new ChartsFilter
            {
                Coin = ViewModel.Base ?? "BTC",
                CalculatingStartDate = DateTime.Parse(ViewModel.FirstDate).AddHours(-3),
                StartDate = DateTime.Parse(ViewModel.StartDate).AddHours(-3),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddHours(-3),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _tradeHistoryOnTradeHistoryDeltaService.Load(filter);

            ViewModel.DatesTH = model.DatesTH.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.DatesTHD = model.DatesTHD.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.THValues = model.THValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.THDValues = model.THDValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Volumes and Asks";
            return View(ViewModel);
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
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
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

            var model = _tradeHistoryService.LoadDataToChart(filter);

            ViewModel.Dates = model.AccountTradeHistories.Select(x => x.Time.ToJavascriptTicks()).ToList();
            ViewModel.Values = model.AccountTradeHistories.Select(x => x.Profit.ToString(SeparateHelper.Separator)).ToList();

            ViewModel.CountOfZero = model.AccountTradeHistories.Where(x => x.Profit == 0).Count();
            ViewModel.CountOfMore = model.AccountTradeHistories.Where(x => x.Profit > 0).Count();
            ViewModel.CountOfLess = model.AccountTradeHistories.Where(x => x.Profit < 0).Count();

            ViewModel.VolumeOfMore = model.ProfitOrdersSumm.ToString(SeparateHelper.Separator);
            ViewModel.VolumeOfLess = (model.LossOrdersSumm * -1).ToString(SeparateHelper.Separator);

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
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
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
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

            using (BasicContext context = new BasicContext())
            {
                List<AccountTradeHistory> BufOrders = context.AccountTradeHistories
                    .Where(x => x.Time >= filter.StartDate)
                    .Where(x => x.Time <= filter.EndDate)
                    .Where(x => x.Pair == filter.Coin)
                    .Where(x => x.Account == filter.Account).ToList();

                
                for (int i = 0; i < BufOrders.ToArray().Length - 1; i++)
                {
                    if (BufOrders[i].Side == "buy" && BufOrders[i + 1].Side == "sell")
                    {
                        if (BufOrders[i + 1].Profit > 0)
                        {
                            for (DateTime time = BufOrders[i].Time;time < BufOrders[i + 1].Time;time = time.AddMinutes(1) )
                            {
                                ViewModel.GreenTimes.Add(time.ToJavascriptTicks());
                            }
                        }
                        else if (BufOrders[i + 1].Profit < 0)
                        {

                            for (DateTime time = BufOrders[i].Time; time < BufOrders[i + 1].Time; time = time.AddMinutes(1))
                            {
                                ViewModel.RedTimes.Add(time.ToJavascriptTicks());
                            }
                        }
                    }
                }

            }

            List<IndicatorValuesModel> Indicators = new List<IndicatorValuesModel>();

            using (MySQLContext context1 = new MySQLContext())
            {
                List<ChartPoint> BufIndicators = context1.ChartPoints
                    .Where(x => x.Time >= filter.StartDate)
                    .Where(x => x.Time <= filter.EndDate)
                    .Where(x => x.Base == filter.Coin)
                    .OrderBy(x => x.Time).ToList();

                

                foreach (var item in BufIndicators)
                {
                    Indicators.Add(new IndicatorValuesModel
                    {
                        Time = item.Time.AddHours(3).ToJavascriptTicks(),
                        Value = item.Close.ToString()
                    });
                }

            }

            ViewModel.Indicators = Indicators;
            ViewBag.MaxValue = Indicators.Select(x => double.Parse(x.Value)).Max();
            ViewBag.MaxValue += ViewBag.MaxValue * 0.3;

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
            ViewModel.PageName = "Orders On TimeHistory";


            return View(ViewModel);
        }

        [HttpGet]
        public IActionResult ProbaBuyOnTimeHistory()
        {
            var viewModel = new ProbaBuyOnTimeHistoryViewModel
            {
                PageName = "OrdersOnTimeHistory",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ProbaBuyOnTimeHistory(ProbaBuyOnTimeHistoryViewModel ViewModel)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                Coin = ViewModel.Coin,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddDays(1),
            };

            List<IndicatorValuesModel> Indicators = new List<IndicatorValuesModel>();

            using (MySQLContext context = new MySQLContext())
            {
                List<ChartPoint> BufIndicators = context.ChartPoints
                    .Where(x => x.Time >= filter.StartDate)
                    .Where(x => x.Time <= filter.EndDate)
                    .Where(x => x.Base == filter.Coin)
                    .OrderBy(x => x.Time).ToList();



                foreach (var item in BufIndicators)
                {
                    Indicators.Add(new IndicatorValuesModel
                    {
                        Time = item.Time.AddHours(3).ToJavascriptTicks(),
                        Value = item.Close.ToString()
                    });
                }


                ViewModel.ProbaBuyTimes = context.NeuralSignals
                    .Where(x=>x.Time > filter.StartDate && x.Time <filter.EndDate)
                    .Where(x => x.Base == filter.Coin).Where(x => x.ProbaBuy > 0.5M)
                    .Select(x => x.Time.ToJavascriptTicks()).ToList();

            }

            ViewModel.Indicators = Indicators;
            ViewBag.MaxValue = Indicators.Select(x => double.Parse(x.Value)).Max();
            ViewBag.MaxValue += ViewBag.MaxValue * 0.3;

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
            ViewModel.PageName = "Orders On TimeHistory";


            return View(ViewModel);
        }


        [HttpGet]
        public ActionResult BollingerBands()
        {
            var viewModel = new BollingerBandsViewModel
            {
                PageName = "Bollinger Bands",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BollingerBands(BollingerBandsViewModel ViewModel)
        {
            ChartsFilter filter = new ChartsFilter
            {
                StartDate = DateTime.Parse(ViewModel.StartDate).AddHours(-3),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddHours(-3),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _bollingerBandsService.Load(filter);

            ViewModel.Dates = model.Dates.Select(x => x.ToJavascriptTicks()).ToList();
            ViewModel.ProbaSellValues = model.ProbaSellValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.ProbaBuyValues = model.ProbaBuyValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.BBLValues = model.BBLValues.Select(x => x.ToString(SeparateHelper.Separator)).ToList();
            ViewModel.PageName = "Bollinger Bands";
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult TradeHistoryIndicators()
        {
            var ViewModel = new TradeHistoryIndicatorsViewModel
            {
                PageName = "TH Indicators",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);


            return View(ViewModel);
        }
        
        [HttpPost]
        public ActionResult TradeHistoryIndicators(TradeHistoryIndicatorsViewModel ViewModel)
        {
            ChartsFilter filter = new ChartsFilter
            {
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate),
            };

            var model = _tradeHistoryIndicatorsService.Load(filter);

            SeparateHelper.Separator.NumberDecimalSeparator = ".";
            foreach (var item in model.Indicators)
            {
                ViewModel.Indicators.Add(new ViewIndicator
                {
                    AL = item.AL.ToString(SeparateHelper.Separator),
                     AP = item.AP.ToString(SeparateHelper.Separator),
                     APF = item.APF.ToString(SeparateHelper.Separator),
                     AR = item.AR.ToString(SeparateHelper.Separator),
                     Date = item.Date.ToJavascriptTicks(),
                     Dmin = item.Dmin.ToString(SeparateHelper.Separator),
                     MIDD = item.MIDD.ToString(SeparateHelper.Separator),
                     N = item.N.ToString(SeparateHelper.Separator),
                     NL = item.NL.ToString(SeparateHelper.Separator),
                     NP = item.NP.ToString(SeparateHelper.Separator),
                     PF = item.PF.ToString(SeparateHelper.Separator),
                     R = item.R.ToString(SeparateHelper.Separator),
                     RAPAL = item.RAPAL.ToString(SeparateHelper.Separator),
                     RF = item.RF.ToString(SeparateHelper.Separator),
                     RPL = item.RPL.ToString(SeparateHelper.Separator),
                     SharpeRatio = item.SharpeRatio.ToString(SeparateHelper.Separator),
                     TL = item.TL.ToString(SeparateHelper.Separator),
                     TP = item.TP.ToString(SeparateHelper.Separator),
                     TR = item.TR.ToString(SeparateHelper.Separator)
                });
            }
            

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);


            return View(ViewModel);
        }

    }
}