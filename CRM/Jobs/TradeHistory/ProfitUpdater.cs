
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

        private AccountTradeHistory LastEl;

        public async void UpdateProfit(List<AccountTradeHistory> AccountTradeHistories, bool regularCalculating = true)
        {
            if (regularCalculating)
                LastEl = Helper.FindLastSellDayAgo();
            else
                LastEl = new AccountTradeHistory { Id = 0, Time = new DateTime(1970, 1, 1, 1, 1, 1) };

            using (BasicContext context = new BasicContext())
            {
                List<AccountTradeHistory> CalculatedTradeHistories = CalculateProfit(AccountTradeHistories);

                List<AccountTradeHistory> buf = context.AccountTradeHistories.Where(x => x.Id > LastEl.Id).ToList();
                context.AccountTradeHistories.RemoveRange(buf);

                int ordersCount = 0;
                for (int i = 0; i < (CalculatedTradeHistories.Count / 500) + 1; i++)
                {
                    var ordersToLoad = CalculatedTradeHistories.Skip(500 * i).Take(500).ToList();
                    ordersCount += ordersToLoad.Count;
                    context.AccountTradeHistories.AddRange(ordersToLoad);
                    context.SaveChanges();
                    Console.WriteLine($"[{ordersCount}/{CalculatedTradeHistories.Count}] orders loaded");
                }
            }
        }


        private List<int> IgnoreIds = new List<int>();
        private void InitializeIgnoreList()
        {
            using (BasicContext db = new BasicContext())
            {
                IgnoreIds.AddRange(db.IgnoreIds.Select(x => x.OrderId));
            }

        }

        private List<AccountTradeHistory> CalculateProfit(List<AccountTradeHistory> UncalculatedTradeHistories)
        {
            InitializeIgnoreList();
            foreach (var item in IgnoreIds)
            {
                var itemToRemove = UncalculatedTradeHistories.FirstOrDefault(x => x.Id == item);
                UncalculatedTradeHistories.Remove(itemToRemove);
            }

            foreach (var _coin in UncalculatedTradeHistories.Select(x => x.Pair).Distinct())
            {
                foreach (var Account in UncalculatedTradeHistories.Select(x => x.Account).Distinct())
                {
                    decimal Fee = 0;
                    decimal buyAmount = 0;
                    decimal buyAmountWithoutFee = 0;
                    decimal sellAmount = 0;
                    decimal sellAmountWithoutFee = 0;

                    var TH = UncalculatedTradeHistories.Where(x => x.Pair == _coin && x.Account == Account).OrderBy(x => x.Time).ToArray();

                    for (int i = 0; i < TH.Count(); i++)
                    {
                        Fee = TH[i].DollarQuantity * (decimal)0.001;
                        TH[i].Fee = Fee;

                        if (TH[i].Side == "buy") 
                        {
                            buyAmount += TH[i].DollarQuantity;
                            buyAmountWithoutFee += TH[i].DollarQuantity - Fee;
                        }
                        else
                        {
                            sellAmount += TH[i].DollarQuantity - Fee;
                            sellAmountWithoutFee += TH[i].DollarQuantity;
                        }

                        if ((TH[i].Side == "sell" && i == TH.Count() - 1) || (TH[i].Side == "sell" && TH[i + 1].Side == "buy"))
                        {
                            TH[i].Profit = sellAmount - buyAmount;
                            TH[i].ProfitWithoutFee = sellAmountWithoutFee - buyAmountWithoutFee;

                            try
                            {
                                TH[i].PercentProfit = (sellAmount - buyAmount) / buyAmount * 100M;
                                TH[i].PercentProfitWithoutFee = (sellAmountWithoutFee - buyAmountWithoutFee) / buyAmountWithoutFee * 100M;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            buyAmount = 0;
                            buyAmountWithoutFee = 0;
                            sellAmount = 0;
                            sellAmountWithoutFee = 0;
                        }
                    }
                    
                }
            }

            return UncalculatedTradeHistories;
        }
    }

    public class Field
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
}
