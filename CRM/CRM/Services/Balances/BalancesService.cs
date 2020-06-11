using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        public async Task<BalancesModel> LoadBalancesAsync(string token)
        {
            BalancesModel balancesModel = new BalancesModel();

            balancesModel.InsterBalance(datavisioAPIService.GetBalance(token, "USDT").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(token, "BTC").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(token, "ETH").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(token, "LTC").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(token, "XRP").Result);

            return balancesModel;
        }

        public async Task<BalancesModel> LoadBalancesAsync(string token, string Coin)
        {
            BalancesModel balancesModel = new BalancesModel();
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(token, Coin).Result);

            return balancesModel;
        }

    }

    
}
