﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Business.Contexts;
using Business;

namespace AuthApp.Controllers
{
    public class AccountController : Controller
    {
        private BasicContext _db;
        public AccountController(BasicContext context)
        {
            _db = context;
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

            UserModel user = await _db.UserModels
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);

            if (user != null)
            {
                await Authenticate(user); // аутентификация


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserModel user = await _db.UserModels.FirstOrDefaultAsync(u => u.Login == model.Login);

            if (user == null)
            {
                // добавляем пользователя в бд
                user = new UserModel { Login = model.Login, Password = model.Password, RegistrationDate = DateTime.Now};
                Role userRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                if (userRole != null)
                    user.Role = userRole;

                _db.UserModels.Add(user);
                await _db.SaveChangesAsync();

                await Authenticate(user); // аутентификация

                return RedirectToAction("Index", "Home");
            }
            else  
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        public async Task Authenticate(UserModel user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));


            using (BasicContext db = new BasicContext())
            {
                db.UserModels.FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password).Result.LastAuthorizationDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home", "Home");
        }
    }
}