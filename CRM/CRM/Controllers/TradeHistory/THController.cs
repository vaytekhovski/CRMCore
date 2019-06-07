using System;
using System.Linq;
using CRM.Helpers;
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
    public class THController : Controller
    {
        private readonly THService THService; // TODO: убрать сокращения в именах бизнес сущностей и сервисов
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
                StartTime = "00:00",
                EndTime = "23:59",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TradeHistory(TradeHistoryFilterModel viewModel, string PageButton = "1")
        {
            var Model = new TradeHistoryModel();

            // TODO: использовать такой паттерн везде
            //var model = service.Load(parameter1, parameter2, ...); 
            //var viewModel = new ViewModel();
            //viewModel.Items = model.Items.Select(x => ...);

            viewModel.Account = viewModel.Account == "Все" ? "Все аккаунты" : viewModel.Account;

            var filter = new TradeHistoryFilter
            {
                Account = viewModel.Account,
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate).Add(TimeSpan.Parse(viewModel.StartTime)),
                EndDate = DateTime.Parse(viewModel.EndDate).Add(TimeSpan.Parse(viewModel.EndTime)),
                CurrentPage = Convert.ToInt32(PageButton)
            };

            Model = THService.Load(filter);

            viewModel = MoveDataFromModelToViewModel(viewModel, Model);


            var pagination = paginationService.GetPaginationModel(filter.CurrentPage, Model.AccountTradeHistories.Count());
            viewModel.CurrentPage = filter.CurrentPage;
            viewModel.FirstVisiblePage = pagination.FirstVisiblePage;
            viewModel.LastVisiblePage = pagination.LastVisiblePage;
            viewModel.CountOfPages = pagination.CountOfPages;

            return View(viewModel);
        }

        private TradeHistoryFilterModel MoveDataFromModelToViewModel(TradeHistoryFilterModel viewModel, TradeHistoryModel Model)
        {
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