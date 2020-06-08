using CRM.Models.DropDown;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
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
        public static List<Field> TimeRanges = new List<Field>();
        public static List<Field> IgnoreIds = new List<Field>();

        static DropDownFields()
        {
            InitiateExchanges();
            InitiateCoins();
            InitiateOrderType();
            InitiateSituations();
            InitiateNulls();
            InitiateTimeRange();
            InitiateIgnoreIds();
        }

        private static void InitiateIgnoreIds()
        {
            IgnoreIds.Add(new Field { Value = "d3ad08f1-a2bd-4f32-89f4-81e2ae5ed5cb", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f3a7f223-c8b0-4ea1-b403-57a9084c1543", Name = "2" });
            IgnoreIds.Add(new Field { Value = "e8999f1f-bbd0-46d0-bdb3-a5c0bbe1d391", Name = "3" });
            IgnoreIds.Add(new Field { Value = "461f6054-e7f3-46a1-9457-d30433a0b18b", Name = "4" });
            IgnoreIds.Add(new Field { Value = "283127e6-d33a-4a46-b53e-8362cda2c234", Name = "5" });
            IgnoreIds.Add(new Field { Value = "c1e8c151-f3ed-4d83-b2f9-01bf3b620314", Name = "6" });
            IgnoreIds.Add(new Field { Value = "400af4b6-e354-4e1d-bf56-a5f93d62565a", Name = "7" });
            IgnoreIds.Add(new Field { Value = "c632e29c-c3a4-49c7-93a8-ab16653e430f", Name = "8" });
            IgnoreIds.Add(new Field { Value = "a92c1387-69f2-47cf-a295-fc678c5d5b83", Name = "9" });
            IgnoreIds.Add(new Field { Value = "9c38c174-44ac-4a3a-a3ed-db9c0b29e19c", Name = "10" });
            IgnoreIds.Add(new Field { Value = "5b46cd81-09aa-465c-b3b3-4fcf29865a16", Name = "11" });
            IgnoreIds.Add(new Field { Value = "e7f0473d-ab8e-40fe-84e8-bbb03c4f1c65", Name = "12" });

        }

        private static void InitiateTimeRange()
        {
            TimeRanges.Add(new Field { Value = "1", Name = "1 мин" });
            TimeRanges.Add(new Field { Value = "5", Name = "5 мин" });
            TimeRanges.Add(new Field { Value = "15", Name = "15 мин" });
            TimeRanges.Add(new Field { Value = "30", Name = "30 мин" });
            TimeRanges.Add(new Field { Value = "60", Name = "1 час" });
            TimeRanges.Add(new Field { Value = "180", Name = "3 часа" });

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
            Coins.Add(new Field { Value = "XRP", Name = "USDT-XRP" });
            Coins.Add(new Field { Value = "ADA", Name = "USDT-ADA" });
            Coins.Add(new Field { Value = "BCH", Name = "USDT-BCH" });
            Coins.Add(new Field { Value = "EOS", Name = "USDT-EOS" });
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

        public static IEnumerable<SelectListItem> GetTimeRages()
        {
            return TimeRanges.Select(x => new SelectListItem { Text = x.Name, Value = x.Value }).ToList();
        }

        public static IEnumerable<SelectListItem> GetIgnoreIds()
        {
            return IgnoreIds.Select(x => new SelectListItem { Text = x.Name, Value = x.Value }).ToList();
        }

    }
}