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

        public decimal TotalProfit { get; set; }
        public decimal DesiredTotalProfit { get; set; }

        private DateTime StartDate;
        private DateTime EndDate;

        private readonly DateTime MinDate = new DateTime(2019, 04, 05);

        public Models.TradeHistory.TradeHistoryModel Load(string acc, string coin, DateTime startDate, DateTime endDate)
        {
            TotalProfit = 0;
            StartDate = startDate;
            StartDate = MinDate > StartDate ? MinDate : StartDate;
            EndDate = endDate;

            using (CRMContext context = new CRMContext())
            {
                AccountTradeHistories = context.AccountTradeHistories
                    .Where(x => coin == "all" ? true : x.Pair == coin)
                    .Where(x => acc == "Все аккаунты" ? true : x.Account == acc)
                    .Where(x => x.Time >= StartDate &&
                    x.Time <= EndDate).ToList();
            }

            UpdateTotalProfit();

            Models.TradeHistory.TradeHistoryModel tradeHistoryModel = new Models.TradeHistory.TradeHistoryModel();
            tradeHistoryModel.AccountTradeHistories = AccountTradeHistories.OrderByDescending(x => x.Time).ToList();
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
