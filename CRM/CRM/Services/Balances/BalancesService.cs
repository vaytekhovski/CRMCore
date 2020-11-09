using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business;
using Business.DataVisioAPI;
using Business.Models.DataVisioAPI;
using CRM.ViewModels.Balances;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CRM.Services.Balances
{
    public class BalancesService
    {
        public BalancesService()
        {
            datavisioAPIService = new DatavisioAPIService();
        }
        DatavisioAPIService datavisioAPIService;

        public async Task<BalancesModel> LoadBalancesAsync(string accountId, string token, string type = "debit")
        {
            BalancesModel balancesModel = new BalancesModel();

            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "USDT", type).Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "BTC", type).Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "ETH", type).Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "LTC", type).Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "XRP", type).Result);

            return balancesModel;
        }

        public async Task<BalancesModel> LoadBalancesAsync(string accountId, string token, string Coin, string type = "debit")
        {
            BalancesModel balancesModel = new BalancesModel();
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, Coin, type).Result);

            return balancesModel;
        }

    }

    
}
