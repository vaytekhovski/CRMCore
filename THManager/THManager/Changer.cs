using Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using THManager.Models;

namespace THManager
{
    class Changer
    {
        public Changer()
        {

        }

        public List<AccountTradeHistory> AccountTradeHistories { get; private set; }

        public List<SignalsPrivate> Signals { get; set; }

        public List<Orders> orders { get; set; }

        public static List<ExchangeKey> ExchangeKeys { get; set; }
        

        private List<int> IgnoreIds = new List<int>();

        private int LastId;


        public List<AccountTradeHistory> ChangeOrdersBeforeCalculate(List<Orders> _orders, bool regularCalculating = true)
        {
            AccountTradeHistories = new List<AccountTradeHistory>();
            orders = _orders;

            using (MySqlContext context = new MySqlContext())
            {
                Signals = context.SignalsPrivate.Where(x => x.ErrorMessages == null).ToList();
            }

            using (CRMContext context = new CRMContext())
            {
                //try
                //{
                //    LastId = context.AccountTradeHistories.FirstOrDefault(x => x.Time > Helper.FindTimeLastSell()).Id;
                //}
                //catch
                //{
                //    LastId = 1;
                //}

                LastId = regularCalculating ? context.AccountTradeHistories.FirstOrDefault(x => x.Time > Helper.FindTimeLastSell()).Id : 1;

            }


            InitializeIgnoreList();
            InitializeExchangeKeys();

            AddToTradeHistories(orders);
            AddSignals(Signals);
            
            return AccountTradeHistories.OrderByDescending(x => x.Time).ToList();
        }

        private void AddSignals(List<SignalsPrivate> signals)
        {
            var previous = AccountTradeHistories.FirstOrDefault();

            foreach (var item in from _acc in ExchangeKeys.Where(x => x.AccountId != "all")
                                 from th in AccountTradeHistories.Where(x => x.Account == _acc.Name)
                                 select th)
            {

                if ((item.Side == "buy" && previous.Side == "sell") || (item.Side == "buy" && item.Pair != previous.Pair))
                {
                    SignalsPrivate signal = signals.FirstOrDefault(x =>
                    x.Base == item.Pair &&
                    x.SourceTime.Year == item.Time.Year &&
                    x.SourceTime.Month == item.Time.Month &&
                    x.SourceTime.Day == item.Time.Day &&
                    x.SourceTime.Hour == item.Time.AddHours(-3).Hour
                    );

                    if (signal != null)
                    {
                        item.SignalStr = signal.Id
                            + " " + signal.Exchange
                            + " " + signal.Base
                            + " " + signal.SourceTime.AddHours(3)
                            + " TrendDiff: " + signal.Value;
                    }
                }

                previous = item;
            }
        }

        private void InitializeExchangeKeys()
        {
            using (CRMContext context = new CRMContext())
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
            IgnoreIds.Add(383);
            IgnoreIds.Add(1910);
            IgnoreIds.Add(1911);
            IgnoreIds.Add(1912);
            IgnoreIds.Add(1913);
            IgnoreIds.Add(1914);
            IgnoreIds.Add(1915);
            IgnoreIds.Add(1916);
            IgnoreIds.Add(1917);
        }

        private string AccountName(string accountId)
        {
            return ExchangeKeys.FirstOrDefault(x => x.AccountId == accountId).Name;
        }

        private void AddToTradeHistories(ICollection<Orders> orders)
        {
            AccountTradeHistories.Clear();
            int counter = LastId;
            foreach (var item in orders.OrderBy(x => x.TimeEnded))
            {
                if (item.ClosedAmount == 0) continue;

                var ignore = IgnoreIds.FirstOrDefault(id => item.Id == id);

                if (ignore == 0) AccountTradeHistories.Add(new AccountTradeHistory
                {
                    Id = counter++,
                    Account = AccountName(item.AccountId),
                    Time = item.TimeEnded.AddHours(3),
                    Side = item.Side,
                    Pair = item.Base,
                    Price = item.Rate,
                    Quantity = item.ClosedAmount,
                    DesiredQuantity = item.Side == "buy" ? item.ClosedAmount : 0,
                    DollarQuantity = item.Rate * item.ClosedAmount,
                    DesiredDollarQuantity = item.Side == "buy" ? item.Rate * item.ClosedAmount : 0
                });
            }
        }
    }
}
