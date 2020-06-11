using Microsoft.AspNetCore.Mvc;
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
            model.Balances = await balancesService.LoadBalancesAsync(token);
            model.AccountData = datavisioAPIService.ShowAccount(token).Result;

            foreach (var pair in model.AccountData.pairs)
            {
                pair.coin = pair.@base;
            }
            
            
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeEnabling(UserPanelModel model)
        {
            var token = HttpContext.User.Identity.Name;

            var AccountData = datavisioAPIService.ShowAccount(token).Result;

            foreach (var pair in model.AccountData.pairs)
            {
                if (AccountData.pairs.First(x => x.@base == pair.coin).enabled != pair.enabled)
                {
                    if (pair.enabled)
                    {
                        await datavisioAPIService.EnablePair(token, pair.coin);
                    }
                    else
                    {
                        await datavisioAPIService.DisablePair(token, pair.coin);
                    }
                }
            }

            return RedirectToAction("UserPanel", "UserPanel");
        }

        
        [HttpGet]
        public async Task<IActionResult> ExitFromAccount()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home", "Home");
        }


    }
}