using CRM.Models.DropDown;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Business.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business
{
    public class DropDownFields
    {
        public static List<Field> Exchanges = new List<Field>();
        public static List<Field> Coins = new List<Field>();
        public static List<Field> OrderType = new List<Field>();
        public static List<Field> Situation = new List<Field>();
        public static List<Field> Nulls = new List<Field>();

        static DropDownFields()
        {
            InitiateExchanges();
            InitiateCoins();
            InitiateOrderType();
            InitiateSituations();
            InitiateNulls();
        }


        private static void InitiateExchanges()
        {
            Exchanges.Add(new Field { Value = "Binance", Name = "Binance" });
            Exchanges.Add(new Field { Value = "Poloniex", Name = "Poloniex" });
        }

        private static void InitiateCoins()
        {
            Coins.Add(new Field { Value = "BTC", Name = "USDT-BTC" });
            Coins.Add(new Field { Value = "ETH", Name = "USDT-ETH" });
            Coins.Add(new Field { Value = "LTC", Name = "USDT-LTC" });
        }

        private static void InitiateOrderType()
        {
            OrderType.Add(new Field { Value = "Sell", Name = "Sell" });
            OrderType.Add(new Field { Value = "Buy", Name = "Buy" });
        }

        private static void InitiateSituations()
        {
            Situation.Add(new Field { Value = "Flat", Name = "Flat" });
            Situation.Add(new Field { Value = "Trend", Name = "Trend" });
            Situation.Add(new Field { Value = "Middle", Name = "Middle" });
        }

        private static void InitiateNulls()
        {
            Nulls.Add(new Field { Value = "notnull", Name = "Не показывать нулевые" });
            Nulls.Add(new Field { Value = "null", Name = "Только нулевые" });
        }

        public static IEnumerable<SelectListItem> GetAccounts(HttpContext httpContext)
        {
            int UserId = Convert.ToInt32(httpContext.User.Identity.Name);

            List<SelectListItem> lst = new List<SelectListItem>();
            using (BasicContext context = new BasicContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Id == UserId);

                lst = context.ExchangeKeys
                    .Where(x => user.RoleId == (int)UserModel.Roles.User ? x.UserId == user.Id : true)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId })
                    .ToList();
            }

            return lst;
        }

        public static IEnumerable<SelectListItem> GetAccountsForBalance(HttpContext httpContext)
        {
            int UserId = Convert.ToInt32(httpContext.User.Identity.Name);

            List<SelectListItem> lst = new List<SelectListItem>();
            using (BasicContext context = new BasicContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Id == UserId);
                lst = context.ExchangeKeys
                      .Where(x => user.RoleId != (int)UserModel.Roles.Boss ? x.UserId == user.Id : true)
                      .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId })
                      .ToList();
            }

            return lst;
        }

        public static IEnumerable<SelectListItem> GetExchanges()
        {
            return Exchanges.Select(x => new SelectListItem { Text = x.Name, Value = x.Value }).ToList();
        }

        public static IEnumerable<SelectListItem> GetCoins()
        {
            return Coins.Select(x => new SelectListItem { Text = x.Name, Value = x.Value }).ToList();
        }

        public static IEnumerable<SelectListItem> GetSituations()
        {
            return Situation.Select(x => new SelectListItem { Text = x.Name, Value = x.Value }).ToList();
        }

        public static IEnumerable<SelectListItem> GetNulls()
        {
            return Nulls.Select(x => new SelectListItem { Text = x.Name, Value = x.Value }).ToList();
        }

        public static IEnumerable<SelectListItem> GetOrderTypes()
        {
            return OrderType.Select(x => new SelectListItem { Text = x.Name, Value = x.Value }).ToList();
        }

    }
}