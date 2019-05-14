using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CRM.Master;
using CRM.Models;
using CRM.Models.Binance;
using CRM.Models.Database;
using CRM.Models.Master;
using CRM.Services.Balances;

namespace CRM.Services
{
    public class THService
    {        
        public List<AccountTradeHistory> AccountTradeHistories { get; private set; } = new List<AccountTradeHistory>();

        public double TotalProfit { get; set; }
        public double TotalPercentProfit { get; set; }
        public int CountOfPages { get; set; }

        private readonly List<int> IgnoreIds = new List<int>();
        private List<ExchangeKey> ExchangeKeys { get; set; }

        private DateTime StartDate;
        private DateTime EndDate;

        private string Acc;
        private string Coin;
        private readonly DateTime MinDate = new DateTime(2019, 04, 05);

        

        public void Load(string acc, string coin, DateTime startDate, DateTime endDate)
        {
            Acc = acc;
            Coin = coin;

            InitializeIgnoreList();
            TotalProfit = 0;

            StartDate = startDate.AddDays(1);
            EndDate = endDate;

            InitializeExchangeKeys();


            List<Orders> orders = new List<Orders>();
            List<SignalsPrivate> signals = new List<SignalsPrivate>();

            StartDate = MinDate > StartDate ? MinDate : StartDate;

            using (masterContext context = new masterContext())
            {
                orders = context.Orders.Where(x => 
                    (acc != "all" ? x.AccountId == acc : true) && 
                    (coin != "all" ? x.Base == coin : true) &&
                    x.TimeEnded >= StartDate &&
                    x.TimeEnded <= EndDate).ToList();

                signals = context.SignalsPrivate.Where(x => x.ErrorMessages == null).ToList();
            }


            orders = (List<Orders>)InsertNewOrders(orders);
            orders = (List<Orders>)ChangeOrdersAmount(orders);
            AddToTradeHistories(orders);
            UpdateProfit();
            AddSignals(signals);

            AccountTradeHistories = AccountTradeHistories
                .OrderByDescending(x => x.Time).ToList();

            CountOfPages = (int)Math.Ceiling((decimal)((double)AccountTradeHistories.Count / 100));

        }

        private ICollection<Orders> InsertNewOrders(List<Orders> orders)
        {
            List<Orders> newOrders = new List<Orders>();

            DateTime date = new DateTime(2019, 04, 25, 0, 23, 08);
            Orders item = new Orders(0, "8025d4bf-4af6-466f-b93c-5a807fd37f68", "DASH", "sell", date, 62.26038000M, 113.2751M);
            newOrders.Add(item);

            date = new DateTime(2019, 04, 25, 0, 22, 47);
            item = new Orders(0, "9560eadf-74cf-4596-a7e5-bffcd201f6ec", "DASH", "sell", date, 4.59701000M, 113.376M);
            newOrders.Add(item);

            orders.AddRange(newOrders.Where(x =>
                    (Acc != "all" ? x.AccountId == Acc : true) &&
                    (Coin != "all" ? x.Base == Coin : true) &&
                    x.TimeEnded >= MinDate).ToList());

            return orders;
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
                Debug.WriteLine(ex.Message);
            }

            try
            {
                order = orders.FirstOrDefault(x => x.Id == 320);
                orders.FirstOrDefault(x => x.Id == 323).ClosedAmount = order.ClosedAmount;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                order = orders.FirstOrDefault(x => x.Id == 325);
                orders.FirstOrDefault(x => x.Id == 325).ClosedAmount = order.ClosedAmount - (decimal)19.81;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                orders.FirstOrDefault(x => x.Id == 381).ClosedAmount = 0.87867500M;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
            IgnoreIds.Add(383);
        }


        private string AccountName(string accountId)
        {
            return ExchangeKeys.FirstOrDefault(x => x.AccountId == accountId).Name;
        }
        
        private void AddToTradeHistories(ICollection<Orders> orders)
        {
            AccountTradeHistories.Clear();
            int counter = 1;
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
                    DollarQuantity = item.Rate * item.ClosedAmount,
                    BalanceUSDT = 0
                });
            }
        }

        
        private void UpdateProfit()
        {
            foreach (var _coin in DropDownFields.Coins.Where(x => x.Value != "all")) 
            {
                foreach (var _acc in ExchangeKeys.Where(x=>x.AccountId != "all"))
                {
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
            }

            foreach (var item in AccountTradeHistories.Where(x => x.Profit != 0 && x.Time >= StartDate && x.Time <= EndDate))
            {
                TotalProfit += item.Profit;
            }

        }
    }
}
