﻿using Business;
using System.Collections.Generic;

namespace CRM.ViewModels
{
    public class TradeHistoryFilterModel
    {
        public TradeHistoryFilterModel()
        {
            Orders = new List<AccountTradeHistory>();
        }

        public string Id { get; set; }
        public string Action { get; set; }
        public int CountOfPages { get; set; }
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }
        public int CurrentPage { get; set; }
        public string Coin { get; set; }
        public string Account { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TypeOfDate { get; set; }
        public ICollection<AccountTradeHistory> Orders { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal DesiredTotalProfit { get; set; }
        public int LossOrdersCount { get; set; }
        public int ProfitOrdersCount { get; set; }
        public int DesiredLossOrdersCount { get; set; }
        public int DesiredProfitOrdersCount { get; set; }
        public decimal LossOrdersSumm { get; set; }
        public decimal ProfitOrdersSumm { get; set; }
        public decimal DesiredLossOrdersSumm { get; set; }
        public decimal DesiredProfitOrdersSumm { get; set; }

        
    }
}
