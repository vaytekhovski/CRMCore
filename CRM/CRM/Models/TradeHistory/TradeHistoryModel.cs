﻿using CRM.Models.Binance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.TradeHistory
{
    public class TradeHistoryModel
    {
        public TradeHistoryModel()
        {

        }
        public List<AccountTradeHistory> AccountTradeHistories { get; set; } = new List<AccountTradeHistory>();
        public double TotalProfit { get; set; }
        public int CountOfPages { get; set; }
    }
}