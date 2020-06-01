using Business;
using Business.Models.DataVisioAPI;
using System.Collections.Generic;

namespace CRM.ViewModels
{
    public class TradeHistoryFilterModel
    {
        public TradeHistoryFilterModel()
        {
        }

        public string Id { get; set; }
        public string Action { get; set; }
        public int CountOfPages { get; set; }
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }
        public int CurrentPage { get; set; }
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TypeOfDate { get; set; }
        //public ICollection<AccountTradeHistory> Orders { get; set; }
        public ListDeals Deals { get; set; }
        public decimal LastPrice { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalProfitWithoutFee { get; set; }
        public int LossOrdersCount { get; set; }
        public int ProfitOrdersCount { get; set; }
        public int LossOrdersCountWithoutFee { get; set; }
        public int ProfitOrdersCountWithoutFee { get; set; }
        public decimal LossOrdersSumm { get; set; }
        public decimal ProfitOrdersSumm { get; set; }
        public decimal LossOrdersSummWithoutFee { get; set; }
        public decimal ProfitOrdersSummWithoutFee { get; set; }
        public string Algorithm { get; set; }
        public decimal TotalEnterTax { get; set; }

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
        public decimal ProfitAverage { get; set; }
        public decimal LossAverage { get; set; }

        public decimal ProbaBuyBTC { get; set; }
        public decimal ProbaBuyLTC { get; set; }
        public decimal ProbaBuyETH { get; set; }
        public decimal ProbaBuyXRP { get; set; }


        public string ProbaBuyBTCstr { get; set; }
        public string ProbaBuyLTCstr { get; set; }
        public string ProbaBuyETHstr { get; set; }
        public string ProbaBuyXRPstr { get; set; }


        public decimal CompoundInterest { get; set; }
        public decimal CompoundInterestWithoutFee { get; set; }



    }
}
