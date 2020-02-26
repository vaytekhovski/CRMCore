using System;

namespace Business
{
    public class AccountTradeHistory
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public DateTime Time { get; set; }
        public string Side { get; set; }
        public string Pair { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal DesiredQuantity { get; set; }
        public decimal DollarQuantity { get; set; }
        public decimal DesiredDollarQuantity { get; set; }
        public decimal Profit { get; set; }
        public decimal DesiredProfit { get; set; }
        public decimal PercentProfit { get; set; }
        public decimal DesiredPercentProfit { get; set; }
        public decimal Fee { get; set; }
        public string SignalStr { get; set; }
        public decimal LowerBand { get; set; }
        
    }
}
