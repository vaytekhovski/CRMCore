using CRM.Services.Charts;
using CRM.ViewModels.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers.Charts
{
    [Authorize]
    public class ChartsController : Controller
    {
        [HttpGet]
        public ActionResult Charts()
        {
            var model = new AskOnBidViewModel
            {

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