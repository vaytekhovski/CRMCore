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

        public async Task<BalancesModel> LoadBalancesAsync(string accountId, string token)
        {
            BalancesModel balancesModel = new BalancesModel();

            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "USDT").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "BTC").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "ETH").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "LTC").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, "XRP").Result);

            return balancesModel;
        }

        public async Task<BalancesModel> LoadBalancesAsync(string accountId, string token, string Coin)
        {
            BalancesModel balancesModel = new BalancesModel();
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(accountId, token, Coin).Result);

            return balancesModel;
        }

    }

    
}
