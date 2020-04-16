using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business;
using Business.Models.DataVisioAPI;
using CRM.Services.DataVisioAPI;
using CRM.ViewModels.Balances;
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

        public async Task<BalancesModel> LoadBalancesAsync(string AccountId)
        {

            BalancesModel balancesModel = new BalancesModel();

            balancesModel.InsterBalance(datavisioAPIService.GetBalance("USDT").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance("BTC").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance("ETH").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance("LTC").Result);

            return balancesModel;
        }

    }

    
}
