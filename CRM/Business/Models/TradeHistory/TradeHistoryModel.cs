﻿using System.Collections.Generic;
using Business.Models.DataVisioAPI;

namespace Business
{
    public class TradeHistoryModel
    {
        public TradeHistoryModel()
        {

        }
        //public List<AccountTradeHistory> AccountTradeHistories { get; set; }

        public ListDeals Deals { get; set; }

        public decimal TotalProfit { get; set; }
        public decimal TotalProfitWithoutFee { get; set; }
        public int CountOfPages { get; set; }
        public int CountOfElements { get; set; }

        public int LossOrdersCount { get; set; }
        public int ProfitOrdersCount { get; set; }

        public decimal LossOrdersSumm { get; set; }
        public decimal ProfitOrdersSumm { get; set; }
        
        public int LossOrdersCountWithoutFee { get; set; }
        public int ProfitOrdersCountWithoutFee { get; set; }

        public decimal LossOrdersSummWithoutFee { get; set; }
        public decimal ProfitOrdersSummWithoutFee { get; set; }

        public decimal ProbaBuyBTC { get; set; }
        public decimal ProbaBuyLTC { get; set; }
        public decimal ProbaBuyETH { get; set; }
        public decimal ProbaBuyXRP { get; set; }

        public decimal DepositProfit { get; set; }

        public decimal TotalEnterTax { get; set; }

    }
}
