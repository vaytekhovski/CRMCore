using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Models.DataVisioAPI;
using CRM.Helpers;
using Newtonsoft.Json;

namespace CRM.Services.DataVisioAPI
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
            var token = Authorization();
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

            return JsonConvert.DeserializeObject<WalletCurrency>(await Client.SendAsync(Request).Result.Content.ReadAsStringAsync());
        }

        public async Task<string> PlaceOrder(PlaceOrderRequest placeOrderModel)
        {
            var Client = new HttpClient();
            var token = Authorization();

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
    }
}
