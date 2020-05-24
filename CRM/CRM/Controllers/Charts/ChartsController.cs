using Business;
using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.Services.Charts;
using CRM.ViewModels.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using Business.Models.Charts.ProfitOnTimeHistory;
using Business.Contexts;
using Business.Models.Master;
using Microsoft.AspNetCore.Http;

namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly AsksOnBidsService _asksOnBids;

        private readonly DeltaOnTradeHistoryService _deltaOnTradeHistory;


        private readonly TradeHistoryService _tradeHistoryService;

        public ChartsController()
        {
            _asksOnBids = new AsksOnBidsService();
            _deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            _tradeHistoryService = new TradeHistoryService();
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
        public ActionResult ProfitChart(ProfitViewModel ViewModel, HttpContext httpContext)
        {
            TradeHistoryFilter filter = new TradeHistoryFilter
            {
                Coin = ViewModel.Coin,
                Account = ViewModel.Account,
                StartDate = DateTime.Parse(ViewModel.StartDate),
                EndDate = DateTime.Parse(ViewModel.EndDate).AddDays(1),
            };

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var model = _tradeHistoryService.LoadDataToChart(filter, httpContext);

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
            /*
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

            }*/

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewModel.PageName = "Orders On TimeHistory";


            return View(ViewModel);
        }


    }
}