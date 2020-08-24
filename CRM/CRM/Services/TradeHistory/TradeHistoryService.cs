﻿using System;
using System.Linq;
using System.Security.Claims;
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

        public TradeHistoryModel Load(TradeHistoryFilter filter, HttpContext httpContext)
        {
            var model = new TradeHistoryModel();

            var token = httpContext.User.Identity.Name;

            model.Deals = datavisioAPI.GetListDeals(token).Result;

            if (filter.Coin != null)
                model.Deals.deals = model.Deals.deals.Where(x => x.@base == filter.Coin).ToArray();

            var ClosedDeals = model.Deals.deals.Where(x => x.outcome != 0).ToList();

            model.DepositProfit = 0;
            decimal Deposit = 0;

            if (httpContext.User.FindFirst(x=>x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == "Boss")
            {
                if (filter.StartDate < new DateTime(2020, 05, 16))
                {
                    Deposit = 200;
                    Deposit += ClosedDeals.Where(x => x.closed.Value > new DateTime(2020, 04, 01)).Where(x => x.closed.Value < filter.StartDate).Sum(x => x.profit.clean.amount);
                    var profitBefore1605 = ClosedDeals.Where(x => x.opened > filter.StartDate).Where(x => x.closed.Value < new DateTime(2020, 05, 16)).Sum(x => x.profit.clean.amount);
                    model.DepositProfit = (profitBefore1605 / Deposit) * 100;

                    Deposit = 1100;
                    var profitAfter1605 = ClosedDeals.Where(x => x.closed.Value >= new DateTime(2020, 05, 16)).Where(x => x.closed.Value < filter.EndDate).Sum(x => x.profit.clean.amount);
                    model.DepositProfit += (profitAfter1605 / Deposit) * 100;
                }
                else
                {
                    Deposit = 1100;
                    Deposit += ClosedDeals.Where(x => x.closed.Value >= new DateTime(2020, 05, 16)).Where(x => x.closed.Value <= filter.StartDate).Sum(x => x.profit.clean.amount);
                    var profitAfter1605 = ClosedDeals.Where(x => x.opened >= filter.StartDate).Where(x => x.closed.Value < filter.EndDate).Sum(x => x.profit.clean.amount);
                    model.DepositProfit += (profitAfter1605 / Deposit) * 100;
                }
            }
            else if(httpContext.User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == "test")
            {
                Deposit = 47;
                Deposit += ClosedDeals.Where(x => x.closed.Value >= new DateTime(2020, 05, 01)).Where(x => x.closed.Value <= filter.StartDate).Sum(x => x.profit.clean.amount);
                var profitAfter1605 = ClosedDeals.Where(x => x.opened >= filter.StartDate).Where(x => x.closed.Value < filter.EndDate).Sum(x => x.profit.clean.amount);
                model.DepositProfit += (profitAfter1605 / Deposit) * 100;
            }
            
            model.Deals.deals = model.Deals.deals.Where(x => x.opened >= filter.StartDate).Where(x => x.opened <= filter.EndDate).ToArray();

            var IgnoreIds = DropDownFields.GetIgnoreIds();

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

            //var candles = datavisioAPI.GetCandles(token, ViewModel.Coin).Result.ToList();
            //ViewModel.LastPrice = candles.Last().c;


            UpdateTotalProfit(model);
            UpdateCountOfLossAndProfitOrders(model);
            UpdateSummOfLossAndProfitOrders(model);


            model.Deals.deals = model.Deals.deals.OrderByDescending(x => x.opened).ToArray();

            model.CountOfElements = model.Deals.deals.Count();


            Signals Signals = datavisioAPI.GetSignals(token, "BTC", "raise").Result;
            Signal signalBTC = Signals.signals.FirstOrDefault();
            model.ProbaBuyBTC = signalBTC.value == 1 ? signalBTC.proba : 1m - signalBTC.proba;
            model.ProbaBuyBTC *= 100m;
            
            Signals = datavisioAPI.GetSignals(token, "ETH", "raise").Result;
            Signal signalETH = Signals.signals.FirstOrDefault();
            model.ProbaBuyETH = signalETH.value == 1 ? signalETH.proba : 1m - signalETH.proba;
            model.ProbaBuyETH *= 100m;
            
            Signals = datavisioAPI.GetSignals(token, "LTC", "raise").Result;
            Signal signalLTC = Signals.signals.FirstOrDefault();
            model.ProbaBuyLTC = signalLTC.value == 1 ? signalLTC.proba : 1m - signalLTC.proba;
            model.ProbaBuyLTC *= 100m;

            Signals = datavisioAPI.GetSignals(token, "XRP", "raise").Result;
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
