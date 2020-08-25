using System;
using System.Linq;
using Business;
using CRM.Helpers;
using CRM.Models;
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
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            _statisticsService = new StatisticsService();
            _paginationService = new PaginationService();
        }

        [HttpGet]
        public IActionResult Statistics()
        {
            var viewModel = new StatisticsViewModel
            {
                Id = "Statistics",
                Account = null,
                Coin = "all",
                StartDate = DatesHelper.MinDateStr,
                EndDate = DatesHelper.CurrentDateStr
            };

            //TODO: [COMPLETE] call GetAccounts and similar dropdown methods in controller, pass through ViewBag
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Statistics(StatisticsViewModel viewModel, string PageButton = "1")
        {
            var Model = new StatisticsModel();

            var filter = new StatisticsFilter
            {
                Account = viewModel.Account,
                Coin = viewModel.Coin,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate),
                CurrentPage = Convert.ToInt32(PageButton)
            };

            Model = _statisticsService.Load(filter, HttpContext);
            if (Model == null)
            {
                return RedirectToAction("Login","Account");
            }

            viewModel.Statistics = Model.Statistics;

            var pagination = _paginationService.GetPaginationModel(filter.CurrentPage, Model.Statistics.Count());
            viewModel.CurrentPage = filter.CurrentPage;
            viewModel.CountOfPages = pagination.CountOfPages;
            viewModel.Action = "Statistics/Statistics";
            viewModel.TypeOfDate = "date";
            ViewBag.Coins = DropDownFields.GetCoins();
            return View(viewModel);
        }
    }
}