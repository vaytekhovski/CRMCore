﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Dashboard
{
    public class DashboardStatisticViewModel
    {
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal TotalProfit { get; set; }
        public int LossOrdersCount { get; set; }
        public int ProfitOrdersCount { get; set; }
        public decimal LossOrdersSumm { get; set; }
        public decimal ProfitOrdersSumm { get; set; }
        public decimal RPL { get; set; }
        public decimal AP { get; set; }
        public decimal AL { get; set; }
        public decimal AR { get; set; }
        public decimal RAPAL { get; set; }
        public decimal? MIDD { get; set; }
        public decimal? Dmin { get; set; }
        public decimal? R { get; set; }
        public decimal? RF { get; set; }
        public decimal PF { get; set; }
        public decimal APF { get; set; }
        public decimal SharpeRatio { get; set; }
        public string DepositProfit { get; set; }
        public string DepositProfitAfterDecimal { get; set; }
    }
}
