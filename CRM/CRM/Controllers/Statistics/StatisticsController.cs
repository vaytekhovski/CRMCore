using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Models;
using CRM.Models.Filters;
using CRM.Models.Statistics;
using CRM.Services.Pagination;
using CRM.Services.Statistics;
using CRM.ViewModels.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Statistics
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly StatisticsService _statisticsService;
        private readonly PaginationService _paginationService;

        public StatisticsController()
        {
            _statisticsService = new StatisticsService();
            _paginationService = new PaginationService();
        }

        [HttpGet]
        public IActionResult Statistics()
        {
            var viewModel = new StatisticsViewModel
            {
                Account = "/",
                Coin = "all",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };

            //ViewBag.Coins = DropDownFields.GetCoins();
            //ViewBag.Accounts = DropDownFields.GetAccounts(HttpContext);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Statistics(StatisticsViewModel viewModel, string PageButton = "1")
        {
            var Model = new StatisticsModel();

            //TODO: [COMPLETE] remove string check, like "All", "Все", "Все аккаунты" etc. Use null check for default list value

            var filter = new StatisticsFilter
            {
                Account = viewModel.Account,
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };

            Model = _statisticsService.Load(filter);

            viewModel.Statistics = Model.Statistics;

            var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, Model.Statistics.Count());
            viewModel.CurrentPage = filter.CurrentPage;
            viewModel.FirstVisiblePage = pagination.FirstVisiblePage;
            viewModel.LastVisiblePage = pagination.LastVisiblePage;
            viewModel.CountOfPages = pagination.CountOfPages;


            return View(viewModel);
        }
    }
}