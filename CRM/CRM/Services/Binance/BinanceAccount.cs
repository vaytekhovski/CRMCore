//using Binance.API.Csharp.Client;
//using Binance.API.Csharp.Client.Models.Account;
//using Binance.API.Csharp.Client.Models.Market;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Binance;
using System.Threading;
using CRM.Models.Binance;

namespace CRM.Services.Binance
{
    public class BinanceAccount
    {
        private string APIKey;
        private string APISecret;

        private IEnumerable<AccountTrade> trades = null;
        AccountInfo account;
        private List<AccountTradeHistory> accountTradeHistories = new List<AccountTradeHistory>();

        public List<AccountTradeHistory> AccountTradeHistories { get => accountTradeHistories;}

        public bool isDone = false;

        private List<Symbol> Coins = new List<Symbol>();

        public async Task LoadAsync(string acc, string coin)
        {
            switch (acc)
            {
                case "a1":
                    APIKey = "R282j1Qcp90DyOWCc3t1y9qOoeTRU6FhkkOgXm60BdOCarzS0Yxxl3N2q1KMQvjD";
                    APISecret = "8xBqM5c716VrP2naX1vCsrDu62CpTmh3LV9xxh4AWjAbF920V160OE8yr9cbQc6v";
                    break;
                case "a2":
                    APIKey = "ZJp3sBb4H2WZjXBZKNTF2P3UdyhuRVFBmkq6JaPBzmrDq9MUDJjNS6ZdmL7sL6Om";
                    APISecret = "E0b1GSCt9S7EbZLjIUS79B1mddqgNHZzmjO0DIFbENZqohPgqQ1Rwqn2rwhWYb7z";
                    break;
                default:
                    break;
            }


            Coins.Add(Symbol.BTC_USDT);
            Coins.Add(Symbol.BNB_USDT);
            Coins.Add(Symbol.EOS_USDT);
            Coins.Add(Symbol.ETH_USDT);
            Coins.Add(Symbol.XRP_USDT);
            Coins.Add(Symbol.LTC_USDT);
            Coins.Add(Symbol.TRX_USDT);


            Symbol symbol = Symbol.ADA_BNB;

            switch (coin)
            {
                case "BNB":
                    symbol = Symbol.BNB_USDT;
                    break;
                case "BTC":
                    symbol = Symbol.BTC_USDT;
                    break;
                case "EOS":
                    symbol = Symbol.EOS_USDT;
                    break;
                case "ETH":
                    symbol = Symbol.ETH_USDT;
                    break;
                case "XRP":
                    symbol = Symbol.XRP_USDT;
                    break;
                case "LTC":
                    symbol = Symbol.LTC_USDT;
                    break;
                case "TRX":
                    symbol = Symbol.TRX_USDT;
                    break;
                default:
                    break;
            }

            


            var api = new BinanceApi();

            using (var user = new BinanceApiUser(APIKey, APISecret))
            {
                account = await api.GetAccountInfoAsync(user);

                if (coin != "all")
                {
                    trades = await api.GetAccountTradesAsync(user, symbol);
                    AddToTradeHistories();
                }
                else
                {
                    foreach (var _coin in Coins)
                    {
                        trades = await api.GetAccountTradesAsync(user, _coin);
                        AddToTradeHistories();
                    }
                }
            }

            UpdateBalance();

            isDone = true;
        }

        private void AddToTradeHistories()
        {
            foreach (var item in trades.OrderByDescending(x => x.Time))
            {
                accountTradeHistories.Add(new AccountTradeHistory
                {
                    Time = item.Time,
                    Side = item.IsBuyer == true ? "BUY" : "SELL",
                    Pair = item.Symbol,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    DollarQuantity = item.Price * item.Quantity,
                    CommissionAsset = item.CommissionAsset,
                    Commission = item.Commission,
                    BalanceUSDT = 0
                });
            }
        }

        private void UpdateBalance()
        {
            var currentBalace = account.Balances.FirstOrDefault(x => x.Asset == "USDT").Free;

            foreach (var item in accountTradeHistories)
            {
                item.BalanceUSDT = currentBalace;

                if (item.Side == "BUY")
                    currentBalace += item.DollarQuantity;
                else
                    currentBalace -= item.DollarQuantity;
            }

        }


    }
}
