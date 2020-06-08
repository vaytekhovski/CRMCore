using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.DataVisioAPI;
using CRM.Models;
using CRM.Services.Balances;
using CRM.ViewModels.Balances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Balances
{
    [Authorize]
    public class BalancesController : Controller
    {
        private readonly BalancesService _balancesService;
        DatavisioAPIService datavisioAPIService;


        public BalancesController()
        {
            _balancesService = new BalancesService();
            datavisioAPIService = new DatavisioAPIService();

        }

        [HttpGet]
        public async Task<ActionResult> Balances()
        {

            var model = new BalancesModel
            {
                Account = "",
                AccountBalances = new List<Balance>()
            };
            var token = HttpContext.User.Identity.Name;

            model = await _balancesService.LoadBalancesAsync(token);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Balances(BalancesModel model)
        {
            var token = HttpContext.User.Identity.Name;

            model = await _balancesService.LoadBalancesAsync(token);

            return View(model);
        }
    }
}
