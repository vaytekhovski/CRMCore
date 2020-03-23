using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.Contexts;
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


            using (BasicContext context = new BasicContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Id == Convert.ToInt32(HttpContext.User.Identity.Name));
                if (user.RoleId == 3)
                {
                    model = await _balancesService.LoadBalancesAsync(model.Account);
                }
                else
                    return RedirectToAction("PermissionDenied", "Home");
            }

            ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Balances(BalancesModel model)
        {
            using (BasicContext context = new BasicContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Id == Convert.ToInt32(HttpContext.User.Identity.Name));
                if (user.RoleId == 3)
                {
                    model = await _balancesService.LoadBalancesAsync(model.Account);
                }
                else
                    return RedirectToAction("PermissionDenied", "Home");
            }

            ViewBag.Accounts = DropDownFields.GetAccountsForBalance(HttpContext);
            return View(model);
        }
    }
}
