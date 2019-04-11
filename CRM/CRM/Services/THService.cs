using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance;
using CRM.Master;
using CRM.Models;
using CRM.Models.Binance;
using CRM.Models.DropDown;

namespace CRM.Services
{
    public class THService
    {        
        public List<AccountTradeHistory> AccountTradeHistories { get; private set; } = new List<AccountTradeHistory>();

        public double Profit { get; set; }

        private List<int> IgnoreIds = new List<int>();

        public void Load(string acc, string coin)
        {
            InitializeIgnoreList();
            Profit = 0;

            var minDateTosearch = new DateTime(2019, 4, 5);
            using (masterContext context = new masterContext())
            {
                var orders = context.Orders.Where(x => 
                    (acc != "all" ? x.AccountId == acc : true) && 
                    (coin != "all" ? x.Base == coin : true) &&
                    x.TimeEnded > minDateTosearch).ToList();
                AddToTradeHistories(orders);
            }
            
            //UpdateBalance(acc);
            UpdateProfit();
        }

        private void InitializeIgnoreList()
        {
            IgnoreIds.Add(265);
            IgnoreIds.Add(266);
            IgnoreIds.Add(267);
            IgnoreIds.Add(268);
            IgnoreIds.Add(272);
            IgnoreIds.Add(273);
            IgnoreIds.Add(274);
            IgnoreIds.Add(275);
        }

        private string accountName(string accountId)
        {
            string account = "";

            switch (accountId)
            {
                case "bccd3ca1-0b5e-41ac-8233-3a35209912c7":
                    account = "POLONIEX 1-й";
                    break;
                case "8025d4bf-4af6-466f-b93c-5a807fd37f68":
                    account = "BINANCE 1-й";
                    break;
                case "9560eadf-74cf-4596-a7e5-bffcd201f6ec":
                    account = "BINANCE 2-й";
                    break;
            }

            return account;
        }

        private void AddToTradeHistories(ICollection<Orders> orders)
        {
            foreach (var item in orders)
            {
                if (item.ClosedAmount == 0) continue;
                
                var isIgnore = false;
                foreach (var id in IgnoreIds)
                {
                    if(item.Id == id)
                    {
                        isIgnore = true;
                        break;
                    }
                }

                if (isIgnore) continue;

                AccountTradeHistories.Add(new AccountTradeHistory
                {
                    Account = accountName(item.AccountId),
                    Time = item.TimeEnded,
                    Side = item.Side,
                    Pair = item.Base,
                    Price = item.Rate,
                    Quantity = item.ClosedAmount,
                    DollarQuantity = item.Rate * item.ClosedAmount,
                    BalanceUSDT = 0
                });
            }
        }

        private void UpdateBalance(string account)
        {
            // 28.02.2019
            // 1-й 5517.8
            // 2-й 518.49

            double currentBalace = 5525;//account.Balances.FirstOrDefault(x => x.Asset == "USDT").Free;
            switch (account)
            {
                case "a1":
                    currentBalace = 5525;
                    break;
                case "a2":
                    currentBalace = 520;
                    break;
                default:
                    break;
            }

            List<AccountTradeHistory> list = AccountTradeHistories.OrderBy(x => x.Time).ToList();
            foreach (var item in list)
            {
                if (item.Side == "BUY")
                {
                    currentBalace -= double.Parse(item.DollarQuantity.ToString());
                }
                else
                {
                    currentBalace += double.Parse(item.DollarQuantity.ToString());
                }
                
                item.BalanceUSDT = currentBalace;
            }

        }

        private void UpdateProfit()
        {
            foreach (var _coin in DropDownFields.Coins)
            {
                if (_coin.Value == "all")
                    continue;

                double profit = 0;
                var TH = AccountTradeHistories.Where(x => x.Pair == _coin.Value).OrderBy(x => x.Time).ToArray();

                for (int i = 0; i < TH.Count(); i++)
                {
                    //if (i != TH.Count() - 1)
                    //    if (TH[i + 1].Time - TH[i].Time > new TimeSpan(2, 0, 0))
                    //    {
                    //        profit = 0;
                    //        continue;
                    //    }

                    if (TH[i].Side == "buy")
                    {
                        profit -= (double)TH[i].DollarQuantity;
                    }
                    else
                    {
                        profit += (double)TH[i].DollarQuantity;
                    }

                    if ((TH[i].Side == "sell" && i == TH.Count() - 1) || (TH[i].Side == "sell" && TH[i + 1].Side == "buy"))
                    {
                        TH[i].Profit = profit;
                        Profit += profit;
                        profit = 0;
                    }
                }

                int j = 0;
                foreach (var item in AccountTradeHistories.Where(x => x.Pair == _coin.Value).OrderBy(x => x.Time))
                {
                    if (TH[j].Profit != 0)
                    {
                        item.Profit = TH[j].Profit;
                    }
                    j++;
                }
            }
        }


    }
}
