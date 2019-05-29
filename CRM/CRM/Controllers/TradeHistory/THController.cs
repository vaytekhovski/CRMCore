using System;
using System.Linq;
using CRM.Helpers;
using CRM.Models.TradeHistory;
using CRM.Services;
using CRM.Services.Pagination;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class THController : Controller
    {
        private readonly THService THService; // TODO: [COMPLETE] не статик, инициализируется в конструкторе контроллера
        private readonly PaginationService paginationService;

        public THController()
        {
            THService = new THService();
            paginationService = new PaginationService();
        }
        
        [HttpGet]
        public ActionResult TradeHistory()
        {
            var viewModel = new TradeHistoryFilterModel
            {
                Account = "/",
                Coin = "all",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TradeHistory(TradeHistoryFilterModel viewModel, string PageButton = "1")
        {
            int PageNumber = Convert.ToInt32(PageButton);
            TradeHistoryModel Model = new TradeHistoryModel();
            // TODO: использовать такой паттерн везде
            //var model = service.Load(parameter1, parameter2, ...); 
            //var viewModel = new ViewModel();
            //viewModel.Items = model.Items.Select(x => ...);

            viewModel.Account = viewModel.Account == "Все" ? "Все аккаунты" : viewModel.Account;

            if (Model.AccountTradeHistories.Count == 0)
            {
                Model = THService.Load(viewModel.Account, viewModel.Coin, DateTime.Parse(viewModel.StartDate), DateTime.Parse(viewModel.EndDate).AddDays(1));
            }
           
            viewModel.Orders = Model.AccountTradeHistories.Skip((PageNumber - 1) * 100).Take(100).ToList(); //TODO: пагинация через IQueryable

            viewModel.TotalProfit = Model.TotalProfit;
            viewModel.DesiredTotalProfit = Model.DesiredTotalProfit;

            viewModel.CurrentPage = PageNumber;

            var pagination = paginationService.GetPaginationModel(PageNumber, Model.AccountTradeHistories.Count);

            viewModel.FirstVisiblePage = pagination.FirstVisiblePage;
            viewModel.LastVisiblePage = pagination.LastVisiblePage;
            viewModel.CountOfPages = pagination.CountOfPages;

            return View(viewModel);
        }



        
    }
}