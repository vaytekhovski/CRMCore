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


        public List<AccountTradeHistory> ChangeOrdersBeforeCalculate(List<Orders> _orders)
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
                    LastId = context.AccountTradeHistories.FirstOrDefault(x => x.Time > Helper.FindTimeLastSell()).Id;
                }
                catch
                {
                    LastId = 1;
                }
            }


            InitializeIgnoreList();
            InitializeExchangeKeys();

            orders = (List<Orders>)ChangeOrdersAmount(orders);
            AddToTradeHistories(orders);
            AddSignals(Signals);
            
            return AccountTradeHistories.OrderByDescending(x => x.Time).ToList();
        }

        private ICollection<Orders> ChangeOrdersAmount(List<Orders> orders)
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

            try
            {
                orders.FirstOrDefault(x => x.Id == 439).ClosedAmount = 5456.98033905M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 519).ClosedAmount = 287994.9M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 520).ClosedAmount = 35668.49496043M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 522).ClosedAmount = 10515.18033905M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 530).ClosedAmount = 285525.4M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                orders.FirstOrDefault(x => x.Id == 532).ClosedAmount = 35368.1M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 534).ClosedAmount = 6154.44296213M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 546).ClosedAmount = 6094.0137037M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 553).ClosedAmount = 25234.8137037M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 555).ClosedAmount = 281500.2M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 562).ClosedAmount = 6040.242M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 566).ClosedAmount = 5961.542M;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return orders;
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
                            + " TrendDiff: " + signal.TrendDiff;
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
                    Price = Convert.ToDouble(item.Rate),
                    Quantity = Convert.ToDouble(item.ClosedAmount),
                    DesiredQuantity = item.Side == "buy" ? Convert.ToDouble(item.ClosedAmount) : 0,
                    DollarQuantity = (double)(item.Rate * item.ClosedAmount),
                    DesiredDollarQuantity = item.Side == "buy" ? (double)(item.Rate * item.ClosedAmount) : 0
                });
            }
        }
    }
}
