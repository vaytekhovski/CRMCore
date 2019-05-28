using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CRM.Master;
using CRM.Models;
using CRM.Models.Binance;
using CRM.Models.Database;
using CRM.Models.Master;
using CRM.Services.Balances;

namespace CRM.Services
{
    public class THService
    {        
        public THService()
        {

        }

        public List<AccountTradeHistory> AccountTradeHistories { get; private set; } = new List<AccountTradeHistory>();

        public double TotalProfit { get; set; }
        public double DesiredTotalProfit { get; set; }
        public int CountOfPages { get; set; }

        private DateTime StartDate;
        private DateTime EndDate;

        private readonly DateTime MinDate = new DateTime(2019, 04, 05);

        public Models.TradeHistory.TradeHistoryModel Load(string acc, string coin, DateTime startDate, DateTime endDate)
        {
            TotalProfit = 0;
            StartDate = startDate.AddDays(1);
            StartDate = MinDate > StartDate ? MinDate : StartDate;
            EndDate = endDate;

            using (CRMContext context = new CRMContext())
            {
                AccountTradeHistories = context.AccountTradeHistories.Where(x =>
                    acc != "Все аккаунты" ? x.Account == acc : true &&
                    coin != "all" ? x.Pair == coin : true &&
                    x.Time >= StartDate &&
                    x.Time <= EndDate).ToList();
            }

            CountOfPages = (int)Math.Ceiling((decimal)((double)AccountTradeHistories.Count / 100));
            UpdateTotalProfit();

            Models.TradeHistory.TradeHistoryModel tradeHistoryModel = new Models.TradeHistory.TradeHistoryModel();
            tradeHistoryModel.AccountTradeHistories = AccountTradeHistories.OrderByDescending(x => x.Time).ToList();
            tradeHistoryModel.CountOfPages = CountOfPages;
            tradeHistoryModel.TotalProfit = TotalProfit;
            tradeHistoryModel.DesiredTotalProfit = DesiredTotalProfit;

            return tradeHistoryModel;
        }

        private void UpdateTotalProfit()
        {
            foreach (var item in AccountTradeHistories.Where(x => x.Profit != 0))
            {
                TotalProfit += item.Profit;
                DesiredTotalProfit += item.DesiredProfit;
            }
        }

    }
}
