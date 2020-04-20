using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.Contexts;

namespace Jobs
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
        


        private int LastId;


        public List<AccountTradeHistory> ChangeOrdersBeforeCalculate(List<Orders> _orders, bool regularCalculating = true)
        {
            AccountTradeHistories = new List<AccountTradeHistory>();
            orders = _orders;
            /*
            using (MySQLContext context = new MySQLContext())
            {
                Signals = context.SignalsPrivate.Where(x => x.ErrorMessages == null).ToList();
            }
            */
            using (BasicContext context = new BasicContext())
            {
                LastId = regularCalculating ? context.AccountTradeHistories.FirstOrDefault(x => x.Time > Helper.FindTimeLastSell()).Id : 1;
            }


            InitializeIgnoreList();
            InitializeExchangeKeys();

            orders = ChangeAmounts(orders);
            AddToTradeHistories(orders);
            //AddSignals(Signals);
            
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
            using (BasicContext context = new BasicContext())
            {
                ExchangeKeys = context.ExchangeKeys.ToList();
            }
        }

        private List<Orders> ChangeAmounts(List<Orders> orders)
        {
            using(BasicContext db = new BasicContext())
            {
                //db.WrongOrders.Add(new WrongOrders { OrderId = 1942, Amount = 0.07899000M });
                //db.WrongOrders.Add(new WrongOrders { OrderId = 2282, Amount = 12.805860M });
                //db.SaveChanges();

                foreach (var item in db.WrongOrders.ToList())
                {
                    try
                    {
                        orders.FirstOrDefault(x => x.id == item.OrderId.ToString()).amount = Convert.ToDouble(item.Amount);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
            }

            
            return orders;
        }

        

        private string AccountName(string accountId)
        {
            return ExchangeKeys.FirstOrDefault(x => x.AccountId == accountId).Name;
        }

        private void AddToTradeHistories(ICollection<Orders> orders)
        {
            AccountTradeHistories.Clear();
            int counter = LastId;

            foreach (var item in orders.OrderBy(x => x.closed))
            {
                if (item.amount == 0) continue;

                AccountTradeHistories.Add(new AccountTradeHistory
                {
                    Id = counter++,
                    Account = item.account_id,
                    Time = item.closed.AddHours(3),
                    Side = item.side,
                    Pair = item.@base,
                    Price = Convert.ToDecimal(item.price),
                    Quantity = Convert.ToDecimal(item.amount),
                    DollarQuantity = Convert.ToDecimal(item.price * item.amount),
                    LowerBand = 0M
                });
            }
        }
    }
}
