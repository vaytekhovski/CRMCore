using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business;
using Business.Contexts;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using Newtonsoft.Json;

namespace Jobs
{
    class Loader
    {
        private readonly DatavisioAPIService datavisioAPI;

        public Loader()
        {
            datavisioAPI = new DatavisioAPIService();

        }
        public async Task<List<Orders>> LoadOrders(DateTime timeToLoad)
        {
            /*
            using (MySQLContext context = new MySQLContext())
            {
                return context.Orders.Where(x => x.TimeEnded > timeToLoad && x.AccountId != "bccd3ca1-0b5e-41ac-8233-3a35209912c7").OrderBy(x => x.TimeEnded).ToList();
            }*/

            var Client = new HttpClient();
            var token = datavisioAPI.Authorization().Result;
            var Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://159.65.126.124/api/orders"),
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
