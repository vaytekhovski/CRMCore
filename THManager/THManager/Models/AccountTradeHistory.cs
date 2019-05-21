using System;
using System.Collections.Generic;
using System.Text;

namespace THManager.Models
{
    public class AccountTradeHistory
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public DateTime Time { get; set; }
        public string Side { get; set; }
        public string Pair { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public decimal DollarQuantity { get; set; }
        public double BalanceUSDT { get; set; }
        public double Profit { get; set; }
        public double TotalProfit { get; set; }
        public double PercentProfit { get; set; }
        public string SignalStr { get; set; }
    }
}
