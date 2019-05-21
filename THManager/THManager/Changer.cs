using Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using THManager.Models;

namespace THManager
{
    class Changer
    {
        public static List<AccountTradeHistory> AccountTradeHistories { get; private set; }

        public static List<SignalsPrivate> Signals { get; set; }

        public static List<Orders> orders { get; set; }

        public static List<ExchangeKey> ExchangeKeys { get; set; }
        

        private static List<int> IgnoreIds = new List<int>();

        private static int LastId;


        public static List<AccountTradeHistory> ChangeOrdersBeforeCalculate(List<Orders> _orders)
        {
            AccountTradeHistories = new List<AccountTradeHistory>();
            orders = _orders;

            using (MySqlContext context = new MySqlContext())
            {
                Signals = context.SignalsPrivate.Where(x => x.ErrorMessages == null).ToList();
            }

            using (CRMContext context = new CRMContext())
            {
                try
                {
                    LastId = context.AccountTradeHistories.Where(x => x.Time >= DateTime.Now.Date).FirstOrDefault().Id;
                }
                catch
                {
                    LastId = 1;
                }
            }


            InitializeIgnoreList();
            InitializeExchangeKeys();

            orders = (List<Orders>)InsertNewOrders(orders);
            orders = (List<Orders>)ChangeOrdersAmount(orders);
            AddToTradeHistories(orders);
            AddSignals(Signals);
            
            return AccountTradeHistories.OrderByDescending(x => x.Time).ToList();
        }

        private static ICollection<Orders> InsertNewOrders(List<Orders> orders)
        {
            DateTime date = new DateTime(2019, 04, 25, 0, 23, 08);
            Orders item = new Orders(0, "8025d4bf-4af6-466f-b93c-5a807fd37f68", "DASH", "sell", date, 62.26038000M, 113.2751M);
            if (!orders.Contains(item))
                orders.Add(item);

            date = new DateTime(2019, 04, 25, 0, 22, 47);
            item = new Orders(0, "9560eadf-74cf-4596-a7e5-bffcd201f6ec", "DASH", "sell", date, 4.59701000M, 113.376M);
            if (!orders.Contains(item))
                orders.Add(item);

            return orders;
        }

        private static ICollection<Orders> ChangeOrdersAmount(List<Orders> orders)
        {
            Orders order;

            try
            {
                order = orders.FirstOrDefault(x => x.Id == 319);
                orders.FirstOrDefault(x => x.Id == 322).ClosedAmount = order.ClosedAmount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                order = orders.FirstOrDefault(x => x.Id == 320);
                orders.FirstOrDefault(x => x.Id == 323).ClosedAmount = order.ClosedAmount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                order = orders.FirstOrDefault(x => x.Id == 325);
                orders.FirstOrDefault(x => x.Id == 325).ClosedAmount = order.ClosedAmount - (decimal)19.81;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 381).ClosedAmount = 0.87867500M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 389).ClosedAmount = 6853.85893474M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 394).ClosedAmount = 7086.49496043M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return orders;
        }

        private static void AddSignals(List<SignalsPrivate> signals)
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
                            + " TrendDiff: " + signal.TrendDiff;
                    }
                }

                previous = item;
            }
        }

        private static void InitializeExchangeKeys()
        {
            using (CRMContext context = new CRMContext())
            {
                ExchangeKeys = context.ExchangeKeys.ToList();
            }
        }

        private static void InitializeIgnoreList()
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
        }

        private static string AccountName(string accountId)
        {
            return ExchangeKeys.FirstOrDefault(x => x.AccountId == accountId).Name;
        }

        private static void AddToTradeHistories(ICollection<Orders> orders)
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
                    Price = Convert.ToDouble(item.Rate),
                    Quantity = Convert.ToDouble(item.ClosedAmount),
                    DollarQuantity = item.Rate * item.ClosedAmount,
                    BalanceUSDT = 0
                });
            }
        }
    }
}
