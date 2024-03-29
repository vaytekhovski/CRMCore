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
            IgnoreIds.Add(new Field { Value = "37e5f141-511d-42e3-b637-3d16c364468e", Name = "1" });

            IgnoreIds.Add(new Field { Value = "d81817da-d346-434a-a68e-d7b6bc5601b4", Name = "1" });
            IgnoreIds.Add(new Field { Value = "53cd04df-8933-42bf-a07b-1eda3596edb2", Name = "2" });
            
            IgnoreIds.Add(new Field { Value = "f2512323-7c7b-4508-9ac3-c741ac00d50e", Name = "1" });
            IgnoreIds.Add(new Field { Value = "bd064bfe-8eeb-4a23-8010-0dc990b3d8ec", Name = "2" });
            IgnoreIds.Add(new Field { Value = "45aec6a4-240d-461a-9d5d-6baed137d3cd", Name = "3" });
            IgnoreIds.Add(new Field { Value = "817278ad-b33d-48da-9579-6da81a8a15d2", Name = "4" });
            IgnoreIds.Add(new Field { Value = "785ac756-7353-4911-bbcc-285157025dd6", Name = "5" });
            IgnoreIds.Add(new Field { Value = "11bf8309-1b28-4f89-9402-c38db6d2d32e", Name = "6" });
            IgnoreIds.Add(new Field { Value = "43423e93-bcf4-4997-b986-acce9c00801a", Name = "6" });

            
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

            IgnoreIds.Add(new Field { Value = "fd5edcf7-e4a3-48a9-bf92-4043c8b6bd37", Name = "1" });
            IgnoreIds.Add(new Field { Value = "7d8852d8-f445-4e44-918d-5de9d5d06585", Name = "1" });
            IgnoreIds.Add(new Field { Value = "7d8ce747-dde9-4086-aa62-7b632b2eb61d", Name = "1" });
            
            IgnoreIds.Add(new Field { Value = "3c068543-928e-43dc-b65f-b3a22d606ff0", Name = "1" });
            IgnoreIds.Add(new Field { Value = "62814632-c09f-439f-a4af-15755fca6bf3", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c7860b15-8fc6-40ad-96bf-a5920f12fb5a", Name = "1" });

            IgnoreIds.Add(new Field { Value = "d17ee802-5647-4f69-a82e-c8cdaf39b149", Name = "1" });
            IgnoreIds.Add(new Field { Value = "d17ee802-5647-4f69-a82e-c8cdaf39b149", Name = "1" });
            IgnoreIds.Add(new Field { Value = "287f4418-5299-415f-a026-270df4d6a255", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a7d07834-927a-49d4-a3e0-af486bfda58f", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c7d66425-1b3d-4bfd-9358-ee133a4c3270", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a13a8f2f-cd39-49a9-921d-3c2d773fdd83", Name = "1" });
            
            IgnoreIds.Add(new Field { Value = "543a04c8-68e8-4550-b2df-b4d9d10d8d63", Name = "1" });
            IgnoreIds.Add(new Field { Value = "7dd95fa7-e195-42fb-afd5-5c1dfe37a37b", Name = "1" });
            IgnoreIds.Add(new Field { Value = "e1d2a675-18b5-44ec-a933-fbd694cca2b9", Name = "1" });
            IgnoreIds.Add(new Field { Value = "543a04c8-68e8-4550-b2df-b4d9d10d8d63", Name = "1" });
            
            IgnoreIds.Add(new Field { Value = "1c28e935-e26d-4bde-826e-29b5c5aa4b53", Name = "1" });
            IgnoreIds.Add(new Field { Value = "70fe3317-d5d2-4543-b8d9-034e69587b3a", Name = "1" });
            IgnoreIds.Add(new Field { Value = "571ee65b-3dcd-4bde-ba04-d78de4311881", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f0d9d9f1-449c-470d-89d0-2355e065ad2b", Name = "1" });
            
            IgnoreIds.Add(new Field { Value = "ae45fa12-5fdf-4423-a9ac-74f905f72296", Name = "1" });
            
            
            IgnoreIds.Add(new Field { Value = "ec782a94-3971-48c1-840b-eeb3a6f9149f", Name = "1" });
            IgnoreIds.Add(new Field { Value = "48d9b0ce-3714-4967-9b5f-eaa03bd52996", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a311be12-b27e-4bfd-bb6b-3fd4a0269f55", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f8cc21cd-2ea4-4ef7-903d-ac362a3ca7ef", Name = "1" });
            IgnoreIds.Add(new Field { Value = "810c9e17-7f68-41a0-bf54-8d18a07232ef", Name = "1" });
            IgnoreIds.Add(new Field { Value = "1f8e5a45-7d60-4b4f-b7d2-17380d0d77f0", Name = "1" });
            IgnoreIds.Add(new Field { Value = "bb6d27d1-6554-4d1b-93f3-dfa5e0ca19ca", Name = "1" });
            IgnoreIds.Add(new Field { Value = "fb2597db-bde4-4e6c-90b1-4f97c9239e37", Name = "1" });
            IgnoreIds.Add(new Field { Value = "9e88eaea-365e-4410-82ec-ff591a1bac2f", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f7d7d9ac-acb9-49e6-9c8d-096d451d7cab", Name = "1" });
            IgnoreIds.Add(new Field { Value = "20606956-5fb2-460b-b23e-f3f4b0779786", Name = "1" });
            IgnoreIds.Add(new Field { Value = "e50a7377-eef5-4141-a9f4-09d15289d015", Name = "1" });
            
            IgnoreIds.Add(new Field { Value = "e28bf5b0-d7bb-4aab-990b-a8b7c96a3e73", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c6991439-30f1-4c51-8c5f-537e34bf181d", Name = "1" });
            IgnoreIds.Add(new Field { Value = "6090c3e3-821d-461a-964e-5176a83a5ba1", Name = "1" });
            IgnoreIds.Add(new Field { Value = "2ff96190-6cf7-42bb-b82e-65f561a83290", Name = "1" });
            IgnoreIds.Add(new Field { Value = "21b88dc3-6bc6-4017-9b11-ae36f46887db", Name = "1" });
            IgnoreIds.Add(new Field { Value = "af1ee6da-94fc-4217-be2b-83f540373dc8", Name = "1" });

            IgnoreIds.Add(new Field { Value = "d3484747-6cca-4848-9e17-a1d0efda68f0", Name = "1" });
            IgnoreIds.Add(new Field { Value = "9f5a29e4-a09d-464d-a69e-35df8f0bee82", Name = "1" });
            IgnoreIds.Add(new Field { Value = "dc2279e1-dfc9-40b3-bf20-b32855cd5c7d", Name = "1" });
            
            IgnoreIds.Add(new Field { Value = "af4d0b5e-69ff-4e41-90d7-52a6b2357edc", Name = "1" });
            IgnoreIds.Add(new Field { Value = "5d719f83-7b82-4379-8773-c80185dab717", Name = "1" });
            IgnoreIds.Add(new Field { Value = "ec7035dd-2200-4d8d-a601-943384cdf279", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c7c4b03d-0679-4639-b1e3-70ba02393d4e", Name = "1" });
            IgnoreIds.Add(new Field { Value = "f2aa6e8d-a0f7-49ad-9003-a646ed8de512", Name = "1" });
            
            
            IgnoreIds.Add(new Field { Value = "cfcbdd42-ad62-4b2b-ba88-e6772f8393c5", Name = "1" });
            IgnoreIds.Add(new Field { Value = "90deb32a-e4a4-4c82-844e-fb8c84fece36", Name = "1" });
            IgnoreIds.Add(new Field { Value = "2254f804-505c-4ec3-b16d-fc8f66de10d3", Name = "1" });
            
            
            IgnoreIds.Add(new Field { Value = "13bdf38a-3e93-4556-9b53-2f300eb718b3", Name = "1" });
            IgnoreIds.Add(new Field { Value = "65bbb63e-0d8d-44b9-9059-2f6be9c2ce9d", Name = "1" });
            IgnoreIds.Add(new Field { Value = "6ae80cd1-0f88-4ea0-b46a-444c3849c751", Name = "1" });
            IgnoreIds.Add(new Field { Value = "323e2f15-5014-487b-8000-5433953b7dd8", Name = "1" });
            
            
            IgnoreIds.Add(new Field { Value = "2f4cf380-84b3-414a-8a16-b25603423b89", Name = "1" });
            IgnoreIds.Add(new Field { Value = "73731969-000d-47ed-a107-963a1e35f956", Name = "1" });
            IgnoreIds.Add(new Field { Value = "4e3995dc-1483-424c-9975-20dae60ba2f1", Name = "1" });
            
            IgnoreIds.Add(new Field { Value = "50f05913-177d-417f-bd06-fb9889e946f2", Name = "1" });
            IgnoreIds.Add(new Field { Value = "00cf1dc0-9135-4475-9dc5-0b83cf108538", Name = "1" });
            IgnoreIds.Add(new Field { Value = "2e0a0efe-3004-4338-9ef4-9fd1196d46ed", Name = "1" });
            
            
            IgnoreIds.Add(new Field { Value = "1a141db3-2bc9-4a29-85bb-df572ef5aae6", Name = "1" });
            IgnoreIds.Add(new Field { Value = "e196d0df-5448-4609-8cf5-99d282d876ec", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a87263b0-270f-458f-b2f9-ff2245472dd7", Name = "1" });
            IgnoreIds.Add(new Field { Value = "1cd5e12d-87be-4479-bad8-c1b23219f8fc", Name = "1" });
            IgnoreIds.Add(new Field { Value = "7e525512-2604-4599-b14f-5a8c6d7bbda0", Name = "1" });
            IgnoreIds.Add(new Field { Value = "39d8b65e-d818-4124-affc-287c71c993fb", Name = "1" });
            IgnoreIds.Add(new Field { Value = "ebbc7940-c72c-45d6-99d7-1e10de00ed85", Name = "1" });
            IgnoreIds.Add(new Field { Value = "6615c1d4-ea77-43c4-981c-424357432f86", Name = "1" });
            IgnoreIds.Add(new Field { Value = "e1a1efcc-8d10-4df7-a8a5-8ab852c057aa", Name = "1" });
            IgnoreIds.Add(new Field { Value = "276503f1-3f7d-43ec-ab6f-7898cb602395", Name = "1" });
            IgnoreIds.Add(new Field { Value = "26f27b2c-f2ea-429b-9cfb-5b3e6197d7df", Name = "1" });
            IgnoreIds.Add(new Field { Value = "6ac8ef94-203c-4397-bdaa-01c5cd870c31", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c2ca3509-acee-4d7e-871e-3b47383645f4", Name = "1" });
            IgnoreIds.Add(new Field { Value = "c1c7b1b3-986b-45f9-a4ab-2c3ae9868143", Name = "1" });
            IgnoreIds.Add(new Field { Value = "e81ae8f0-94cc-4157-9453-a9fb281e5f16", Name = "1" });
            IgnoreIds.Add(new Field { Value = "fa6c1f42-3bc7-4504-8194-02adaeea3d94", Name = "1" });
            IgnoreIds.Add(new Field { Value = "ac32d29d-cf93-4fd8-b4ad-27ca4b07da44", Name = "1" });
            IgnoreIds.Add(new Field { Value = "dd0aa3b6-1caa-4776-81aa-79734d5684f0", Name = "1" });
            IgnoreIds.Add(new Field { Value = "76911ffe-8044-4fb5-8aa9-3a8898370469", Name = "1" });
           
            IgnoreIds.Add(new Field { Value = "17b331e7-0c27-4b23-9a2b-b5b2f44624bb", Name = "1" });
            IgnoreIds.Add(new Field { Value = "cbe12f0e-507b-4ec2-a6a9-88da4f478a36", Name = "1" });
            IgnoreIds.Add(new Field { Value = "b89809ef-e0fd-4abd-be24-17571d4ef487", Name = "1" });

            IgnoreIds.Add(new Field { Value = "6b2894cc-6b6e-4213-95fc-495aab9c2454", Name = "1" });
            IgnoreIds.Add(new Field { Value = "b742e715-18fa-4dc5-8ef2-d8c3c6c6d457", Name = "1" });
            IgnoreIds.Add(new Field { Value = "cb7f7c54-39fa-4e30-bda4-3519398ca0cc", Name = "1" });
            IgnoreIds.Add(new Field { Value = "1ad065d0-2232-44e5-a24f-344039f7a71d", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a8156b21-0fce-4043-82fd-637022ebe4a2", Name = "1" });
            IgnoreIds.Add(new Field { Value = "177cede4-8479-4622-87fb-4acaef5f9977", Name = "1" });
            IgnoreIds.Add(new Field { Value = "4455c99d-e3db-4a5c-a1ab-2babd325fe2a", Name = "1" });
            IgnoreIds.Add(new Field { Value = "2959b929-9b64-474b-b77c-0a46ffbba6e3", Name = "1" });
            IgnoreIds.Add(new Field { Value = "d757195f-590a-4e59-94fb-2016242fa9ff", Name = "1" });
            IgnoreIds.Add(new Field { Value = "13777c5f-b46c-4def-83d6-13822f018362", Name = "1" });
            IgnoreIds.Add(new Field { Value = "69bc46a9-9db9-402b-8393-3dd73da59851", Name = "1" });
            IgnoreIds.Add(new Field { Value = "d94597cb-99ae-48d6-b660-f098188c9e3a", Name = "1" });
            IgnoreIds.Add(new Field { Value = "e8eaf667-4628-4fe6-a51a-941f35df4959", Name = "1" });
            IgnoreIds.Add(new Field { Value = "4d975837-f752-4fbe-9ca0-52d30ad11a22", Name = "1" });
            
            
            IgnoreIds.Add(new Field { Value = "16d86c8a-a3a8-4558-a791-3b537d364d4c", Name = "1" });
            IgnoreIds.Add(new Field { Value = "b20821a1-5c27-47bf-b217-f597644cb1f5", Name = "1" });
            IgnoreIds.Add(new Field { Value = "a75b9a30-1f11-4d92-89a0-a3c86b421dea", Name = "1" });
            IgnoreIds.Add(new Field { Value = "3a64c95a-2b53-4b58-bd16-adbace5f7215", Name = "1" });
            IgnoreIds.Add(new Field { Value = "5e433ba2-9d4d-41dd-814e-ce27c62d56a2", Name = "1" });
            
            IgnoreIds.Add(new Field { Value = "72e42800-7112-4c34-ac8f-d70cd5873e82", Name = "1" });
            IgnoreIds.Add(new Field { Value = "09fc52b6-725a-4e71-a199-5055941b1353", Name = "1" });
            IgnoreIds.Add(new Field { Value = "19349923-554e-47f4-8a23-fdef40625bd1", Name = "1" });
            IgnoreIds.Add(new Field { Value = "99e41a7d-bdbb-45ad-b8cd-6bb547b645cb", Name = "1" });
           


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
