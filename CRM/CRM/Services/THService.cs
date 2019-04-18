using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Binance;
using CRM.Master;
using CRM.Models;
using CRM.Models.Binance;
using CRM.Models.Database;
using CRM.Models.DropDown;

namespace CRM.Services
{
    public class THService
    {        
        public List<AccountTradeHistory> AccountTradeHistories { get; private set; } = new List<AccountTradeHistory>();

        public double TotalProfit { get; set; }
        public double TotalPercentProfit { get; set; }

        private List<int> IgnoreIds = new List<int>();

        private List<ExchangeKey> ExchangeKeys { get; set; }

        private DateTime StartDate;
        private DateTime EndDate;

        public void Load(string acc, string coin, DateTime startDate, DateTime endDate)
        {
            InitializeIgnoreList();
            TotalProfit = 0;

            StartDate = startDate;
            EndDate = endDate;

            InitializeExchangeKeys();

            DateTime MinDate = new DateTime(2019, 04, 05);

            using (masterContext context = new masterContext())
            {
                var orders = context.Orders.Where(x => 
                    (acc != "all" ? x.AccountId == acc : true) && 
                    (coin != "all" ? x.Base == coin : true) &&
                    x.TimeEnded >= MinDate).ToList();
                AddToTradeHistories(orders);
            }
            
            UpdateProfit();
        }

        private void InitializeExchangeKeys()
        {
            using (UserContext context = new UserContext())
            {
                ExchangeKeys = context.ExchangeKeys.ToList();
            }
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
            IgnoreIds.Add(312);
        }

        private string AccountName(string accountId)
        {
            return ExchangeKeys.FirstOrDefault(x => x.AccountId == accountId).Name;
        }
        
        private void AddToTradeHistories(ICollection<Orders> orders)
        {
            foreach (var item in orders)
            {
                if (item.ClosedAmount == 0) continue;
                
                bool isIgnore = false;
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
                    Account = AccountName(item.AccountId),
                    Time = item.TimeEnded.AddHours(3),
                    Side = item.Side,
                    Pair = item.Base,
                    Price = item.Rate,
                    Quantity = item.ClosedAmount,
                    DollarQuantity = item.Rate * item.ClosedAmount,
                    BalanceUSDT = 0
                });
            }
        }

        
        private void UpdateProfit()
        {
            foreach (var _coin in DropDownFields.Coins)
            {
                foreach (var _acc in ExchangeKeys)
                {
                    if (_acc.AccountId == "all")
                        continue;

                    if (_coin.Value == "all")
                        break;
                    

                    double profit = 0;
                    double buyAmount = 0;

                    var TH = AccountTradeHistories.Where(x => x.Pair == _coin.Value && x.Account == AccountName(_acc.AccountId)).OrderBy(x => x.Time).ToArray();

                    for (int i = 0; i < TH.Count(); i++)
                    {
                        buyAmount += TH[i].Side == "buy" ? (double)TH[i].DollarQuantity : 0;
                        
                        profit += TH[i].Side == "buy" ? ((double)TH[i].DollarQuantity) * -1 : (double)TH[i].DollarQuantity;
                        
                        if ((TH[i].Side == "sell" && i == TH.Count() - 1) || (TH[i].Side == "sell" && TH[i + 1].Side == "buy"))
                        {
                            TH[i].Profit = profit;
                            TH[i].PercentProfit = profit / ((buyAmount + (buyAmount + profit)) / 2) * 100;
                            profit = 0;
                            buyAmount = 0;
                        }
                    }

                    int j = 0;
                    

                    foreach (var item in AccountTradeHistories.Where(x => x.Pair == _coin.Value && x.Account == AccountName(_acc.AccountId)).OrderBy(x => x.Time))
                    {
                        if (TH[j].Profit != 0)
                        {
                            item.Profit = TH[j].Profit;
                            item.PercentProfit = TH[j].PercentProfit;
                        }
                        j++;
                    }


                }
                if (_coin.Value == "all")
                    continue;
            }

            foreach (var item in AccountTradeHistories.Where(x => x.Profit != 0 && x.Time >= StartDate && x.Time <= EndDate))
            {
                TotalProfit += item.Profit;
            }

        }


    }
}
