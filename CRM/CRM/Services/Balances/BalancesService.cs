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

        public async Task<BalancesModel> LoadBalancesAsync(HttpContext httpContext)
        {

            BalancesModel balancesModel = new BalancesModel();

            balancesModel.InsterBalance(datavisioAPIService.GetBalance(httpContext, "USDT").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(httpContext, "BTC").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(httpContext, "ETH").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(httpContext, "LTC").Result);
            balancesModel.InsterBalance(datavisioAPIService.GetBalance(httpContext, "XRP").Result);


            return balancesModel;
        }

    }

    
}
