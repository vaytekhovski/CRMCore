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

            balancesModel.InsterBalance(await datavisioAPIService.GetBalance(accountId, token, "USDT", type));
            balancesModel.InsterBalance(await datavisioAPIService.GetBalance(accountId, token, "USDC", type));
            balancesModel.InsterBalance(await datavisioAPIService.GetBalance(accountId, token, "BUSD", type));

            balancesModel.InsterBalance(await datavisioAPIService.GetBalance(accountId, token, "BTC", type));
            balancesModel.InsterBalance(await datavisioAPIService.GetBalance(accountId, token, "ETH", type));
            balancesModel.InsterBalance(await datavisioAPIService.GetBalance(accountId, token, "LTC", type));
            balancesModel.InsterBalance(await datavisioAPIService.GetBalance(accountId, token, "XRP", type));

            return balancesModel;
        }

        public async Task<BalancesModel> LoadBalancesAsync(string accountId, string token, string Coin, string type = "debit")
        {
            BalancesModel balancesModel = new BalancesModel();
            balancesModel.InsterBalance(await datavisioAPIService.GetBalance(accountId, token, Coin, type));

            return balancesModel;
        }

    }

    
}
