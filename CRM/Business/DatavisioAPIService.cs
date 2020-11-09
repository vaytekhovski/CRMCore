using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Models.DataVisioAPI;
using CRM.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Business.DataVisioAPI
{
    public class DatavisioAPIService
    {
        private static HttpClient Client = new HttpClient();

        public DatavisioAPIService()
        {
        }

        public async Task<string> Authorization(LoginModel login)
        {
            var uri = $"http://46.101.131.228/api/auth";

            var jsonInString = JsonConvert.SerializeObject(login);
            var resp = await Client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            AuthenticationResnose authenticationResnose = JsonConvert.DeserializeObject<AuthenticationResnose>(resp.Content.ReadAsStringAsync().Result);

            return authenticationResnose.token;
        }

        public async Task<WalletCurrency> GetBalance(string accountId, string token, string CoinBase, string type = "debit")
        {
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/balance/{type}/{CoinBase}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            WalletCurrency walletCurrency = JsonConvert.DeserializeObject<WalletCurrency>(response);
            walletCurrency.coin = CoinBase;

            return walletCurrency;
        }

        public async Task GetBalance(string accountId, string token, string type = "debit")
        {
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/balance/{type}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };
            var responce = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();

            //var Wallets = JsonConvert.DeserializeObject<WalletCurrency>();
            
        }

        public async Task<string> EnterDeal(string accountId, string token, PlaceOrderRequest placeOrderModel)
        {

            var jsonInString = JsonConvert.SerializeObject(placeOrderModel);

            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/deals"),
                Headers =
                {
                    { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(jsonInString, Encoding.UTF8, "application/json")
            };

            var response = JsonConvert.DeserializeObject<PlaceOrderResponse>(await Client.SendAsync(Request).Result.Content.ReadAsStringAsync());

            return response.id;
        }

        public async Task<string> TradeDeal(string accountId, string token, string DealId, double amount)
        {
            TradeDealRequest tradeDealRequest = new TradeDealRequest
            {
                amount = amount
            };
            var jsonInString = JsonConvert.SerializeObject(tradeDealRequest);

            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/deals/{DealId}/trade"),
                Headers =
                {
                    { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(jsonInString, Encoding.UTF8, "application/json")
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string>(response);
        }

        public async Task<string> LeaveDeal(string accountId, string token, string DealId)
        {
            TradeDealRequest tradeDealRequest = new TradeDealRequest
            {
                amount = 0
            };

            var jsonInString = JsonConvert.SerializeObject(tradeDealRequest);

            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/deals/{DealId}/leave"),
                Headers =
                {
                    { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(jsonInString, Encoding.UTF8, "application/json")
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string>(response);
        }



        public async Task<Signals> GetSignals(string token, string CoinBase, string source)
        {
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/signals/binance/{CoinBase}/usdt?limit=5000&source={source}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Signals>(response);
        }


        public async Task<Candles[]> GetCandles(string token, string CoinBase)
        {
            var Since = (long)(DateTime.UtcNow.AddHours(-3).Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/exchanges/binance/candles/{CoinBase}/usdt?frame=1&since={Since}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Candles[]>(response);
        }

        public async Task<ListDeals> GetListDeals(string accountId, string token)
        {
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/deals?limit=2000"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };
            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ListDeals>(response);
        }

        public async Task<ListDeals> GetListErrorDeals(string accountId, string token)
        {

            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/account/{accountId}/deals?failed=true"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ListDeals>(response);
        }

        public async Task<Deal> GetDeal(string accountId, string token, string DealId)
        {
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/deals/{DealId}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };
            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            var Deal = JsonConvert.DeserializeObject<Deal>(response);
            Deal.id = DealId;
            return Deal;
        }

        public async Task<List<Graph>> GetGraphs(string token, string CoinBase, DateTime StartDate, DateTime EndDate, string source = "grad")
        {
            var since = ((DateTimeOffset)StartDate).ToUnixTimeSeconds();
            var limit = Convert.ToInt32((EndDate - StartDate).TotalMinutes);
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/graph/binance/{CoinBase}/usdt?since={since}&limit={limit}&source={source}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            Graph[] graphs = JsonConvert.DeserializeObject<Graph[]>(response);
            return graphs.ToList();
        }

        public async Task<ShowAccount> ShowAccount(string accountId, string token)
        {

            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            ShowAccount show = JsonConvert.DeserializeObject<ShowAccount>(response);
            return show;
        }

        public async Task<List<ShowAccount>> ShowAccounts(string token)
        {
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            List<ShowAccount> show = JsonConvert.DeserializeObject<List<ShowAccount>>(response);
            return show;
        }

        public async Task EnablePair(string accountId, string token, string @base)
        {
            string quote = "usdt";
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/pairs/{@base}/{quote}/enable"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };
            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
        }

        public async Task DisablePair(string accountId, string token, string @base)
        {
            string quote = "usdt";
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://46.101.131.228/api/accounts/{accountId}/pairs/{@base}/{quote}/disable"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };
            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
        }

        public async Task<bool> isKeyAvailable(string token)
        {
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://46.101.131.228/api/account"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = Client.SendAsync(Request).Result.IsSuccessStatusCode;
            return response;
        }

    }
}
