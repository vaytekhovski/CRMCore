using System;
using System.Collections.Generic;

namespace CRM.ViewModels.Balances
{
    public class BalancesModel
    {
        public string Account { get; set; }

        public List<Balance> AccountBalances { get; set; }


    }

    public class Balance 
    {
        public Balance(string _currency, string _amount, double _dollarAmount)
        {
            Currency = _currency;
            Amount = _amount;
            DollarAmount = _dollarAmount;
        }


        public string Currency { get; set; }
        public string Amount { get; set; }
        public double DollarAmount { get; set; }
    }

}
