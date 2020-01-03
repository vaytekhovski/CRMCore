
using Business;
using Business.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jobs
{
    class ProfitUpdater
    {
        public ProfitUpdater()
        {

        }
        private List<AccountTradeHistory> AccountTradeHistories { get; set; }

        private List<Field> Coins = new List<Field>();

        private int LastSellId;

        public void UpdateProfit(List<AccountTradeHistory> _AccountTradeHistories, bool regularCalculating = true)
        {
            AccountTradeHistories = _AccountTradeHistories;
            InitiateCoins();
            LastSellId = Helper.FindLastSell();

            using (BasicContext context = new BasicContext())
            {
                AccountTradeHistories = UpdateDesiredAmounts(AccountTradeHistories);
                List<AccountTradeHistory> CalculatedTradeHistories = CalculateProfit(AccountTradeHistories.ToList());

                if (regularCalculating)
                {
                    List<AccountTradeHistory> buf = context.AccountTradeHistories.Where(x => x.Id > LastSellId).ToList();
                    context.AccountTradeHistories.RemoveRange(buf);
                }
                else
                {
                    //context.AccountTradeHistories.RemoveRange(context.AccountTradeHistories.ToList());
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE [AccountTradeHistories]");
                }

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

        
        private List<AccountTradeHistory> UpdateDesiredAmounts(List<AccountTradeHistory> UncalculatedTradeHistories)
        {
            foreach (var _coin in UncalculatedTradeHistories.Where(x => x.Pair != "all").Select(x => x.Pair).Distinct())
            {
                foreach (var AccountName in UncalculatedTradeHistories.Select(x => x.Account).Distinct())
                {
                    decimal buyAmount = 0;

                    var TH = UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == AccountName).OrderBy(x => x.Time).ToArray();

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
                    foreach (var item in UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == AccountName).OrderBy(x => x.Time))
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


        private List<AccountTradeHistory> CalculateProfit(List<AccountTradeHistory> UncalculatedTradeHistories)
        {
            foreach (var _coin in UncalculatedTradeHistories.Select(x => x.Pair).Distinct())
            {
                foreach (var Account in UncalculatedTradeHistories.Select(x => x.Account).Distinct())
                {
                    decimal profit = 0;
                    decimal enterTax = 0;
                    decimal desiredProfit = 0;
                    decimal buyAmount = 0;

                    var TH = UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == Account).OrderBy(x => x.Time).ToArray();

                    for (int i = 0; i < TH.Count(); i++)
                    {
                        buyAmount += TH[i].Side == "buy" ? TH[i].DollarQuantity : 0;
                        enterTax = TH[i].Side == "buy" ? TH[i].DollarQuantity * (decimal)0.001 : 0;
                        TH[i].EnterTax = TH[i].Side == "buy" ? enterTax : 0;

                        profit += TH[i].Side == "buy" ? TH[i].DollarQuantity * -1 : TH[i].DollarQuantity;
                        desiredProfit += TH[i].Side == "buy" ? TH[i].DollarQuantity * -1 : TH[i].DesiredDollarQuantity;

                        if ((TH[i].Side == "sell" && i == TH.Count() - 1) || (TH[i].Side == "sell" && TH[i + 1].Side == "buy"))
                        {
                            TH[i].Profit = profit;
                            TH[i].DesiredProfit = desiredProfit;

                            try
                            {
                                TH[i].PercentProfit = profit / ((buyAmount + (buyAmount + profit)) / 2) * 100;
                                TH[i].DesiredPercentProfit = desiredProfit / ((buyAmount + (buyAmount + desiredProfit)) / 2) * 100;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            profit = 0;
                            desiredProfit = 0;
                            buyAmount = 0;
                        }
                    }

                    int j = 0;
                    foreach (var item in UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == Account).OrderBy(x => x.Time))
                    {
                        if (TH[j].Profit != 0)
                        {
                            item.Profit = TH[j].Profit;
                            item.DesiredProfit = TH[j].DesiredProfit;

                            item.PercentProfit = TH[j].PercentProfit;
                            item.DesiredPercentProfit = TH[j].DesiredPercentProfit;
                        }
                        item.EnterTax = TH[j].EnterTax;
                        j++;
                    }
                    
                }
            }

            return UncalculatedTradeHistories;
        }

        private string AccountId(string accountId)
        {
            return Changer.ExchangeKeys.FirstOrDefault(x => x.AccountId == accountId).Name;
        }

        private string GetAccountId(string accountName)
        {
            return Changer.ExchangeKeys.FirstOrDefault(x => x.Name == accountName).AccountId;
        }

        private void InitiateCoins()
        {
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
