using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance;
using CRM.Models.Binance;

namespace CRM.Services.Binance
{
    public class BinanceAccountService
    {
        private string APIKey;
        private string APISecret;

        private IEnumerable<AccountTrade> trades = null;

        private List<Symbol> Coins = new List<Symbol>();
        private AccountInfo account;

        public List<AccountTradeHistory> AccountTradeHistories { get; private set; } = new List<AccountTradeHistory>();

        public bool isDone = false;


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

            Coins.Add(Symbol.ONT_USDT);
            Coins.Add(Symbol.XLM_USDT);
            Coins.Add(Symbol.ADA_USDT);
            Coins.Add(Symbol.BCHABC_USDT);


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
                case "ONT":
                    symbol = Symbol.ONT_USDT;
                    break;
                case "XLM":
                    symbol = Symbol.XLM_USDT;
                    break;
                case "ADA":
                    symbol = Symbol.ADA_USDT;
                    break;
                case "BCHABC":
                    symbol = Symbol.BCHABC_USDT;
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

            UpdateBalance(acc);
            UpdateProfit();

            isDone = true;
        }

        private void AddToTradeHistories()
        {
            foreach (var item in trades.Where(x => x.Time >= new DateTime(2019, 04, 01)))
            {
                AccountTradeHistories.Add(new AccountTradeHistory
                {
                    Time = item.Time.AddHours(3),
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

        private void UpdateBalance(string account)
        {
            // 28.02.2019
            // 1-й 5517.8
            // 2-й 518.49

            double currentBalace = 5525;//account.Balances.FirstOrDefault(x => x.Asset == "USDT").Free;
            switch (account)
            {
                case "a1":
                    currentBalace = 5525;
                    break;
                case "a2":
                    currentBalace = 520;
                    break;
                default:
                    break;
            }

            List<AccountTradeHistory> list = AccountTradeHistories.OrderBy(x => x.Time).ToList();
            foreach (var item in list)
            {
                if (item.Side == "BUY")
                {
                    currentBalace -= double.Parse(item.DollarQuantity.ToString());
                }
                else
                {
                    currentBalace += double.Parse(item.DollarQuantity.ToString());
                }
                
                item.BalanceUSDT = currentBalace;
            }

        }

        private void UpdateProfit()
        {
            foreach (var _coin in Coins)
            {
                double profit = 0;
                var TH = AccountTradeHistories.Where(x => x.Pair == _coin).OrderBy(x => x.Time).ToArray();

                for (int i = 0; i < TH.Count(); i++)
                {
                    if (TH[i].Side == "BUY")
                    {
                        profit -= (double)TH[i].DollarQuantity;
                    }
                    else
                    {
                        profit += (double)TH[i].DollarQuantity;
                    }

                    if ((TH[i].Side == "SELL" && i == TH.Count() - 1) || (TH[i].Side == "SELL" && TH[i + 1].Side == "BUY"))
                    {
                        TH[i].Profit = profit;
                        profit = 0;
                    }
                }

                int j = 0;
                foreach (var item in AccountTradeHistories.Where(x => x.Pair == _coin).OrderBy(x => x.Time))
                {
                    if (TH[j].Profit != 0)
                    {
                        item.Profit = TH[j].Profit;
                    }
                    j++;
                }
            }
        }


    }
}
