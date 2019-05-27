using CRM.Models.Master;
using System;

namespace CRM.Models.Binance
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
        public double DesiredQuantity { get; set; }
        public double DollarQuantity { get; set; }
        public double DesiredDollarQuantity { get; set; }
        public double Profit { get; set; }
        public double DesiredProfit { get; set; }
        public double PercentProfit { get; set; }
        public double DesiredPercentProfit { get; set; }
        public string SignalStr { get; set; }
    }
}
