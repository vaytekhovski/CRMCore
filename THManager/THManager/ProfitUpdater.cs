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
        private static List<SecondAccountTH> SecondAccountTHs { get; set; }

        private static List<Field> Coins = new List<Field>();

        private static int LastSellId;
        private static int FirstIdToCalculate;

        public static void UpdateProfit(List<SecondAccountTH> _SecondAccountTHs)
        {
            SecondAccountTHs = _SecondAccountTHs;
            InitiateCoins();
            LastSellId = FindLastSell();

            using (CRMContext context = new CRMContext())
            {
                //SecondAccountTH order = context.SecondAccountTHs.FirstOrDefault(x => x.Id > LastSellId);
                //try
                //{
                //    FirstIdToCalculate = SecondAccountTHs.FirstOrDefault(x => x.Account == order.Account && x.Time == order.Time).Id;
                //}
                //catch 
                //{
                //    FirstIdToCalculate = 0;
                //}
                List<SecondAccountTH> CalculatedTradeHistories = CalculateProfit(SecondAccountTHs/*.Where(x => x.Id >= FirstIdToCalculate)*/.ToList());



                List<SecondAccountTH> buf = context.SecondAccountTHs.Where(x => x.Id > LastSellId).ToList();
                context.SecondAccountTHs.RemoveRange(buf);

                context.SecondAccountTHs.AddRange(CalculatedTradeHistories);

                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.SecondAccountTHs ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.SecondAccountTHs OFF");
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
                    LastSellId = context.SecondAccountTHs.LastOrDefault(x => x.Side == "sell" && x.Profit != 0).Id;
                    var previusBuy = context.SecondAccountTHs.LastOrDefault(x => x.Side == "buy" && x.Id < LastSellId).Id;
                    LastSellId = context.SecondAccountTHs.LastOrDefault(x => x.Side == "sell" && x.Id < previusBuy).Id;
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
                    var _lastSellId = context.SecondAccountTHs.LastOrDefault(x => x.Side == "sell" && x.Profit != 0).Id;
                    var previusBuy = context.SecondAccountTHs.LastOrDefault(x => x.Side == "buy" && x.Id < _lastSellId).Id;
                    LastSellTime = context.SecondAccountTHs.LastOrDefault(x => x.Side == "sell" && x.Id < previusBuy).Time;
                }
                catch
                {
                    LastSellTime = new DateTime(1999,01,01);
                }
            }
            return LastSellTime;
        }
        
        private static List<SecondAccountTH> CalculateProfit(List<SecondAccountTH> UncalculatedTradeHistories)
        {
            foreach (var _coin in Coins.Where(x => x.Value != "all")) // TODO: select base + distinct
            {
                foreach (var _acc in Changer.ExchangeKeys.Where(x => x.AccountId != "all")) // TODO: select AccountId + distinct
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
