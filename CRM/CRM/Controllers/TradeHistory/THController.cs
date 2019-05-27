using System;
using System.Linq;
using CRM.Helpers;
using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class THController : Controller
    {
        private THService tHService; // TODO: [COMPLETE] не статик, инициализируется в конструкторе контроллера


        [HttpGet]
        public ActionResult TradeHistory()
        {
            var model = new TradeHistoryFilterModel
            {
                Account = "/",
                Coin = "all",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };


            return View(model);
        }

        [HttpPost]
        public ActionResult TradeHistory(TradeHistoryFilterModel model, string PageButton = "0")
        {

            tHService = new THService();
            model = LoadTradeHistory(model, Convert.ToInt32(PageButton));

            return View(model);
        }

        private TradeHistoryFilterModel LoadTradeHistory(TradeHistoryFilterModel model, int PageNumber = 0)
        {
            if (tHService.AccountTradeHistories.Count == 0 || PageNumber == 0)
            {
                tHService.Load(model.Account, model.Coin, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate).AddDays(1));
            }

            model.CountOfPages = tHService.CountOfPages;
            model.CurrentPage = PageNumber == 0 ? 1 : PageNumber;

            // TODO: использовать такой паттерн везде
            //var model = service.Load(parameter1, parameter2, ...); 
            //var viewModel = new ViewModel();
            //viewModel.Items = model.Items.Select(x => ...);
            model.Orders = tHService.AccountTradeHistories.Skip((PageNumber - 1) * 100).Take(100).ToList(); //TODO: пагинация через IQueryable

            model.TotalProfit = tHService.TotalProfit;

            return model;
        }


        
    }
}