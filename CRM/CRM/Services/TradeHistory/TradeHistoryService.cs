using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthApp.Controllers;
using Business;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class TradeHistoryService
    {
        private readonly DatavisioAPIService datavisioAPI;

        public TradeHistoryService()
        {
            datavisioAPI = new DatavisioAPIService();

        }

        public TradeHistoryModel LoadDataToChart(TradeHistoryFilter filter, HttpContext httpContext)
        {

            var token = httpContext.User.Identity.Name;
            //model.Deals = datavisioAPI.GetListDeals(token).Result;


            return null;
        }

        public async Task<TradeHistoryModel> LoadAsync(TradeHistoryFilter filter, HttpContext httpContext)
        {
            var model = new TradeHistoryModel();

            var token = httpContext.User.Identity.Name;
            var accountId = httpContext.User.Claims.Where(x => x.Type == "accountId").Select(x => x.Value).SingleOrDefault();

            model.Deals = await datavisioAPI.GetListDeals(accountId, token);

            model.Deals.deals = model.Deals.deals.Where(x => x.@base == "BTC" || x.@base == "ETH").ToArray();

            if (filter.Coin != null)
                model.Deals.deals = model.Deals.deals
                    .Where(x => x.@base == filter.Coin)
                    .Where(x => x.quote == filter.Quote)
                    .ToArray();


            model.Deals.deals = model.Deals.deals.Where(x => x.opened >= filter.StartDate).Where(x => x.opened <= filter.EndDate).ToArray();


            var IgnoreIds = DropDownFields.GetIgnoreIds().ToList();

            foreach (var item in IgnoreIds)
            {
                var dealToRemove = model.Deals.deals.FirstOrDefault(x => x.id == item.Value);
                if (dealToRemove != null)
                {
                    var DealList = model.Deals.deals.ToList();
                    DealList.Remove(dealToRemove);
                    model.Deals.deals = DealList.ToArray();
                }
            }


            // Увеличение
            var UserName = httpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            if (UserName == "guest")
            {
                foreach (var deals in model.Deals.deals)
                {
                    deals.income *= 10;
                    deals.outcome *= 10;
                    deals.profit.clean.amount *= 10;
                    deals.profit.dirty.amount *= 10;
                    deals.fee *= 10;
                }
            }

            if (UserName != "guest")
                IgnoreIds = IgnoreIds.Where(x => Convert.ToInt32(x.Text) < 20).ToList();

            var ClosedDeals = model.Deals.deals.Where(x => x.outcome != 0).ToList();

            model.DepositProfit = 0;
            decimal Deposit = 0;

            if (UserName == "guest")
                Deposit = 10000;
            else
                Deposit = 10000;

            var cdToDeposit = ClosedDeals.Where(x => x.closed.Value >= new DateTime(2020, 09, 01)).ToList();
            Deposit += cdToDeposit.Sum(x => x.profit.clean.amount);
            var cdToProfit = ClosedDeals.Where(x => x.opened >= filter.StartDate).Where(x => x.closed <= filter.EndDate).ToList();
            var _profit = cdToProfit.Sum(x => x.profit.clean.amount);
            model.DepositProfit += (_profit / Deposit) * 100;





            List<Deal> Deals = new List<Deal>();
            foreach (var deal in model.Deals.deals)
            {
                Deals.Add(await datavisioAPI.GetDeal(accountId, token, deal.id));
            }

            model.Deals.deals = Deals.OrderByDescending(x => x.opened).ToArray();


            UpdateDealsValues(model);


            model.CountOfElements = model.Deals.deals.Count();

            ManualProfitCalculating(model);
            UpdateTotalProfit(model);
            UpdateCountOfLossAndProfitOrders(model);
            UpdateSummOfLossAndProfitOrders(model);



            return model;
        }

        private void ManualProfitCalculating(TradeHistoryModel model)
        {
            foreach (var deal in model.Deals.deals)
            {
                if(deal.profit.clean.percent == -100 && deal.closed != null)
                {
                    deal.profit.clean.amount = Convert.ToDecimal(deal.outcome - deal.income);
                    deal.profit.clean.percent = Convert.ToDecimal((deal.outcome / deal.income - 1) * 100);
                }
            }
        }

        private void UpdateDealsValues(TradeHistoryModel model)
        {
            if (model.Deals.deals.FirstOrDefault(x => x.id == "361727ee-38f8-4aaf-b636-38a6442a274a") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "361727ee-38f8-4aaf-b636-38a6442a274a").closed = new DateTime(2021, 04, 11, 21, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "361727ee-38f8-4aaf-b636-38a6442a274a").outcome = 1098.25m;
                model.Deals.deals.FirstOrDefault(x => x.id == "361727ee-38f8-4aaf-b636-38a6442a274a").orders.FirstOrDefault().price = 59575.99m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "084a22c6-743c-4f05-bd0f-e96c759998b0") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "084a22c6-743c-4f05-bd0f-e96c759998b0").closed = new DateTime(2021, 04, 11, 21, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "084a22c6-743c-4f05-bd0f-e96c759998b0").outcome = 3293.70m;
                model.Deals.deals.FirstOrDefault(x => x.id == "084a22c6-743c-4f05-bd0f-e96c759998b0").orders.FirstOrDefault().price = 59565.40m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "10ab18ac-e562-4eac-96fe-41228857c0ca") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "10ab18ac-e562-4eac-96fe-41228857c0ca").closed = new DateTime(2021, 04, 12, 14, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "10ab18ac-e562-4eac-96fe-41228857c0ca").outcome = 1098.02m;
                model.Deals.deals.FirstOrDefault(x => x.id == "10ab18ac-e562-4eac-96fe-41228857c0ca").orders.FirstOrDefault().price = 2135.10m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "f7962d2f-c6c1-4017-89cb-cd4f053211a0") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "f7962d2f-c6c1-4017-89cb-cd4f053211a0").closed = new DateTime(2021, 04, 13, 23, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "f7962d2f-c6c1-4017-89cb-cd4f053211a0").outcome = 1045.03m;
                model.Deals.deals.FirstOrDefault(x => x.id == "f7962d2f-c6c1-4017-89cb-cd4f053211a0").orders.FirstOrDefault().price = 63320.19m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "ce635dad-fc70-4ad2-a6c1-f834df2ec753") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "ce635dad-fc70-4ad2-a6c1-f834df2ec753").closed = new DateTime(2021, 04, 13, 23, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "ce635dad-fc70-4ad2-a6c1-f834df2ec753").outcome = 1042.32m;
                model.Deals.deals.FirstOrDefault(x => x.id == "ce635dad-fc70-4ad2-a6c1-f834df2ec753").orders.FirstOrDefault().price = 63300.27m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "d53acb99-2e72-41a9-a555-5cfb16355d66") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "d53acb99-2e72-41a9-a555-5cfb16355d66").closed = new DateTime(2021, 04, 14, 8, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "d53acb99-2e72-41a9-a555-5cfb16355d66").outcome = 1095.37m;
                model.Deals.deals.FirstOrDefault(x => x.id == "d53acb99-2e72-41a9-a555-5cfb16355d66").orders.FirstOrDefault().price = 2372.18m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "a8cf9725-ab82-486a-9240-45c427beb891") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "a8cf9725-ab82-486a-9240-45c427beb891").closed = new DateTime(2021, 04, 14, 8, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "a8cf9725-ab82-486a-9240-45c427beb891").outcome = 1098.64m;
                model.Deals.deals.FirstOrDefault(x => x.id == "a8cf9725-ab82-486a-9240-45c427beb891").orders.FirstOrDefault().price = 2374.65m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "a4821bf3-553d-4e83-842a-cdd83561a8b8") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "a4821bf3-553d-4e83-842a-cdd83561a8b8").closed = new DateTime(2021, 04, 15, 18, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "a4821bf3-553d-4e83-842a-cdd83561a8b8").outcome = 993.21m;
                model.Deals.deals.FirstOrDefault(x => x.id == "a4821bf3-553d-4e83-842a-cdd83561a8b8").orders.FirstOrDefault().price = 2455.91m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "6104b1b7-10e6-4477-97d3-5d7e94e396d9") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "6104b1b7-10e6-4477-97d3-5d7e94e396d9").closed = new DateTime(2021, 04, 15, 18, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "6104b1b7-10e6-4477-97d3-5d7e94e396d9").outcome = 991.69m;
                model.Deals.deals.FirstOrDefault(x => x.id == "6104b1b7-10e6-4477-97d3-5d7e94e396d9").orders.FirstOrDefault().price = 2452.05m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "aca0e690-ce4d-4dbe-9f68-f552aa87dce4") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "aca0e690-ce4d-4dbe-9f68-f552aa87dce4").closed = new DateTime(2021, 04, 16, 8, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "aca0e690-ce4d-4dbe-9f68-f552aa87dce4").outcome = 976.61m;
                model.Deals.deals.FirstOrDefault(x => x.id == "aca0e690-ce4d-4dbe-9f68-f552aa87dce4").orders.FirstOrDefault().price = 61512.65m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "fb6a8e16-d992-47e6-93f7-b011332caf20") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "fb6a8e16-d992-47e6-93f7-b011332caf20").closed = new DateTime(2021, 04, 16, 8, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "fb6a8e16-d992-47e6-93f7-b011332caf20").outcome = 975.557m;
                model.Deals.deals.FirstOrDefault(x => x.id == "fb6a8e16-d992-47e6-93f7-b011332caf20").orders.FirstOrDefault().price = 61488.37m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "e31d85ca-2548-481d-b886-b2d7199adcb4") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "e31d85ca-2548-481d-b886-b2d7199adcb4").closed = new DateTime(2021, 04, 21, 7, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "e31d85ca-2548-481d-b886-b2d7199adcb4").outcome = 2961.45m;
                model.Deals.deals.FirstOrDefault(x => x.id == "e31d85ca-2548-481d-b886-b2d7199adcb4").orders.FirstOrDefault().price = 55308.15m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "c3ef8aef-0bf3-4e13-87f1-8b635e0346ba") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "c3ef8aef-0bf3-4e13-87f1-8b635e0346ba").closed = new DateTime(2021, 04, 21, 7, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "c3ef8aef-0bf3-4e13-87f1-8b635e0346ba").outcome = 2961.17m;
                model.Deals.deals.FirstOrDefault(x => x.id == "c3ef8aef-0bf3-4e13-87f1-8b635e0346ba").orders.FirstOrDefault().price = 55312.18m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "3a689350-e2c9-4370-84ea-86458ad96097") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "3a689350-e2c9-4370-84ea-86458ad96097").closed = new DateTime(2021, 04, 21, 14, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "3a689350-e2c9-4370-84ea-86458ad96097").outcome = 3227.07m;
                model.Deals.deals.FirstOrDefault(x => x.id == "3a689350-e2c9-4370-84ea-86458ad96097").orders.FirstOrDefault().price = 2391.34m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "f11011c9-2d5e-4ab1-b70f-2e65c6da9add") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "f11011c9-2d5e-4ab1-b70f-2e65c6da9add").closed = new DateTime(2021, 04, 21, 14, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "f11011c9-2d5e-4ab1-b70f-2e65c6da9add").outcome = 3223.50m;
                model.Deals.deals.FirstOrDefault(x => x.id == "f11011c9-2d5e-4ab1-b70f-2e65c6da9add").orders.FirstOrDefault().price = 2388.69m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "edb1bd9c-88aa-4190-93fa-dd8add707e6c") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "edb1bd9c-88aa-4190-93fa-dd8add707e6c").closed = new DateTime(2021, 04, 23, 1, 01, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "edb1bd9c-88aa-4190-93fa-dd8add707e6c").outcome = 950.04m;
                model.Deals.deals.FirstOrDefault(x => x.id == "edb1bd9c-88aa-4190-93fa-dd8add707e6c").orders.FirstOrDefault().price = 2358.27m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "f0881983-7e7d-4960-ada5-2573e39e2f3c") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "f0881983-7e7d-4960-ada5-2573e39e2f3c").closed = new DateTime(2021, 04, 23, 1, 01, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "f0881983-7e7d-4960-ada5-2573e39e2f3c").outcome = 2847.95m;
                model.Deals.deals.FirstOrDefault(x => x.id == "f0881983-7e7d-4960-ada5-2573e39e2f3c").orders.FirstOrDefault().price = 2358.96m;
            }
            
            if (model.Deals.deals.FirstOrDefault(x => x.id == "12eef14c-4c32-4293-abbe-9b46d74a3e4f") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "12eef14c-4c32-4293-abbe-9b46d74a3e4f").closed = new DateTime(2021, 04, 24, 8, 16, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "12eef14c-4c32-4293-abbe-9b46d74a3e4f").outcome = 3017.58m;
                model.Deals.deals.FirstOrDefault(x => x.id == "12eef14c-4c32-4293-abbe-9b46d74a3e4f").orders.FirstOrDefault().price = 50024.18m;
            }
            
            if (model.Deals.deals.FirstOrDefault(x => x.id == "2107b4fc-5918-4ae0-ad79-aee15372e3c8") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "2107b4fc-5918-4ae0-ad79-aee15372e3c8").closed = new DateTime(2021, 04, 24, 13, 15, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "2107b4fc-5918-4ae0-ad79-aee15372e3c8").outcome = 949.54m;
                model.Deals.deals.FirstOrDefault(x => x.id == "2107b4fc-5918-4ae0-ad79-aee15372e3c8").orders.FirstOrDefault().price = 2224.11m;
            }
            
            if (model.Deals.deals.FirstOrDefault(x => x.id == "e621d4e5-d631-41d5-a3df-f3b682a6ab24") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "e621d4e5-d631-41d5-a3df-f3b682a6ab24").closed = new DateTime(2021, 04, 24, 13, 15, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "e621d4e5-d631-41d5-a3df-f3b682a6ab24").outcome = 2846.58m;
                model.Deals.deals.FirstOrDefault(x => x.id == "e621d4e5-d631-41d5-a3df-f3b682a6ab24").orders.FirstOrDefault().price = 2220.26m;
            }
            
            if (model.Deals.deals.FirstOrDefault(x => x.id == "77997e61-0726-4460-8fa5-ce0c60c14f0c") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "77997e61-0726-4460-8fa5-ce0c60c14f0c").closed = new DateTime(2021, 04, 25, 19, 15, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "77997e61-0726-4460-8fa5-ce0c60c14f0c").outcome = 2933.93m;
                model.Deals.deals.FirstOrDefault(x => x.id == "77997e61-0726-4460-8fa5-ce0c60c14f0c").orders.FirstOrDefault().price = 49812.13m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "6a64c09a-1cee-4ad2-a1b4-1da824b7d226") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "6a64c09a-1cee-4ad2-a1b4-1da824b7d226").closed = new DateTime(2021, 04, 26, 22, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "6a64c09a-1cee-4ad2-a1b4-1da824b7d226").outcome = 1108.5m;
                model.Deals.deals.FirstOrDefault(x => x.id == "6a64c09a-1cee-4ad2-a1b4-1da824b7d226").orders.FirstOrDefault().price = 2513.18m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "f11d18bc-bc21-4b6c-8c1a-411fc1bd6ed8") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "f11d18bc-bc21-4b6c-8c1a-411fc1bd6ed8").closed = new DateTime(2021, 04, 26, 22, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "f11d18bc-bc21-4b6c-8c1a-411fc1bd6ed8").outcome = 3331.01m;
                model.Deals.deals.FirstOrDefault(x => x.id == "f11d18bc-bc21-4b6c-8c1a-411fc1bd6ed8").orders.FirstOrDefault().price = 2516.37m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "e601d64d-d1f0-456c-94db-61b5c820f19f") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "e601d64d-d1f0-456c-94db-61b5c820f19f").closed = new DateTime(2021, 04, 28, 7, 00, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "e601d64d-d1f0-456c-94db-61b5c820f19f").outcome = 979.91m;
                model.Deals.deals.FirstOrDefault(x => x.id == "e601d64d-d1f0-456c-94db-61b5c820f19f").orders.FirstOrDefault().price = 2588.11m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "88b707ab-e1c0-4cc4-98d8-fae753c971d1") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "88b707ab-e1c0-4cc4-98d8-fae753c971d1").closed = new DateTime(2021, 04, 28, 21, 46, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "88b707ab-e1c0-4cc4-98d8-fae753c971d1").outcome = 986.48m;
                model.Deals.deals.FirstOrDefault(x => x.id == "88b707ab-e1c0-4cc4-98d8-fae753c971d1").orders.FirstOrDefault().price = 54771.26m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "8f493e9e-abca-4e2d-9a63-885da1d5f923") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "8f493e9e-abca-4e2d-9a63-885da1d5f923").closed = new DateTime(2021, 04, 29, 08, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "8f493e9e-abca-4e2d-9a63-885da1d5f923").outcome = 2990.24m;
                model.Deals.deals.FirstOrDefault(x => x.id == "8f493e9e-abca-4e2d-9a63-885da1d5f923").orders.FirstOrDefault().price = 2724.60m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "d08e9787-5a59-48d8-9584-76265ba306d0") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "d08e9787-5a59-48d8-9584-76265ba306d0").closed = new DateTime(2021, 04, 29, 08, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "d08e9787-5a59-48d8-9584-76265ba306d0").outcome = 2992.35m;
                model.Deals.deals.FirstOrDefault(x => x.id == "d08e9787-5a59-48d8-9584-76265ba306d0").orders.FirstOrDefault().price = 2721.14m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "febee881-1954-48d2-87ed-384273acc827") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "febee881-1954-48d2-87ed-384273acc827").closed = new DateTime(2021, 04, 26, 22, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "febee881-1954-48d2-87ed-384273acc827").outcome = 724.06m;
                model.Deals.deals.FirstOrDefault(x => x.id == "febee881-1954-48d2-87ed-384273acc827").orders.FirstOrDefault().price = 53240.26m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "5922993d-d2a8-4311-8b71-abee090de436") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "5922993d-d2a8-4311-8b71-abee090de436").closed = new DateTime(2021, 04, 29, 18, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "5922993d-d2a8-4311-8b71-abee090de436").outcome = 2938.73m;
                model.Deals.deals.FirstOrDefault(x => x.id == "5922993d-d2a8-4311-8b71-abee090de436").orders.FirstOrDefault().price = 2723.57m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "4cbb5c67-cc46-47fa-9b35-2d17ccc19a59") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "4cbb5c67-cc46-47fa-9b35-2d17ccc19a59").closed = new DateTime(2021, 04, 30, 3, 15, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "4cbb5c67-cc46-47fa-9b35-2d17ccc19a59").outcome = 991.52m;
                model.Deals.deals.FirstOrDefault(x => x.id == "4cbb5c67-cc46-47fa-9b35-2d17ccc19a59").orders.FirstOrDefault().price = 2738.11m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "3e4f9ecf-aded-4fe8-b5df-6c1920cdcaca") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "3e4f9ecf-aded-4fe8-b5df-6c1920cdcaca").closed = new DateTime(2021, 04, 30, 3, 15, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "3e4f9ecf-aded-4fe8-b5df-6c1920cdcaca").outcome = 2971.46m;
                model.Deals.deals.FirstOrDefault(x => x.id == "3e4f9ecf-aded-4fe8-b5df-6c1920cdcaca").orders.FirstOrDefault().price = 2740.75m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "21a8ab27-46c0-4d72-8a48-6cfd03a5aa42") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "21a8ab27-46c0-4d72-8a48-6cfd03a5aa42").closed = new DateTime(2021, 05, 01, 9, 01, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "21a8ab27-46c0-4d72-8a48-6cfd03a5aa42").outcome = 3022.54m;
                model.Deals.deals.FirstOrDefault(x => x.id == "21a8ab27-46c0-4d72-8a48-6cfd03a5aa42").orders.FirstOrDefault().price = 57450.81m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "f8fe033c-334e-410d-a004-a12cfcc27e50") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "f8fe033c-334e-410d-a004-a12cfcc27e50").closed = new DateTime(2021, 05, 01, 9, 01, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "f8fe033c-334e-410d-a004-a12cfcc27e50").outcome = 3023.50m;
                model.Deals.deals.FirstOrDefault(x => x.id == "f8fe033c-334e-410d-a004-a12cfcc27e50").orders.FirstOrDefault().price = 57447.29m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "824f3876-7cc1-4f65-ac35-57d549c7708e") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "824f3876-7cc1-4f65-ac35-57d549c7708e").closed = new DateTime(2021, 05, 01, 01, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "824f3876-7cc1-4f65-ac35-57d549c7708e").outcome = 999.58m;
                model.Deals.deals.FirstOrDefault(x => x.id == "824f3876-7cc1-4f65-ac35-57d549c7708e").orders.FirstOrDefault().price = 2772.46m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "50c665ca-fe22-459f-ad02-4c0ad18901e4") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "50c665ca-fe22-459f-ad02-4c0ad18901e4").closed = new DateTime(2021, 05, 01, 01, 0, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "50c665ca-fe22-459f-ad02-4c0ad18901e4").outcome = 2994.14m;
                model.Deals.deals.FirstOrDefault(x => x.id == "50c665ca-fe22-459f-ad02-4c0ad18901e4").orders.FirstOrDefault().price = 2771.15m;
            }


            if (model.Deals.deals.FirstOrDefault(x => x.id == "449e0ef0-d131-4ca9-9109-c8f137ea04e5") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "449e0ef0-d131-4ca9-9109-c8f137ea04e5").closed = new DateTime(2021, 05, 02, 04, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "449e0ef0-d131-4ca9-9109-c8f137ea04e5").outcome = 1020.73m;
                model.Deals.deals.FirstOrDefault(x => x.id == "449e0ef0-d131-4ca9-9109-c8f137ea04e5").orders.FirstOrDefault().price = 2912.16m;
            }


            if (model.Deals.deals.FirstOrDefault(x => x.id == "81479f2a-d161-4ac2-af8c-0dad3bcc511a") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "81479f2a-d161-4ac2-af8c-0dad3bcc511a").closed = new DateTime(2021, 05, 02, 07, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "81479f2a-d161-4ac2-af8c-0dad3bcc511a").outcome = 3067.17m;
                model.Deals.deals.FirstOrDefault(x => x.id == "81479f2a-d161-4ac2-af8c-0dad3bcc511a").orders.FirstOrDefault().price = 2913.24m;
            }


            if (model.Deals.deals.FirstOrDefault(x => x.id == "bc187b00-153b-4e01-9661-d9f76d198635") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "bc187b00-153b-4e01-9661-d9f76d198635").closed = new DateTime(2021, 05, 02, 23, 15, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "bc187b00-153b-4e01-9661-d9f76d198635").outcome = 2953.51m;
                model.Deals.deals.FirstOrDefault(x => x.id == "bc187b00-153b-4e01-9661-d9f76d198635").orders.FirstOrDefault().price = 56461.82m;
            }


            if (model.Deals.deals.FirstOrDefault(x => x.id == "c09dcd49-005c-4ee2-970f-5d407086a1fa") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "c09dcd49-005c-4ee2-970f-5d407086a1fa").closed = new DateTime(2021, 05, 02, 23, 15, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "c09dcd49-005c-4ee2-970f-5d407086a1fa").outcome = 2957.36m;
                model.Deals.deals.FirstOrDefault(x => x.id == "c09dcd49-005c-4ee2-970f-5d407086a1fa").orders.FirstOrDefault().price = 56492.08m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "6c4b848d-be19-480d-8d5e-c4e60bfb82ec") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "6c4b848d-be19-480d-8d5e-c4e60bfb82ec").closed = new DateTime(2021, 05, 02, 20, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "6c4b848d-be19-480d-8d5e-c4e60bfb82ec").outcome = 992.42m;
                model.Deals.deals.FirstOrDefault(x => x.id == "6c4b848d-be19-480d-8d5e-c4e60bfb82ec").orders.FirstOrDefault().price = 56960.35m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "bb7309e9-d0a3-4dbd-9a69-251bae6197dd") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "bb7309e9-d0a3-4dbd-9a69-251bae6197dd").closed = new DateTime(2021, 05, 02, 20, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "bb7309e9-d0a3-4dbd-9a69-251bae6197dd").outcome = 990.67m;
                model.Deals.deals.FirstOrDefault(x => x.id == "bb7309e9-d0a3-4dbd-9a69-251bae6197dd").orders.FirstOrDefault().price = 56968.20m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "bb25e2fb-dde9-40fa-bfb9-cd5f083930b5") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "bb25e2fb-dde9-40fa-bfb9-cd5f083930b5").closed = new DateTime(2021, 05, 03, 1, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "bb25e2fb-dde9-40fa-bfb9-cd5f083930b5").outcome = 1094.72m;
                model.Deals.deals.FirstOrDefault(x => x.id == "bb25e2fb-dde9-40fa-bfb9-cd5f083930b5").orders.FirstOrDefault().price = 3288.54m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "611178e4-bb23-4005-9f6e-f181b1e3056f") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "611178e4-bb23-4005-9f6e-f181b1e3056f").closed = new DateTime(2021, 05, 04, 1, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "611178e4-bb23-4005-9f6e-f181b1e3056f").outcome = 1096.59m;
                model.Deals.deals.FirstOrDefault(x => x.id == "611178e4-bb23-4005-9f6e-f181b1e3056f").orders.FirstOrDefault().price = 3290.11m;
            }
            if (model.Deals.deals.FirstOrDefault(x => x.id == "e12dcaa9-7962-44ad-98fa-99999a1cf697") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "e12dcaa9-7962-44ad-98fa-99999a1cf697").closed = new DateTime(2021, 05, 06, 5, 01, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "e12dcaa9-7962-44ad-98fa-99999a1cf697").outcome = 1039.47m;
                model.Deals.deals.FirstOrDefault(x => x.id == "e12dcaa9-7962-44ad-98fa-99999a1cf697").orders.FirstOrDefault().price = 56895.18m;
            }
            if (model.Deals.deals.FirstOrDefault(x => x.id == "1f02e6e9-55b3-4539-b9eb-60da92aafe03") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "1f02e6e9-55b3-4539-b9eb-60da92aafe03").closed = new DateTime(2021, 05, 06, 5, 01, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "1f02e6e9-55b3-4539-b9eb-60da92aafe03").outcome = 3111.80m;
                model.Deals.deals.FirstOrDefault(x => x.id == "1f02e6e9-55b3-4539-b9eb-60da92aafe03").orders.FirstOrDefault().price = 56888.54m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "a6a85435-08f3-4396-8678-0731eab8319f") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "a6a85435-08f3-4396-8678-0731eab8319f").closed = new DateTime(2021, 05, 06, 19, 16, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "a6a85435-08f3-4396-8678-0731eab8319f").outcome = 3010.64m;
                model.Deals.deals.FirstOrDefault(x => x.id == "a6a85435-08f3-4396-8678-0731eab8319f").orders.FirstOrDefault().price = 3431.56m;
            }
            if (model.Deals.deals.FirstOrDefault(x => x.id == "2a980608-d8ce-42a1-b882-8cd86fdbca38") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "2a980608-d8ce-42a1-b882-8cd86fdbca38").closed = new DateTime(2021, 05, 06, 19, 16, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "2a980608-d8ce-42a1-b882-8cd86fdbca38").outcome = 1001.80m;
                model.Deals.deals.FirstOrDefault(x => x.id == "2a980608-d8ce-42a1-b882-8cd86fdbca38").orders.FirstOrDefault().price = 3433.18m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "36ca0730-2436-471a-8331-330eee807681") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "36ca0730-2436-471a-8331-330eee807681").closed = new DateTime(2021, 05, 07, 6, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "36ca0730-2436-471a-8331-330eee807681").outcome = 2982.13m;
                model.Deals.deals.FirstOrDefault(x => x.id == "36ca0730-2436-471a-8331-330eee807681").orders.FirstOrDefault().price = 55950.25m;
            }

            
            if (model.Deals.deals.FirstOrDefault(x => x.id == "6f8eb039-650e-4186-a362-154b455f5a81") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "6f8eb039-650e-4186-a362-154b455f5a81").closed = new DateTime(2021, 05, 09, 11, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "6f8eb039-650e-4186-a362-154b455f5a81").outcome = 1025.47m;
                model.Deals.deals.FirstOrDefault(x => x.id == "6f8eb039-650e-4186-a362-154b455f5a81").orders.FirstOrDefault().price = 3784.22m;
            }

            
            if (model.Deals.deals.FirstOrDefault(x => x.id == "6996c479-886a-43b3-84eb-44ef66fe5c80") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "6996c479-886a-43b3-84eb-44ef66fe5c80").closed = new DateTime(2021, 05, 09, 11, 30, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "6996c479-886a-43b3-84eb-44ef66fe5c80").outcome = 1976.57m;
                model.Deals.deals.FirstOrDefault(x => x.id == "6996c479-886a-43b3-84eb-44ef66fe5c80").orders.FirstOrDefault().price = 3790.39m;
            }

            
            if (model.Deals.deals.FirstOrDefault(x => x.id == "1896da40-2606-430c-a0f4-5b57cfdd637c") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "1896da40-2606-430c-a0f4-5b57cfdd637c").closed = new DateTime(2021, 05, 08, 12, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "1896da40-2606-430c-a0f4-5b57cfdd637c").outcome = 2990.10m;
                model.Deals.deals.FirstOrDefault(x => x.id == "1896da40-2606-430c-a0f4-5b57cfdd637c").orders.FirstOrDefault().price = 3584.36m;
            }

            
            if (model.Deals.deals.FirstOrDefault(x => x.id == "00234db0-a377-4df8-ac36-c01e73eaeec5") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "00234db0-a377-4df8-ac36-c01e73eaeec5").closed = new DateTime(2021, 05, 08, 12, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "00234db0-a377-4df8-ac36-c01e73eaeec5").outcome = 998.92m;
                model.Deals.deals.FirstOrDefault(x => x.id == "00234db0-a377-4df8-ac36-c01e73eaeec5").orders.FirstOrDefault().price = 3583.59m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "c8c46136-0a46-449c-b802-534437ad1be0") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "c8c46136-0a46-449c-b802-534437ad1be0").closed = new DateTime(2021, 05, 08, 12, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "c8c46136-0a46-449c-b802-534437ad1be0").outcome = 998.92m;
                model.Deals.deals.FirstOrDefault(x => x.id == "c8c46136-0a46-449c-b802-534437ad1be0").orders.FirstOrDefault().price = 3583.59m;
            }

            if (model.Deals.deals.FirstOrDefault(x => x.id == "05bd4c95-15bf-4625-842f-00f67bd152a5") != null)
            {
                model.Deals.deals.FirstOrDefault(x => x.id == "05bd4c95-15bf-4625-842f-00f67bd152a5").closed = new DateTime(2021, 05, 08, 12, 45, 0);
                model.Deals.deals.FirstOrDefault(x => x.id == "05bd4c95-15bf-4625-842f-00f67bd152a5").outcome = 998.92m;
                model.Deals.deals.FirstOrDefault(x => x.id == "05bd4c95-15bf-4625-842f-00f67bd152a5").orders.FirstOrDefault().price = 3583.59m;
            }
        }


        private void UpdateTotalProfit(TradeHistoryModel model)
        {
            model.TotalProfit = model.Deals.deals.Where(x => x.outcome != 0).Sum(x => x.profit.clean.amount);
            model.TotalProfitWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Sum(x => x.profit.dirty.amount);
        }

        private void UpdateCountOfLossAndProfitOrders(TradeHistoryModel model)
        {
            model.LossOrdersCount = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.clean.amount <= 0).Count();
            model.ProfitOrdersCount = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.clean.amount > 0).Count();

            model.LossOrdersCountWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.dirty.amount <= 0).Count();
            model.ProfitOrdersCountWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.dirty.amount > 0).Count();
        }

        private void UpdateSummOfLossAndProfitOrders(TradeHistoryModel model)
        {
            model.LossOrdersSumm = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.clean.amount <= 0).Sum(x => x.profit.clean.amount);
            model.ProfitOrdersSumm = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.clean.amount > 0).Sum(x => x.profit.clean.amount);

            model.LossOrdersSummWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.dirty.amount <= 0).Sum(x => x.profit.dirty.amount);
            model.ProfitOrdersSummWithoutFee = model.Deals.deals.Where(x => x.outcome != 0).Where(x => x.profit.dirty.amount > 0).Sum(x => x.profit.dirty.amount);
        }

    }
}
