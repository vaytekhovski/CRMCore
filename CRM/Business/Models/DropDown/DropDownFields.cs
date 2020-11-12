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
            IgnoreIds.Add(new Field { Value = "8a6957d0-b66a-4a5a-8ec0-e7a3eacc7dbe", Name = "13" });
            IgnoreIds.Add(new Field { Value = "c63cb362-4d33-473e-8d79-cc88618057a4", Name = "14" });
            IgnoreIds.Add(new Field { Value = "8e892a02-27a5-4a82-aee4-5c6f6bbc0fb4", Name = "15" });
            IgnoreIds.Add(new Field { Value = "777170de-275f-4703-a8f3-a4ca1282e1f5", Name = "16" });
            IgnoreIds.Add(new Field { Value = "7f0df3a1-996a-4ba2-b5d9-e02c1aaff0b9", Name = "17" });
            IgnoreIds.Add(new Field { Value = "d384bf88-1fc2-4dc4-b809-806f4841de83", Name = "18" });
            IgnoreIds.Add(new Field { Value = "b16a2db0-e8bc-4038-a30f-9dd50e58c28e", Name = "19" });

            IgnoreIds.Add(new Field { Value = "3cfea91f-abfe-4012-98c5-811c4b8e4970", Name = "20" });
            IgnoreIds.Add(new Field { Value = "fec0ea62-0f03-48f2-bd77-345f07a31082", Name = "21" });
            IgnoreIds.Add(new Field { Value = "854ac63c-3237-4588-a511-8a931625ea32", Name = "23" });
            IgnoreIds.Add(new Field { Value = "0f63682a-f2f0-478d-bd6c-0ee8e7aae872", Name = "24" });
            IgnoreIds.Add(new Field { Value = "dc00108b-0e26-4b1f-b6d7-2482fb7e22fa", Name = "25" });
            IgnoreIds.Add(new Field { Value = "ebb92861-874f-4d6e-8526-c306deec9d02", Name = "26" });
            IgnoreIds.Add(new Field { Value = "62908b9b-7f5f-4c6b-8ff0-9a2d9cd9cbb7", Name = "27" });
            IgnoreIds.Add(new Field { Value = "50e907f2-0293-4e9d-80e4-3f1ab6c946e6", Name = "28" });
            IgnoreIds.Add(new Field { Value = "c0c87619-65a0-49da-94f2-74cfa80b18ea", Name = "29" });
            IgnoreIds.Add(new Field { Value = "a127dfba-b9d1-4b75-9cd0-bc46b3dfc032", Name = "30" });
            IgnoreIds.Add(new Field { Value = "ac228d92-1387-4da8-8b8e-0aff1bbe634c", Name = "31" });
            IgnoreIds.Add(new Field { Value = "69708ca6-0c03-474a-95fd-0580ad959afd", Name = "32" });
            IgnoreIds.Add(new Field { Value = "ea51dcf6-514b-43e0-a5b0-87a5100e7cad", Name = "33" });
            IgnoreIds.Add(new Field { Value = "b44ae684-d0fa-41a9-a707-7480a7b9a574", Name = "34" });
            IgnoreIds.Add(new Field { Value = "07b8d649-a204-4b30-982b-36ddce15628b", Name = "35" });
            IgnoreIds.Add(new Field { Value = "4e3510db-8800-48e1-b58f-d44b3ace31ee", Name = "36" });
            IgnoreIds.Add(new Field { Value = "97e9aedd-e6a6-4f7a-98bf-f33cd6df078a", Name = "37" });
            IgnoreIds.Add(new Field { Value = "05d07684-c6f1-41a8-98d7-411a048861ba", Name = "38" });
            IgnoreIds.Add(new Field { Value = "8f86d42a-92c6-418b-bee7-f93597d0599d", Name = "39" });
            IgnoreIds.Add(new Field { Value = "bc52a37a-3b8f-4cff-a29c-a8ff7bfb597f", Name = "40" });
            IgnoreIds.Add(new Field { Value = "ec33db37-b7b9-46f9-9d0b-16034466065b", Name = "41" });
            IgnoreIds.Add(new Field { Value = "ec33db37-b7b9-46f9-9d0b-16034466065b", Name = "42" });
            IgnoreIds.Add(new Field { Value = "83141bef-8377-44e8-9cf9-74a11accd05c", Name = "43" });

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