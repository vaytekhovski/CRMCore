using System.Collections.Generic;

namespace Business
{
    public class TradeHistoryModel
    {
        public TradeHistoryModel()
        {

        }
        public List<AccountTradeHistory> AccountTradeHistories { get; set; }
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

        public decimal TotalEnterTax { get; set; }

    }
}
