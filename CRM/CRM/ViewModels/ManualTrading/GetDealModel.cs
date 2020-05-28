using System;
using Business.Models.DataVisioAPI;
using CRM.ViewModels.Balances;

namespace CRM.ViewModels.ManualTrading
{
    public class GetDealModel
    {
        public GetDealModel()
        {
        }

        public Deal Deal { get; set; }
        public decimal LastPrice { get; set; }
        public BalancesModel balancesModel { get; set; }
        public string BuyAmount { get; set; }

    }
}
