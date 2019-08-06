﻿using System.Collections.Generic;

namespace Business
{
    public class TradeHistoryModel
    {
        public TradeHistoryModel()
        {

        }
        public List<AccountTradeHistory> AccountTradeHistories { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal DesiredTotalProfit { get; set; }
        public int CountOfPages { get; set; }
        public int CountOfElements { get; set; }

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