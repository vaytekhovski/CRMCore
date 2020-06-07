using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Contexts;
using Business.Models.DataVisioAPI;
using CRM.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Business.DataVisioAPI
{
    public class DatavisioAPIService
    {
        public DatavisioAPIService()
        {

        }

        public async Task<string> Authorization(int UserId)
        {
            var Client = new HttpClient();
            var uri = $"http://159.65.126.124/api/auth";


            UserModel User = new UserModel();
            using (BasicContext context = new BasicContext())
            {
                User = context.UserModels.FirstOrDefault(x => x.Id == UserId);
            }

            LoginModel login = new LoginModel
            {
                username = User.Login,
                password = User.Password
            };

            var jsonInString = JsonConvert.SerializeObject(login);
            var resp = await Client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            AuthenticationResnose authenticationResnose = JsonConvert.DeserializeObject<AuthenticationResnose>(resp.Content.ReadAsStringAsync().Result);

            return authenticationResnose.token;
        }

        public async Task<string> Authorization(LoginModel login)
        {
            var Client = new HttpClient();
            var uri = $"https://api.datavisio.net/auth";

            var jsonInString = JsonConvert.SerializeObject(login);
            var resp = await Client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            AuthenticationResnose authenticationResnose = JsonConvert.DeserializeObject<AuthenticationResnose>(resp.Content.ReadAsStringAsync().Result);

            return authenticationResnose.token;
        }

        public async Task<WalletCurrency> GetBalance(string token, string CoinBase)
        {
            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/exchange/binance/wallets/{CoinBase}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            WalletCurrency walletCurrency = JsonConvert.DeserializeObject<WalletCurrency>(await Client.SendAsync(Request).Result.Content.ReadAsStringAsync());
            walletCurrency.coin = CoinBase;

            return walletCurrency;
        }

        public async Task<string> EnterDeal(string token, PlaceOrderRequest placeOrderModel)
        {
            var Client = new HttpClient();

            var jsonInString = JsonConvert.SerializeObject(placeOrderModel);

            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://159.65.126.124/api/account/deals"),
                Headers =
                {
                    { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(jsonInString, Encoding.UTF8, "application/json")
            };

            var response = JsonConvert.DeserializeObject<PlaceOrderResponse>(await Client.SendAsync(Request).Result.Content.ReadAsStringAsync());

            return response.id;
        }

        public async Task<string> TradeDeal(string token,string DealId, double amount)
        {
            var Client = new HttpClient();
            TradeDealRequest tradeDealRequest = new TradeDealRequest
            {
                amount = amount
            };
            var jsonInString = JsonConvert.SerializeObject(tradeDealRequest);

            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://159.65.126.124/api/account/deals/{DealId}"),
                Headers =
                {
                    { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(jsonInString, Encoding.UTF8, "application/json")
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string>(response);
        }

        public async Task<string> LeaveDeal(string token, string DealId)
        {
            var Client = new HttpClient();


            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://159.65.126.124/api/account/deals/{DealId}"),
                Headers =
                {
                    { "Authorization", "Bearer " + token }
                }
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string>(response);
        }



        public async Task<Signals>  GetSignals(string token, string CoinBase)
        {
            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/signals/binance/{CoinBase}/usdt?limit=2000"),
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
            var Client = new HttpClient();
            var Since = (long)(DateTime.UtcNow.AddHours(-3).Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/exchange/binance/{CoinBase}/usdt/candles?frame=1&since={Since}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Candles[]>(response);
        }

        public async Task<ListDeals> GetListDeals(string token)
        {
            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/account/deals?limit=2000"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ListDeals>(response);
        }

        public async Task<ListDeals> GetListErrorDeals(string token)
        {
            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/account/deals?failed=true"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ListDeals>(response);
        }

        public async Task<Deal> GetDeal(string token, string DealId)
        {
            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/account/deals/{DealId}"),
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

        public async Task<List<Graph>> GetGraphs(string token, string CoinBase, DateTime StartDate, DateTime EndDate)
        {
            var since = ((DateTimeOffset)StartDate).ToUnixTimeSeconds();
            var limit = Convert.ToInt32((EndDate - StartDate).TotalMinutes);

            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/graph/binance/{CoinBase}/usdt?since={since}&limit={limit}"),
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

        public async Task<ShowAccount> ShowAccount(string token)
        {
            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/account"),
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

        public async Task EnablePair(string token, string @base)
        {
            string quote = "usdt";

            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://159.65.126.124/api/account/binance/{@base}/{quote}/enable"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };
            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
        }

        public async Task DisablePair(string token, string @base)
        {
            string quote = "usdt";
            var Client = new HttpClient();
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://159.65.126.124/api/account/binance/{@base}/{quote}/disable"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };
            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
        }

        
    }
}
