using CRM.Services;
using CRM.Services.Database;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using CRM.ViewModels;
using Business.Contexts;
using Business;
using Business.DataVisioAPI;

namespace CRM.Controllers.User
{
    [Authorize] 
    public class UserPanelController : Controller
    {
        private readonly ChangeUserDataService _changeUserDataService;
        private readonly DatavisioAPIService datavisioAPIService;

        private BasicContext db;

        public UserPanelController(BasicContext context)
        {
            db = context;

            _changeUserDataService = new ChangeUserDataService();
            datavisioAPIService = new DatavisioAPIService();

        }

        [HttpGet]
        public ActionResult UserPanel(UserPanelModel model)
        {
            UserModel user = db.UserModels.FirstOrDefault(x => x.Id == Convert.ToInt32(User.Identity.Name));

            model.Login = user.Login;
            model.Password = user.Password;
            model.RegistrationDate = user.RegistrationDate;

            var token = datavisioAPIService.Authorization(Convert.ToInt32(HttpContext.User.Identity.Name)).Result;
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
            var token = datavisioAPIService.Authorization(Convert.ToInt32(HttpContext.User.Identity.Name)).Result;

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
        public async Task<ActionResult> ChangeLogin(UserPanelModel model)
        {
            _changeUserDataService.ChangeUserLogin(User.Identity.Name, model.Login);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        
        [HttpGet]
        public ActionResult ChangePassword(UserPanelModel model)
        {
            _changeUserDataService.ChangeUserPassword(User.Identity.Name, model.Password);
            return RedirectToAction("UserPanel");
        }
        
        
        [HttpGet]
        public async Task<IActionResult> ExitFromAccount()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home", "Home");
        }


    }
}