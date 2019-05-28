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
        [HttpGet]
        public ActionResult Balances()
        {
            var model = new BalancesModel
            {
                Account = "/",
                AccountBalances = new System.Collections.Generic.List<Balance>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Balances(BalancesModel model)
        {
            model.AccountBalances = BalancesService.LoadBalances(model.Account);
            return View(model);
        }
    }
}
