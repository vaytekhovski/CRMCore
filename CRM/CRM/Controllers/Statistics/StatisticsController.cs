using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
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
        private readonly StatisticsService statisticsService;
        private readonly PaginationService paginationService;

        public StatisticsController()
        {
            statisticsService = new StatisticsService();
            paginationService = new PaginationService();
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
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Statistics(StatisticsViewModel viewModel, string PageButton = "1")
        {
            var Model = new StatisticsModel();
            viewModel.Account = viewModel.Account == "Все" ? "Все аккаунты" : viewModel.Account; //TODO: remove string check, like "All", "Все", "Все аккаунты" etc. Use null check for default list value

            var filter = new StatisticsFilter
            {
                Account = viewModel.Account,
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };

            Model = statisticsService.Load(filter);

            viewModel.Statistics = Model.Statistics;

            var pagination = paginationService.GetPaginationModel(filter.CurrentPage, Model.Statistics.Count());
            viewModel.CurrentPage = filter.CurrentPage;
            viewModel.FirstVisiblePage = pagination.FirstVisiblePage;
            viewModel.LastVisiblePage = pagination.LastVisiblePage;
            viewModel.CountOfPages = pagination.CountOfPages;


            return View(viewModel);
        }
    }
}