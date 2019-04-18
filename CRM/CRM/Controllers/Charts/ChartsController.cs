using CRM.Services;
using CRM.Services.Charts;
using CRM.ViewModels.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        [HttpGet]
        public ActionResult Charts()
        {
            string minDate = "2019-04-05";
            var model = new AskOnBidViewModel
            {
                StartDate = minDate,
                EndDate = Dates.CurrentDate()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AsksOnBids()
        {
            string minDate = "2019-04-05";
            var model = new AskOnBidViewModel
            {
                StartDate = minDate,
                EndDate = Dates.CurrentDate(),
                DatesAsks = new List<long>(),
                DatesBids = new List<long>(),
                AsksValues = new List<string>(),
                BidsValues = new List<string>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AsksOnBids(AskOnBidViewModel model)
        {
            AsksOnBidsService asksOnBids = new AsksOnBidsService();
            asksOnBids.Load(model.Coin, model.StartDate, model.EndDate);

            model.DatesAsks = asksOnBids.DatesAsks;
            model.DatesBids = asksOnBids.DatesBids;

            model.AsksValues = asksOnBids.AsksValues;
            model.BidsValues = asksOnBids.BidsValues;

            return View(model);
        }

        [HttpGet]
        public ActionResult DeltaOnTradeHistory()
        {
            string minDate = "2019-04-05";
            var model = new DeltaOnTradeHistoryViewModel
            {
                StartDate = minDate,
                EndDate = Dates.CurrentDate(),
                DatesDelta = new List<long>(),
                DeltaValues = new List<string>(),
                DatesTHBuy = new List<long>(),
                THBuyValues = new List<string>(),
                DatesTHSell = new List<long>(),
                THSellValues = new List<string>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult DeltaOnTradeHistory(DeltaOnTradeHistoryViewModel model)
        {
            DeltaOnTradeHistoryService deltaOnTradeHistory = new DeltaOnTradeHistoryService();
            deltaOnTradeHistory.Load(model.Coin, model.StartDate, model.EndDate);

            model.DatesDelta = deltaOnTradeHistory.DatesDelta;
            model.DeltaValues = deltaOnTradeHistory.DeltaValues;

            model.DatesTHBuy = deltaOnTradeHistory.DatesTHBuy;
            model.THBuyValues = deltaOnTradeHistory.THBuyValues;

            model.DatesTHSell = deltaOnTradeHistory.DatesTHSell;
            model.THSellValues = deltaOnTradeHistory.THSellValues;


            return View(model);
        }
    }
}