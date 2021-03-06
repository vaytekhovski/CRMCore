﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using CRM.ViewModels;
using Business;
using Business.DataVisioAPI;
using CRM.Services.Balances;
using System.Diagnostics;
using AuthApp.Controllers;

namespace CRM.Controllers.User
{
    [Authorize] 
    public class UserPanelController : Controller
    {
        private readonly DatavisioAPIService datavisioAPIService;
        private readonly BalancesService balancesService;

        public UserPanelController()
        {

            datavisioAPIService = new DatavisioAPIService();
            balancesService = new BalancesService();

        }

        [HttpGet]
        public async Task<ActionResult> UserPanel(UserPanelModel model)
        {
            var token = HttpContext.User.Identity.Name;
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            model.BalancesDebit = await balancesService.LoadBalancesAsync(accountId, token, "debit");
            model.BalancesMargin = await balancesService.LoadBalancesAsync(accountId, token, "margin");
            model.AccountData = datavisioAPIService.ShowAccount(accountId, token).Result;
            model.Accounts = datavisioAPIService.ShowAccounts(token).Result;
            foreach (var pair in model.AccountData.pairs)
            {
                pair.coin = pair.@base;
            }
            
            
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeEnabling(UserPanelModel model)
        {
            var accountId = HttpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            var token = HttpContext.User.Identity.Name;
            var AccountData = datavisioAPIService.ShowAccount(accountId, token).Result;

            foreach (var pair in model.AccountData.pairs)
            {
                if (AccountData.pairs.First(x => x.@base == pair.coin).enabled != pair.enabled)
                {
                    if (pair.enabled)
                    {
                        await datavisioAPIService.EnablePair(accountId, token, pair.coin, pair.quote);
                    }
                    else
                    {
                        await datavisioAPIService.DisablePair(accountId, token, pair.coin, pair.quote);
                    }
                }
            }

            return RedirectToAction("UserPanel", "UserPanel");
        }

        public async Task<IActionResult> SwitchAccount(string accountId, string Name, string returnUrl = "")
        {
            await AccountController.Authenticate(HttpContext, accountId, HttpContext.User.Identity.Name, Name);
            if (returnUrl == "")
            {
                return RedirectToAction("UserPanel", "UserPanel");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }


        [HttpGet]
        public async Task<IActionResult> ExitFromAccount()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home", "Home");
        }


    }
}