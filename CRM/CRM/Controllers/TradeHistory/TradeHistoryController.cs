using System;
using System.Linq;
using Business;
using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.Services.Pagination;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class TradeHistoryController : Controller
    {
        private readonly TradeHistoryService _tradeHistoryService;
        private readonly PaginationService _paginationService;

        public TradeHistoryController()
        {
            _tradeHistoryService = new TradeHistoryService();
            _paginationService = new PaginationService();
        }
        
        [HttpGet]
        public ActionResult TradeHistory()
        {
            var viewModel = new TradeHistoryFilterModel
            {
                Id = "TradeHistory",
                Account = "/",
                Coin = "all",
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr,
            };

            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
            ViewBag.Algorithms = DropDownFields.GetAlgorithms();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TradeHistory(TradeHistoryFilterModel viewModel, string PageButton = "1")
        {
            // TODO: [COMPLETE] использовать такой паттерн везде
            //var model = service.Load(parameter1, parameter2, ...); 
            //var viewModel = new ViewModel();
            //viewModel.Items = model.Items.Select(x => ...);


            var filter = new TradeHistoryFilter
            {
                Account = viewModel.Account,
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };

            TradeHistoryModel Model = _tradeHistoryService.Load(filter);

            viewModel = MoveDataFromModelToViewModel(Model, viewModel);


            var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, Model.CountOfElements);
            viewModel.CurrentPage = filter.CurrentPage;
            viewModel.CountOfPages = pagination.CountOfPages;
            viewModel.Action = "TradeHistory/TradeHistory";
            viewModel.TypeOfDate = "datetime-local";
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
            ViewBag.Algorithms = DropDownFields.GetAlgorithms();
            return View(viewModel);
        }

        private TradeHistoryFilterModel MoveDataFromModelToViewModel(TradeHistoryModel Model, TradeHistoryFilterModel viewModel)
        {
            //TODO: [COMPLETE] make single query for all distinct accounts, set names
            
            foreach (var item in Model.AccountTradeHistories)
            {
                item.Account = AccountExchangeKeys.ExchangeKeys.FirstOrDefault(x => x.AccountId == item.Account).Name;
            }

            viewModel.Orders = Model.AccountTradeHistories;

            viewModel.TotalProfit = Model.TotalProfit;
            viewModel.DesiredTotalProfit = Model.DesiredTotalProfit;

            viewModel.LossOrdersCount = Model.LossOrdersCount;
            viewModel.LossOrdersSumm = Model.LossOrdersSumm;

            viewModel.ProfitOrdersCount = Model.ProfitOrdersCount;
            viewModel.ProfitOrdersSumm = Model.ProfitOrdersSumm;

            viewModel.DesiredLossOrdersCount = Model.DesiredLossOrdersCount;
            viewModel.DesiredLossOrdersSumm = Model.DesiredLossOrdersSumm;

            viewModel.DesiredProfitOrdersCount = Model.DesiredProfitOrdersCount;
            viewModel.DesiredProfitOrdersSumm = Model.DesiredProfitOrdersSumm;
            

            return viewModel;
        }




    }
}