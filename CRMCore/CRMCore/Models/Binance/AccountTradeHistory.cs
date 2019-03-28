using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Models.Binance
{
    public class AccountTradeHistory
    {
        public DateTime Time { get; set; }
        public string Side { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal DollarQuantity { get; set; }
        public string CommissionAsset { get; set; }
        public decimal Commission { get; set; }
        public decimal BalanceUSDT { get; set; }
    }
}
