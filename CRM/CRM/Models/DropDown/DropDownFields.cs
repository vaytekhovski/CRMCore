using CRM.Models.DropDown;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

using System.Linq;

namespace CRM.Models
{
    public class DropDownFields
    {
        public static List<Field> Coins = new List<Field>();
        public static List<Field> OrderType = new List<Field>();
        public static List<Field> Situation = new List<Field>();
        public static List<Field> Nulls = new List<Field>();
        public static List<Field> Accounts = new List<Field>();

        public static void InitiateFields()
        {
            InitiateCoins();
            InitiateOrderType();
            InitiateSituations();
            InitiateNulls();
            InitateAccounts();
        }

        private static void InitateAccounts()
        {
            Accounts.Add(new Field { Value = "all", Name = "Все аккаунты" });
            Accounts.Add(new Field { Value = "8025d4bf-4af6-466f-b93c-5a807fd37f68", Name = "BINANCE 1-й аккаунт" });
            Accounts.Add(new Field { Value = "9560eadf-74cf-4596-a7e5-bffcd201f6ec", Name = "BINANCE 2-й аккаунт" });
            Accounts.Add(new Field { Value = "bccd3ca1-0b5e-41ac-8233-3a35209912c7", Name = "POLONIEX 1-й аккаунт" });
            Accounts.Add(new Field { Value = "lol", Name = "YOBIT 1-й аккаунт" });
        }

        private static void InitiateCoins()
        {
            Coins.Add(new Field { Value = "all", Name = "Все валюты" });
            Coins.Add(new Field { Value = "BTC", Name = "USDT-BTC" });
            Coins.Add(new Field { Value = "BNB", Name = "USDT-BNB" });
            Coins.Add(new Field { Value = "EOS", Name = "USDT-EOS" });
            Coins.Add(new Field { Value = "ETH", Name = "USDT-ETH" });
            Coins.Add(new Field { Value = "XRP", Name = "USDT-XRP" });
            Coins.Add(new Field { Value = "LTC", Name = "USDT-LTC" });
            Coins.Add(new Field { Value = "TRX", Name = "USDT-TRX" });

            Coins.Add(new Field { Value = "ZEC", Name = "USDT-ZEC" });
            Coins.Add(new Field { Value = "DASH", Name = "USDT-DASH" });
            Coins.Add(new Field { Value = "XMR", Name = "USDT-XMR" });
            Coins.Add(new Field { Value = "ONT", Name = "USDT-ONT" });
            Coins.Add(new Field { Value = "XLM", Name = "USDT-XLM" });
            Coins.Add(new Field { Value = "ADA", Name = "USDT-ADA" });
            Coins.Add(new Field { Value = "BCHABC", Name = "USDT-BCHABC" });
        }

        private static void InitiateOrderType()
        {
            OrderType.Add(new Field { Value = "all", Name = "Все" });
            OrderType.Add(new Field { Value = "Sell", Name = "Sell" });
            OrderType.Add(new Field { Value = "Buy", Name = "Buy" });
        }

        private static void InitiateSituations()
        {
            Situation.Add(new Field { Value = "all", Name = "Все" });
            Situation.Add(new Field { Value = "Flat", Name = "Flat" });
            Situation.Add(new Field { Value = "Trend", Name = "Trend" });
        }

        private static void InitiateNulls()
        {
            Nulls.Add(new Field { Value = "all", Name = "Показывать нулевые значения" });
            Nulls.Add(new Field { Value = "notnull", Name = "Не показывать нулевые значения" });
        }
        
        public static IEnumerable<SelectListItem> GetAccounts() //TODO: [COMPLETE] переделать все в таком же стиле
        {
            return Accounts.Select(x => new SelectListItem { Text = x.Name, Value = x.Value }).ToList();
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