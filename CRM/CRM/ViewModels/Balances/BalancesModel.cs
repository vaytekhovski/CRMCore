using System;
using System.Collections.Generic;
using System.Linq;
using Business.Models.DataVisioAPI;
using CRM.Services.Balances;

namespace CRM.ViewModels.Balances
{
    public class BalancesModel
    {
        public BalancesModel()
        {
            AccountBalances = new List<Balance>();
        }

        public string Account { get; set; }

        public List<Balance> AccountBalances { get; set; }

        public void InsterBalance(WalletCurrency walletCurrency)
        {

            Balance balance = new Balance(walletCurrency.coin, walletCurrency.free, walletCurrency.used, walletCurrency.total);

            balance.buttonDisabled = balance.Free > 0.001M ? "false" : "true";

            AccountBalances.Add(balance);
        }
    }

    public class Balance 
    {
        public Balance(string currency, double free, double used, double total)
        {
            Currency = currency;
            Free = Convert.ToDecimal(free);
            Used = Convert.ToDecimal(used);
            Total = Convert.ToDecimal(total);
        }


        public string Currency { get; set; }
        public decimal Free { get; set; }
        public decimal Used { get; set; }
        public decimal Total { get; set; }
        public string buttonDisabled { get; set; }
    }

}
