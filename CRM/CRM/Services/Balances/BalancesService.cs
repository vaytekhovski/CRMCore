using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CRM.ViewModels.Balances;
using Newtonsoft.Json;

namespace CRM.Services.Balances
{
    public class BalancesService
    {
        public BalancesService()
        {
        }

        private static string ResponseBody;

        public static List<Balance> LoadBalances(string AccountId)
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

            var response = client.SendAsync(httpRequestMessage).Result;
            GetResponseBodyAsync(response);

            Currencies currencies = JsonConvert.DeserializeObject<Currencies>(ResponseBody);
            List<Balance> balance = new List<Balance>();

            try
            {
                balance = new List<Balance>
                {
                    new Balance("USDT",currencies.USDT,0),
                    new Balance("BTC", (currencies.BTC), 0),
                    new Balance("BNB", (currencies.BNB), 0),
                    new Balance("EOS", (currencies.EOS), 0),
                    new Balance("ETH", (currencies.ETH), 0),
                    new Balance("XRP", (currencies.XRP), 0),
                    new Balance("LTC", (currencies.LTC), 0),
                    new Balance("TRX", (currencies.TRX), 0),
                    new Balance("ZEC", (currencies.ZEC), 0),
                    new Balance("DASH", (currencies.DASH), 0),
                    new Balance("XMR", (currencies.XMR), 0),
                    new Balance("ONT", (currencies.ONT), 0),
                    new Balance("ADA", (currencies.ADA), 0),
                    new Balance("XMR", (currencies.XMR), 0)
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            return balance;
        }

        public static async void GetResponseBodyAsync(HttpResponseMessage response)
        {
            ResponseBody = await response.Content.ReadAsStringAsync();
        }
    }


    public class Currencies
    {
        public string USDT { get; set; }
        public string BTC { get; set; }
        public string BNB { get; set; }
        public string EOS { get; set; }
        public string ETH { get; set; }
        public string XRP { get; set; }
        public string LTC { get; set; }
        public string TRX { get; set; }
        public string ZEC { get; set; }
        public string DASH { get; set; }
        public string XMR { get; set; }
        public string ONT { get; set; }
        public string XLM { get; set; }
        public string ADA { get; set; }
        public string BCHABC { get; set; }
    }
}
