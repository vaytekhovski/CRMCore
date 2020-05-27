using System;
using System.Linq;
using Business;
using Business.Contexts;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using Microsoft.AspNetCore.Http;
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
            var model = new TradeHistoryModel();

            var token = datavisioAPI.Authorization(Convert.ToInt32(httpContext.User.Identity.Name)).Result;

            model.Deals = datavisioAPI.GetListDeals(token).Result;


            return model;
        }

        public TradeHistoryModel Load(TradeHistoryFilter filter, HttpContext httpContext)
        {
            var model = new TradeHistoryModel();

            var token = datavisioAPI.Authorization(Convert.ToInt32(httpContext.User.Identity.Name)).Result;

            model.Deals = datavisioAPI.GetListDeals(token).Result;

            if (filter.Coin != null)
                model.Deals.deals = model.Deals.deals.Where(x => x.@base == filter.Coin).ToArray();

            model.Deals.deals = model.Deals.deals.Where(x => x.opened >= filter.StartDate).Where(x => x.opened <= filter.EndDate).ToArray();

            
            System.Collections.Generic.List<IgnoreIds> IgnoreList = new System.Collections.Generic.List<IgnoreIds>();
            using (BasicContext db = new BasicContext())
            {
                IgnoreList = db.IgnoreIds.ToList();

            }

            foreach (var item in IgnoreList)
            {
                var dealToRemove = model.Deals.deals.First(x => x.id == item.OrderId);
                if (dealToRemove != null) {
                    var DealList = model.Deals.deals.ToList();
                    DealList.Remove(dealToRemove);
                    model.Deals.deals = DealList.ToArray();
                        }
            }
            


            UpdateTotalProfit(model);
            UpdateCountOfLossAndProfitOrders(model);
            UpdateSummOfLossAndProfitOrders(model);


            model.Deals.deals = model.Deals.deals.OrderByDescending(x => x.opened).ToArray();

            model.CountOfElements = model.Deals.deals.Count();


            Signals Signals = datavisioAPI.GetSignals(token, "BTC").Result;
            Signal signalBTC = Signals.signals.FirstOrDefault();
            model.ProbaBuyBTC = signalBTC.value == 1 ? signalBTC.proba : 1m - signalBTC.proba;
            model.ProbaBuyBTC *= 100m;
            
            Signals = datavisioAPI.GetSignals(token, "ETH").Result;
            Signal signalETH = Signals.signals.FirstOrDefault();
            model.ProbaBuyETH = signalETH.value == 1 ? signalETH.proba : 1m - signalETH.proba;
            model.ProbaBuyETH *= 100m;
            
            Signals = datavisioAPI.GetSignals(token, "LTC").Result;
            Signal signalLTC = Signals.signals.FirstOrDefault();
            model.ProbaBuyLTC = signalLTC.value == 1 ? signalLTC.proba : 1m - signalLTC.proba;
            model.ProbaBuyLTC *= 100m;

            Signals = datavisioAPI.GetSignals(token, "XRP").Result;
            Signal signalXRP = Signals.signals.FirstOrDefault();
            model.ProbaBuyXRP = signalXRP.value == 1 ? signalXRP.proba : 1m - signalXRP.proba;
            model.ProbaBuyXRP *= 100m;



            return model;
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
