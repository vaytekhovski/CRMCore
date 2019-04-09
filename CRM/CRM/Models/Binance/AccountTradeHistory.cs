using System;

namespace CRM.Models.Binance
{
    public class AccountTradeHistory
    {
        public DateTime Time { get; set; }
        public string Side { get; set; }
        public string Pair { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal DollarQuantity { get; set; }
        public double BalanceUSDT { get; set; }
        public double Profit { get; set; }
    }
}
