using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Models.DataVisioAPI;
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
    }
}
