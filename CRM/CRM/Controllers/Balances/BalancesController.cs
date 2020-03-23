using System.Collections.Generic;
using System.Threading.Tasks;
using Business;
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
        public async Task<ActionResult> Balances()
        {
            var model = new BalancesModel
            {
                Account = "556c8663-5706-4112-9440-c6ac965cfa26",
                AccountBalances = new List<Balance>()
            };
            ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            

            model = await _balancesService.LoadBalancesAsync(model.Account);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Balances(BalancesModel model)
        {
            model = await _balancesService.LoadBalancesAsync(model.Account); 

            ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            return View(model);
        }
    }
}
