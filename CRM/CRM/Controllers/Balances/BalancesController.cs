using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Helpers;
using CRM.Services;
using CRM.Services.Balances;
using CRM.ViewModels;
using CRM.ViewModels.Balances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Balances
{
    [Authorize]
    public class BalancesController : Controller
    {
        private readonly BalancesService balancesService;

        public BalancesController()
        {
            balancesService = new BalancesService();
        }

        [HttpGet]
        public ActionResult Balances()
        {
            var model = new BalancesModel
            {
                Account = "/",
                AccountBalances = new List<Balance>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Balances(BalancesModel model)
        {
            model = balancesService.LoadBalancesAsync(model.Account).Result;
            return View(model);
        }
    }
}
