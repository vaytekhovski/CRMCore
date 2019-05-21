﻿using Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THManager.Models;

namespace THManager
{
    class ProfitUpdater
    {
        private static List<AccountTradeHistory> AccountTradeHistories { get; set; }

        private static List<Field> Coins = new List<Field>();

        private static int LastSellId;
        

        public static void UpdateProfit(List<AccountTradeHistory> _AccountTradeHistories)
        {
            AccountTradeHistories = _AccountTradeHistories;
            InitiateCoins();
            FindLastSell();

            List<AccountTradeHistory> CalculatedTradeHistories = CalculateProfit(AccountTradeHistories.Where(x => x.Id > LastSellId).ToList());

            using (CRMContext context = new CRMContext())
            {
                var currentDate = DateTime.Now.Date;
                List<AccountTradeHistory> buf = context.AccountTradeHistories.Where(x => x.Time > currentDate).ToList();

                context.AccountTradeHistories.RemoveRange(buf);
                context.AccountTradeHistories.AddRange(CalculatedTradeHistories);

                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.AccountTradeHistories ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.AccountTradeHistories OFF");
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
        }


        
        private static void FindLastSell()
        {
            LastSellId = AccountTradeHistories.FindLastIndex(x => x.Side == "sell" && x.Profit != 0);
        }
        

        private static List<AccountTradeHistory> CalculateProfit(List<AccountTradeHistory> UncalculatedTradeHistories)
        {
            foreach (var _coin in Coins.Where(x => x.Value != "all"))
            {
                foreach (var _acc in Changer.ExchangeKeys.Where(x => x.AccountId != "all"))
                {
                    double profit = 0;
                    double buyAmount = 0;

                    var TH = UncalculatedTradeHistories.Where(x => x.Pair == _coin.Value && x.Account == AccountName(_acc.AccountId)).OrderBy(x => x.Time).ToArray();

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
                    foreach (var item in UncalculatedTradeHistories.Where(x => x.Pair == _coin.Value && x.Account == AccountName(_acc.AccountId)).OrderBy(x => x.Time))
                    {
                        if (TH[j].Profit != 0)
                        {
                            item.Profit = TH[j].Profit;
                            item.PercentProfit = TH[j].PercentProfit;
                        }
                        j++;
                    }
                    
                }
            }

            foreach (var item in UncalculatedTradeHistories.Where(x => x.Profit != 0).OrderBy(x => x.Time)) 
            {
                UncalculatedTradeHistories.First().TotalProfit += item.Profit;
            }

            return UncalculatedTradeHistories;
        }

        private static string AccountName(string accountId)
        {
            return Changer.ExchangeKeys.FirstOrDefault(x => x.AccountId == accountId).Name;
        }

        private static void InitiateCoins()
        {
            Coins.Add(new Field { Value = "all", Name = "Все валюты" });
            Coins.Add(new Field { Value = "BTC", Name = "USDT-BTC" });
            Coins.Add(new Field { Value = "BNB", Name = "USDT-BNB" });
            Coins.Add(new Field { Value = "EOS", Name = "USDT-EOS" });
            Coins.Add(new Field { Value = "ETH", Name = "USDT-ETH" });
            Coins.Add(new Field { Value = "XRP", Name = "USDT-XRP" });
            Coins.Add(new Field { Value = "LTC", Name = "USDT-LTC" });
            Coins.Add(new Field { Value = "TRX", Name = "USDT-TRX" });

            Coins.Add(new Field { Value = "ZEC", Name = "USDT-ZEC" });
            Coins.Add(new Field { Value = "DASH", Name = "USDT-DASH" });
            Coins.Add(new Field { Value = "XMR", Name = "USDT-XMR" });
            Coins.Add(new Field { Value = "ONT", Name = "USDT-ONT" });
            Coins.Add(new Field { Value = "XLM", Name = "USDT-XLM" });
            Coins.Add(new Field { Value = "ADA", Name = "USDT-ADA" });
            Coins.Add(new Field { Value = "BCHABC", Name = "USDT-BCHABC" });
        }


    }

    public class Field
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
}
