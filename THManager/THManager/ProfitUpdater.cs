using Contexts;
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
            LastSellId = FindLastSell();

            using (CRMContext context = new CRMContext())
            {

                AccountTradeHistories = UpdateDesiredAmounts(AccountTradeHistories);
                List<AccountTradeHistory> CalculatedTradeHistories = CalculateProfit(AccountTradeHistories.ToList());

                //List<AccountTradeHistory> buf = context.AccountTradeHistories.Where(x => x.Id > LastSellId).ToList();
                //context.AccountTradeHistories.RemoveRange(buf);

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


        
        public static int FindLastSell()
        {
            using (CRMContext context = new CRMContext())
            {
                try
                {
                    LastSellId = context.AccountTradeHistories.LastOrDefault(x => 
                        x.Time < DateTime.Now.AddDays(-1) &&
                        x.Side == "sell" &&
                        x.Profit != 0).Id;
                }
                catch
                {
                    LastSellId = 0;
                }
            }
            return LastSellId;
        }

        public static DateTime FindTimeLastSell()
        {
            DateTime LastSellTime;
            using (CRMContext context = new CRMContext())
            {
                try
                {
                    LastSellTime = context.AccountTradeHistories.LastOrDefault(x => 
                        x.Time < DateTime.Now.AddDays(-1) && 
                        x.Side == "sell" && 
                        x.Profit != 0).Time;
                }
                catch
                {
                    LastSellTime = new DateTime(1999,01,01);
                }
            }
            return LastSellTime;
        }

        private static List<AccountTradeHistory> UpdateDesiredAmounts(List<AccountTradeHistory> UncalculatedTradeHistories)
        {
            foreach (var _coin in UncalculatedTradeHistories.Where(x => x.Pair != "all").Select(x => x.Pair).Distinct())
            {
                foreach (var _acc in Changer.ExchangeKeys.Where(x => x.AccountId != "all"))
                {
                    double buyAmount = 0;

                    var TH = UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == AccountName(_acc.AccountId)).OrderBy(x => x.Time).ToArray();

                    for (int i = 0; i < TH.Count(); i++)
                    {
                        buyAmount += TH[i].Side == "buy" ? TH[i].Quantity : 0;


                        if ((TH[i].Side == "sell" && i == TH.Count() - 1) || (TH[i].Side == "sell" && TH[i + 1].Side == "buy"))
                        {
                            TH[i].DesiredQuantity = buyAmount;
                            TH[i].DesiredDollarQuantity = buyAmount * TH[i].Price;
                            buyAmount = 0;
                        }
                    }

                    int j = 0;
                    foreach (var item in UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == AccountName(_acc.AccountId)).OrderBy(x => x.Time))
                    {
                        if (TH[j].DesiredQuantity != 0)
                        {
                            item.DesiredQuantity = TH[j].DesiredQuantity;
                            item.DesiredDollarQuantity = TH[j].DesiredDollarQuantity;
                        }
                        j++;
                    }
                }
            }

            return UncalculatedTradeHistories;
        }


        private static List<AccountTradeHistory> CalculateProfit(List<AccountTradeHistory> UncalculatedTradeHistories)
        {
            foreach (var _coin in UncalculatedTradeHistories.Where(x=>x.Pair != "all").Select(x => x.Pair).Distinct()) // TODO: [COMPLETE] select base + distinct
            {
                foreach (var _acc in Changer.ExchangeKeys.Where(x => x.AccountId != "all")) // TODO: select AccountId + distinct
                {
                    double profit = 0;
                    double desiredProfit = 0;
                    double buyAmount = 0;

                    var TH = UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == AccountName(_acc.AccountId)).OrderBy(x => x.Time).ToArray();

                    for (int i = 0; i < TH.Count(); i++)
                    {
                        buyAmount += TH[i].Side == "buy" ? TH[i].DollarQuantity : 0;

                        profit += TH[i].Side == "buy" ? TH[i].DollarQuantity * -1 : TH[i].DollarQuantity;
                        desiredProfit += TH[i].Side == "buy" ? TH[i].DollarQuantity * -1 : TH[i].DesiredDollarQuantity;

                        if ((TH[i].Side == "sell" && i == TH.Count() - 1) || (TH[i].Side == "sell" && TH[i + 1].Side == "buy"))
                        {
                            TH[i].Profit = profit;
                            TH[i].DesiredProfit = desiredProfit;

                            TH[i].PercentProfit = profit / ((buyAmount + (buyAmount + profit)) / 2) * 100;
                            TH[i].DesiredPercentProfit = desiredProfit / ((buyAmount + (buyAmount + desiredProfit)) / 2) * 100;

                            profit = 0;
                            desiredProfit = 0;
                            buyAmount = 0;
                        }
                    }

                    int j = 0;
                    foreach (var item in UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == AccountName(_acc.AccountId)).OrderBy(x => x.Time))
                    {
                        if (TH[j].Profit != 0)
                        {
                            item.Profit = TH[j].Profit;
                            item.DesiredProfit = TH[j].DesiredProfit;

                            item.PercentProfit = TH[j].PercentProfit;
                            item.DesiredPercentProfit = TH[j].DesiredPercentProfit;
                        }
                        j++;
                    }
                    
                }
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
