using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using CRM.ViewModels.Balances;
using Newtonsoft.Json;

namespace CRM.Services.Balances
{
    public class BalancesService
    {


        public async Task<BalancesModel> LoadBalancesAsync(string AccountId)
        {
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124:5000/ccxt/balance/{AccountId}"),
                Headers = {
                     { "password", "1234567890QWERTYasd!@" }
                 },
                Content = new StringContent(string.Empty)
            };

            Currencies currencies;
            using (var response = client.SendAsync(httpRequestMessage))
            {
                currencies = JsonConvert.DeserializeObject<Currencies>(await response.Result.Content.ReadAsStringAsync());
            }

            var balance = InitiateBalance(currencies);
            var balancesModel = new BalancesModel { AccountBalances = balance };

            return balancesModel;
        }


        private List<Balance> InitiateBalance(Currencies currencies)
        {
            var balance = new List<Balance>();
            try
            {
                balance = new List<Balance>
                {
                    new Balance("USDT",currencies.USDT, 0),
                    new Balance("BTC", currencies.BTC, 0),
                    new Balance("ETH", currencies.ETH, 0),
                    new Balance("LTC", currencies.LTC, 0)
                };

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            foreach (var item in balance)
            {
                var dAmount = Double.Parse(item.Amount, CultureInfo.InvariantCulture);
                item.buttonDisabled = dAmount > 0.001 ? "false" : "true";
            }
            return balance;
        }

    }


    public class Currencies
    {
        public string USDT { get; set; }
        public string BTC { get; set; }
        public string ETH { get; set; }
        public string LTC { get; set; }
    }
}
