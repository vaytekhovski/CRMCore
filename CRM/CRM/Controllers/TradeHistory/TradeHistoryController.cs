using System;
using System.Linq;
using CRM.Helpers;
using CRM.Models;
using CRM.Models.Filters;
using CRM.Models.TradeHistory;
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
                EndDate = DatesHelper.CurrentDateTimeStr
            };

            AccountExchangeKeys.InitializeExchangeKeys();
            ViewBag.Coins = DropDownFields.GetCoins();
            ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TradeHistory(TradeHistoryFilterModel viewModel, string PageButton = "1")
        {
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
            return View(viewModel);
        }

        private TradeHistoryFilterModel MoveDataFromModelToViewModel(TradeHistoryModel Model, TradeHistoryFilterModel viewModel)
        {
            //TODO: make single query for all distinct accounts, set names
            viewModel.Orders = Model.AccountTradeHistories.Select(x => { x.Account = AccountExchangeKeys.AccountName(x.Account); return x; }).ToList();
            //viewModel.Orders = Model.AccountTradeHistories;
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