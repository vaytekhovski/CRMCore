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
            var uri = $"http://159.65.126.124/api/login";


            UserModel User = new UserModel();
            using (BasicContext context = new BasicContext())
            {
                User = context.UserModels.FirstOrDefault(x => x.Id == UserId);
            }

            LoginModel login = new LoginModel
            {
                Login = User.Login,
                Password = User.Password
            };

            var jsonInString = JsonConvert.SerializeObject(login);
            var resp = await Client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            AuthenticationResnose authenticationResnose = JsonConvert.DeserializeObject<AuthenticationResnose>(resp.Content.ReadAsStringAsync().Result);

            return authenticationResnose.token;
        }

        public async Task<string> Authorization(LoginModel login)
        {
            var Client = new HttpClient();
            var uri = $"http://159.65.126.124/api/login";

            var jsonInString = JsonConvert.SerializeObject(login);
            var resp = await Client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            AuthenticationResnose authenticationResnose = JsonConvert.DeserializeObject<AuthenticationResnose>(resp.Content.ReadAsStringAsync().Result);

            return authenticationResnose.token;
        }

        public async Task<WalletCurrency> GetBalance(HttpContext httpContext, string CoinBase)
        {
            var Client = new HttpClient();
            var token = Authorization(Convert.ToInt32(httpContext.User.Identity.Name)).Result;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/exchange/wallets/{CoinBase}"),
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

        public async Task<string> PlaceOrder(HttpContext httpContext, PlaceOrderRequest placeOrderModel)
        {
            var Client = new HttpClient();
            var token = Authorization(Convert.ToInt32(httpContext.User.Identity.Name)).Result;

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            var jsonInString = JsonConvert.SerializeObject(placeOrderModel);

            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://159.65.126.124/api/orders"),
                Headers =
                {
                    { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(jsonInString, Encoding.UTF8, "application/json")
            };

            var response = JsonConvert.DeserializeObject<PlaceOrderResponse>(await Client.SendAsync(Request).Result.Content.ReadAsStringAsync());

            return response.id;
        }

        public async Task<Signals> GetSignals(HttpContext httpContext, string CoinBase)
        {
            var Client = new HttpClient();
            var token = Authorization(Convert.ToInt32(httpContext.User.Identity.Name)).Result;
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

        public async Task<Candles[]> GetCandles(HttpContext httpContext, string CoinBase)
        {
            var Client = new HttpClient();
            var token = Authorization(Convert.ToInt32(httpContext.User.Identity.Name)).Result;
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

        public async Task<List<Order>> GetOrderList(HttpContext httpContext)
        {
            var Client = new HttpClient();
            var token = Authorization(Convert.ToInt32(httpContext.User.Identity.Name)).Result;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/orders?limit=5000"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            ListOrderModel OrderList = JsonConvert.DeserializeObject<ListOrderModel>(response);
            return OrderList.orders.ToList();
        }

        public async Task<List<Order>> GetOrderList()
        {
            var Client = new HttpClient();
            var token = Authorization(new LoginModel { Login = "Boss", Password = "9Qj7RTUdMF7C3Pf8" }).Result;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/orders?limit=5000"),
                Headers =
                {
                     { "Authorization", "Bearer " + token }
                },
                Content = new StringContent(string.Empty)
            };

            var response = await Client.SendAsync(Request).Result.Content.ReadAsStringAsync();
            ListOrderModel OrderList = JsonConvert.DeserializeObject<ListOrderModel>(response);
            return OrderList.orders.ToList();
        }

    }
}
