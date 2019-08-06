using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Services.Balances;
using CRM.ViewModels.Balances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Balances
{
    [Authorize]
    public class BalancesController : Controller
    {
        private readonly BalancesService _balancesService;

        public BalancesController()
        {
            _balancesService = new BalancesService();
        }

        [HttpGet]
        public ActionResult Balances()
        {
            var model = new BalancesModel
            {
                Account = "/",
                AccountBalances = new List<Balance>()
            };
            ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Balances(BalancesModel model)
        {
            model = await _balancesService.LoadBalancesAsync(model.Account); //TODO: !!! use async/await where possible
            ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            return View(model);
        }
    }
}
