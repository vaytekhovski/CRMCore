﻿using CRM.Models.DropDown;
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
            IgnoreIds.Add(new Field { Value = "8c2d7ed0-dde5-479f-b29b-0d2976a0aac7", Name = "44" });
            IgnoreIds.Add(new Field { Value = "166adc9f-ffdb-41d9-b4b8-dff8eef8d451", Name = "44" });
            IgnoreIds.Add(new Field { Value = "991a2b9f-a257-4d1e-8f4e-c176cceee272", Name = "45" });
            IgnoreIds.Add(new Field { Value = "16639702-c1ef-450d-9fe3-0cd36e442746", Name = "45" });

            IgnoreIds.Add(new Field { Value = "d4e3ecec-09fc-4f2f-945f-f3fedd82cf02", Name = "1" });
            IgnoreIds.Add(new Field { Value = "06151189-7770-4441-9b25-5b52329b1683", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c32c57c6-4f54-41ed-91d5-e71245f43e26", Name = "1" });
            IgnoreIds.Add(new Field { Value = "2c8f8d05-e28c-4fac-9a7b-6defbcee6d86", Name = "1" });
            IgnoreIds.Add(new Field { Value = "38e91f06-31e2-424f-9061-e1d84963c685", Name = "1" });
            IgnoreIds.Add(new Field { Value = "2e306f9f-0275-46a6-a02c-b474ff7e51bb", Name = "1" });
            IgnoreIds.Add(new Field { Value = "2bec04d5-f05a-4c55-9de1-4067c8676f45", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a6588cd5-0862-4dbc-8947-e856dc7d6bd4", Name = "1" });
            IgnoreIds.Add(new Field { Value = "675cb271-b45b-417b-9c38-0c4bf79e2fcf", Name = "1" });
            IgnoreIds.Add(new Field { Value = "98ae7c02-bef7-4108-9eaf-7fd3bc779a8f", Name = "1" });
            IgnoreIds.Add(new Field { Value = "678de6c5-2642-4e61-961d-816f726e55b4", Name = "1" });
            IgnoreIds.Add(new Field { Value = "48e7b996-24c6-4a03-870f-4b81ab4cecc4", Name = "1" });
            IgnoreIds.Add(new Field { Value = "0ae5afe5-4893-4293-8922-0f9b8553b152", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f05d51ba-a5d0-40ef-8f6f-ac9dd4c61a1b", Name = "1" });
            IgnoreIds.Add(new Field { Value = "8c5fe328-a13a-46aa-ad0d-4ae142e392db", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f31e7308-5b2f-47ed-9330-78488f26cb21", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a0733c81-df3d-480f-982e-56bcbe9891fd", Name = "1" });
            IgnoreIds.Add(new Field { Value = "be4c6dfc-7d1c-4cce-99b2-77a73790f5be", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c49e6b58-98a1-4456-80fa-b904905918ec", Name = "1" });
            IgnoreIds.Add(new Field { Value = "5348583f-4556-4151-ba11-04e01fcff2cd", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c5a8a566-959d-4c1b-b553-995b39dd081e", Name = "1" });
            IgnoreIds.Add(new Field { Value = "7ba73fa6-f7cf-47cf-8a51-ea76eb95b1f8", Name = "1" });
            IgnoreIds.Add(new Field { Value = "1aaae9bc-4122-4c6a-b421-cfe008f2cfe2", Name = "1" });
            IgnoreIds.Add(new Field { Value = "62667983-95f1-4950-bde4-363e999fad8a", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f1b8f762-4177-4689-9a5c-bfb26b97146c", Name = "1" });
            IgnoreIds.Add(new Field { Value = "702967ea-d099-4bd6-84f3-08ff0e17e09a", Name = "1" });
            IgnoreIds.Add(new Field { Value = "5b3131a7-edfa-436d-a522-170202ef77f3", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c189426d-a537-4933-b305-16ea21db5b94", Name = "1" });
            IgnoreIds.Add(new Field { Value = "354ec73f-17a9-4e4e-aef1-3e96ad2ff487", Name = "1" });
            IgnoreIds.Add(new Field { Value = "89c3a276-d832-4fa3-b522-6e0be1963cc9", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a85d387e-dddb-4379-aec7-fef6d8c656f8", Name = "1" });
            IgnoreIds.Add(new Field { Value = "cbf6c88d-f383-473d-b979-9c25f5336f16", Name = "1" });
            IgnoreIds.Add(new Field { Value = "99c122cd-ba7a-4502-a0e5-7cbc43022637", Name = "1" });
            IgnoreIds.Add(new Field { Value = "8e7f64b1-68f7-4507-aba5-e9114f848b63", Name = "1" });
            IgnoreIds.Add(new Field { Value = "883ad873-f0b8-4734-af3b-732553f65b09", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f607d37c-4784-4e10-a93c-a86f35ddaf3d", Name = "1" });
            IgnoreIds.Add(new Field { Value = "dc8500d8-7909-4b58-bad0-74a16282f65f", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a8103149-500c-46dd-b6c2-27c935cbadc2", Name = "1" });
            IgnoreIds.Add(new Field { Value = "d87aca41-fc62-4cae-ad2b-7df08d59527e", Name = "1" });
            IgnoreIds.Add(new Field { Value = "3b4612f9-a39e-4d4c-9745-eeafea0d24d3", Name = "1" });
            IgnoreIds.Add(new Field { Value = "7ae7e442-ed98-4858-9f94-1599e54865a1", Name = "1" });
            IgnoreIds.Add(new Field { Value = "5412cd7f-214d-427d-99f7-8868ef166fc5", Name = "1" });
            IgnoreIds.Add(new Field { Value = "0435e214-b276-4729-aa71-783fb13f6087", Name = "1" });
            IgnoreIds.Add(new Field { Value = "0f4eb57a-81a4-4e4f-b1e9-c1f0dd9709db", Name = "1" });
            IgnoreIds.Add(new Field { Value = "6eff2340-ba1d-468a-9a2f-102f37c9c594", Name = "1" });
            IgnoreIds.Add(new Field { Value = "04a76727-7c89-44c8-be0c-910ead29e175", Name = "1" });
            IgnoreIds.Add(new Field { Value = "66f1541b-2044-4a70-b8a3-3f78d6ca7a2a", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a3e3db9a-c16f-4b41-ae95-4274cbc1be7d", Name = "1" });
            IgnoreIds.Add(new Field { Value = "17123a48-f2f5-42c4-9f4c-ee3d0214e1cd", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a444643b-6067-4f9a-8790-c44439a33fe8", Name = "1" });
            IgnoreIds.Add(new Field { Value = "6059efa7-5f48-4245-b4e8-26cc9343212a", Name = "1" });



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
            Coins.Add(new Field { Value = "BTC&USDT", Name = "Bitcoin/USDT" });
            Coins.Add(new Field { Value = "BTC&USDC", Name = "Bitcoin/USDC" });
            Coins.Add(new Field { Value = "BTC&BUSD", Name = "Bitcoin/BUSD" });

            Coins.Add(new Field { Value = "ETH&USDT", Name = "Etherium/USDT" });
            Coins.Add(new Field { Value = "ETH&USDC", Name = "Etherium/USDC" });
            Coins.Add(new Field { Value = "ETH&BUSD", Name = "Etherium/BUSD" });
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