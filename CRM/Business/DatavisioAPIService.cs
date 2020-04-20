using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Models.DataVisioAPI;
using CRM.Helpers;
using Newtonsoft.Json;

namespace Business.DataVisioAPI
{
    public class DatavisioAPIService
    {
        public DatavisioAPIService()
        {

        }

        public async Task<string> Authorization()
        {
            var Client = new HttpClient();
            var uri = $"http://159.65.126.124/api/login";
            LoginModel login = new LoginModel { Login = "datavisio", Password = "9Qj7RTUdMF7C3Pf8" };

            var jsonInString = JsonConvert.SerializeObject(login);
            var resp = await Client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            AuthenticationResnose authenticationResnose = JsonConvert.DeserializeObject<AuthenticationResnose>(resp.Content.ReadAsStringAsync().Result);

            return authenticationResnose.token;
        }

        public async Task<WalletCurrency> GetBalance(string CoinBase)
        {
            var Client = new HttpClient();
            var token = Authorization().Result;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/wallets/{CoinBase}"),
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

        public async Task<string> PlaceOrder(PlaceOrderRequest placeOrderModel)
        {
            var Client = new HttpClient();
            var token = Authorization().Result;

            SeparateHelper.Separator.NumberDecimalSeparator = ".";


            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/login"),
                Headers =
                {
                     { "type", placeOrderModel.type },
                    { "side",placeOrderModel.side},
                    {"base",placeOrderModel.@base },
                    {"quote",placeOrderModel.quote },
                    {"amount",placeOrderModel.amount.ToString(SeparateHelper.Separator) }
                },
                Content = new StringContent(string.Empty)
            };

            return JsonConvert.DeserializeObject<PlaceOrderResponse>(await Client.SendAsync(Request).Result.Content.ReadAsStringAsync()).id;
        }

        public async Task<Signals> GetSignals(string CoinBase)
        {
            var Client = new HttpClient();
            var token = Authorization().Result;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/signals/{CoinBase}/usdt?limit=360"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Signals>(response);
        }

        public async Task<Candles[]> GetCandles(string CoinBase)
        {
            var Client = new HttpClient();
            var token = Authorization().Result;
            var Since = (long)(DateTime.UtcNow.AddHours(-3).Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/exchange/{CoinBase}/usdt/candles?frame=1&since={Since}"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Candles[]>(response);
        }

    }
}
