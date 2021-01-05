using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.Models.DataVisioAPI;
using CRM.Helpers;
using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly TradeHistoryService tradeHistoryService;

        public DashboardController(TradeHistoryService tradeHistoryService)
        {
            this.tradeHistoryService = tradeHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TradeHistoryFilterModel
            {
                Id = "TradeHistory",
                Coin = null,
                StartDate = DatesHelper.MinDateTimeStr,
                EndDate = DatesHelper.CurrentDateTimeStr,
                CurrentPage = 1
            };

            var filter = new TradeHistoryFilter
            {
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate),
                CurrentPage = Convert.ToInt32(viewModel.CurrentPage.ToString())
            };

            TradeHistoryModel Model = await tradeHistoryService.LoadAsync(filter, HttpContext);
            viewModel = Converter(Model, viewModel);


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TradeHistoryFilterModel viewModel)
        {
            var filter = new TradeHistoryFilter
            {
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate),
                CurrentPage = Convert.ToInt32(viewModel.CurrentPage.ToString())
            };

            TradeHistoryModel Model = await tradeHistoryService.LoadAsync(filter, HttpContext);
            viewModel = Converter(Model, viewModel);


            return View(viewModel);
        }

        public TradeHistoryFilterModel Converter(TradeHistoryModel Model, TradeHistoryFilterModel viewModel)
        {
            var ClosedDeals = Model.Deals.deals.Where(x => x.outcome != 0).ToList();
            foreach (var item in Model.Deals.deals)
            {
                item.coin = item.@base;
                item.opened = item.opened.AddHours(3);
                item.closed = item.closed != null ? item.closed.Value.AddHours(3) : new DateTime(1999, 01, 01);
                item.fee = Math.Abs(item.profit.dirty.amount - item.profit.clean.amount);
            }

            viewModel.TotalProfit = Model.TotalProfit;
            viewModel.LossOrdersSumm = Model.LossOrdersSumm;
            viewModel.DepositProfit = Model.DepositProfit;

            viewModel.Deals = new ListDeals();
            viewModel.Deals.page = Model.Deals.page;
            viewModel.Deals.deals = new Deal[] { };

            viewModel.Deals.deals = ClosedDeals.OrderByDescending(x => x.opened).ToArray();
            return viewModel;
        }
    }
}