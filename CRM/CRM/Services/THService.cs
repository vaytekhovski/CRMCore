using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance;
using CRM.Master;
using CRM.Models;
using CRM.Models.Binance;

namespace CRM.Services
{
    public class THService
    {
        private IEnumerable<Orders> orders = null;
        
        public List<AccountTradeHistory> AccountTradeHistories { get; private set; } = new List<AccountTradeHistory>();
        

        public void Load(string acc, string coin)
        {
            using (masterContext context = new masterContext())
            {
                if (coin != "all")
                {
                    orders = context.Orders.Where(x => x.Base == coin).ToList();
                    AddToTradeHistories();
                }
                else
                {
                    foreach (var _coin in DropDownFields.Coins)
                    {
                        if (_coin.Value == "all")
                            continue;

                        orders = context.Orders.Where(x => x.Base == _coin.Value).ToList();
                        AddToTradeHistories();
                    }
                }
            }

            AccountTradeHistories = AccountTradeHistories.Where(x => x.Time > DateTime.Parse("2019-04-05")).ToList();
            //UpdateBalance(acc);
            UpdateProfit();
           
            
        }

        private void AddToTradeHistories()
        {
            foreach (var item in orders)
            {
                if (item.ClosedAmount == 0)
                    continue;

                AccountTradeHistories.Add(new AccountTradeHistory
                {
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
