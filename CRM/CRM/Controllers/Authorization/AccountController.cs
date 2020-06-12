using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Business;
using Business.DataVisioAPI;
using Microsoft.AspNetCore.Http;

namespace AuthApp.Controllers
{
    public class AccountController : Controller
    {
        DatavisioAPIService DatavisioAPIService { get; set; }

        public AccountController()
        {
            DatavisioAPIService = new DatavisioAPIService();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var key = DatavisioAPIService.Authorization(new Business.Models.DataVisioAPI.LoginModel
            {
                username = model.Login,
                password = model.Password
            }).Result;

            if (key != null)
            {
                await Authenticate(key, model.Login);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return View(model);

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task Authenticate(string key, string login)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, key),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, login)
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home", "Home");
        }

        public static async Task<string> GetAuthorizationKey(HttpContext httpContext, DatavisioAPIService datavisioAPIService)
        {
            string key = httpContext.User.Identity.Name;

            if(!datavisioAPIService.isKeyAvailable(key).Result)
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }


            return key;
        }
    }
}