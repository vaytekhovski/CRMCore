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
        private static THService tHService; // TODO: не статик, инициализируется в конструкторе контроллера


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

            tHService = new THService();

            return View(model);
        }

        [HttpPost]
        public ActionResult TradeHistory(TradeHistoryFilterModel model, string PageButton = "0")
        {
            model = LoadTradeHistory(model, Convert.ToInt32(PageButton));

            return View(model);
        }

        private TradeHistoryFilterModel LoadTradeHistory(TradeHistoryFilterModel model, int PageNumber = 0)
        {
            if (DateTime.TryParse(model.StartDate, out var StartDate) && DateTime.TryParse(model.EndDate, out var EndDate))
            {
                EndDate = EndDate.AddDays(1);
                if (tHService.AccountTradeHistories.Count == 0 || PageNumber == 0)
                {
                    tHService.Load(model.Account, model.Coin, StartDate, EndDate);
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

            ModelState.AddModelError("Date", "Dates invalid");
            return model;
        }


        
    }
}