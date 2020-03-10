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
            viewModel.TotalProfitWithoutFee = Model.TotalProfitWithoutFee;

            viewModel.LossOrdersCount = Model.LossOrdersCount;
            viewModel.LossOrdersSumm = Model.LossOrdersSumm;

            viewModel.ProfitOrdersCount = Model.ProfitOrdersCount;
            viewModel.ProfitOrdersSumm = Model.ProfitOrdersSumm;

            viewModel.ProfitOrdersCountWithoutFee = Model.ProfitOrdersCountWithoutFee;
            viewModel.LossOrdersCountWithoutFee = Model.LossOrdersCountWithoutFee;

            viewModel.ProfitOrdersSummWithoutFee = Model.ProfitOrdersSummWithoutFee;
            viewModel.LossOrdersSummWithoutFee = Model.LossOrdersSummWithoutFee;

            viewModel.TotalEnterTax = Model.TotalEnterTax;

            if (Model.AccountTradeHistories.Count > 0)
            {
                viewModel.RPL = (decimal)Model.ProfitOrdersCount / (decimal)Model.LossOrdersCount;
                viewModel.AP = Model.ProfitOrdersCount > 0 ? Model.ProfitOrdersSumm / Model.ProfitOrdersCount : 0;
                viewModel.AL = Model.LossOrdersSumm / Model.LossOrdersCount;
                viewModel.AR = Model.TotalProfit / (Model.ProfitOrdersCount + Model.LossOrdersCount);
                viewModel.RAPAL = viewModel.AP / Math.Abs(viewModel.AL);

                var TroughValue = Model.AccountTradeHistories.Min(x => x.DollarQuantity);
                var PeakValue = Model.AccountTradeHistories.Max(x => x.DollarQuantity);
                viewModel.MIDD = TroughValue - PeakValue / PeakValue;
                viewModel.Dmin = viewModel.MIDD + 100;

                viewModel.R = Model.TotalProfit / viewModel.Dmin;
                viewModel.RF = Model.TotalProfit / viewModel.MIDD;
                viewModel.PF = Model.ProfitOrdersSumm / Math.Abs(Model.LossOrdersSumm);
                viewModel.APF = (Model.ProfitOrdersSumm - Model.AccountTradeHistories.Max(x => x.Profit) / Math.Abs(Model.LossOrdersSumm));
                viewModel.CompoundInterest = 1;
                viewModel.CompoundInterestWithoutFee = 1;

                var MidPercentProfit = Model.AccountTradeHistories.Sum(x => x.PercentProfit) / Model.AccountTradeHistories.Count;

                double StandardDeviation = 0;
                foreach (var percentProfit in Model.AccountTradeHistories.Where(x=>x.PercentProfit != 0).OrderBy(x=> x.Time).Select(x => x.PercentProfit).ToList())
                {
                    StandardDeviation += Math.Pow((double)percentProfit - (double)MidPercentProfit, 2);

                    viewModel.CompoundInterest *= 1 + (percentProfit / 100);
                }

                foreach (var percentProfitWithoutFee in Model.AccountTradeHistories.Where(x => x.PercentProfitWithoutFee != 0).OrderBy(x => x.Time).Select(x => x.PercentProfitWithoutFee).ToList())
                {
                    viewModel.CompoundInterestWithoutFee *= 1 + (percentProfitWithoutFee / 100);
                }

                viewModel.CompoundInterest = (viewModel.CompoundInterest - 1) * 100;
                viewModel.CompoundInterestWithoutFee = (viewModel.CompoundInterestWithoutFee - 1) * 100;


                StandardDeviation /= Model.AccountTradeHistories.Count;
                StandardDeviation = Math.Sqrt(StandardDeviation);

                viewModel.SharpeRatio = (MidPercentProfit - 0.05m) / (decimal)StandardDeviation;
            }

            return viewModel;
        }




    }
}