using Microsoft.AspNetCore.Mvc;
using CRM.Services.Data;
using Microsoft.AspNetCore.Authorization;
using CRM.ViewModels.Data;

namespace CRM.Controllers.Data
{
    [Authorize]
    public class DataController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowOrderBookAsks(OrderBookViewModel model)
        {
            OrderBookService orderBook = new OrderBookService();
            orderBook.Load("ask", model.Coin, model.Situation, model.StartDate, model.EndDate);

            model.Show = orderBook.Show; // TODO: использовать модели, без необходимости ViewBag и ViewData не использовать
            model.SummVolume = orderBook.SummVolume;

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowOrderBookBids(OrderBookViewModel model)
        {
            OrderBookService orderBook = new OrderBookService();
            orderBook.Load("bid", model.Coin, model.Situation, model.StartDate, model.EndDate);

            model.Show = orderBook.Show;
            model.SummVolume = orderBook.SummVolume;

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowTradeHistory(TradeHistoryViewModel model)
        {
            TradeHistoryService tradeHistory = new TradeHistoryService();
            model.OrderType = model.OrderType == null ? "all" : model.OrderType;
            tradeHistory.Load(model.Coin, model.Situation, model.OrderType, model.StartDate, model.EndDate);

            model.Show = tradeHistory.Show;
            model.SummVolume = tradeHistory.SummVolume;

            return View(model);
        }

        [HttpPost]
        public ActionResult ShowTradeDelta(TradeDeltaViewModel model)
        {
            TradeDeltaService tradeDelta = new TradeDeltaService();
            tradeDelta.Load(model.Coin, model.StartDate, model.EndDate, model.NullDelta);

            model.Show = tradeDelta.Show;
            model.SummDelta = tradeDelta.SummDelta;
            return View(model);
        }
    }
}