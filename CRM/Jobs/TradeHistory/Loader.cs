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

        public async Task<List<Order>> LoadOrders()
        {
            return await datavisioAPI.GetOrderList();
        }
    }

    
}
