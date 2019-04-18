using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Master;
using CRM.Models;
using CRM.Models.Binance;
using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class THController : Controller
    {
        [HttpGet]
        public ActionResult TradeHistory()
        {
            var model = new TradeHistoryFilterModel
            {
                Account = "all",
                Coin = "all",
                StartDate = "2019-04-05",
                EndDate = CurrentDate()
            };

            THService tHService = new THService();

            tHService.Load(model.Account, model.Coin, model.StartDate, model.EndDate);

            DateTime StartDate = DateTime.Parse(model.StartDate);
            DateTime EndDate = DateTime.Parse(model.EndDate);

            model.Orders = tHService.AccountTradeHistories
                .OrderByDescending(x => x.Time)
                .Where(x => x.Time >= StartDate && x.Time <= EndDate)
                .ToList();

            model.TotalProfit = tHService.TotalProfit;
            model.TotalPercentProfit = tHService.TotalPercentProfit;

            return View(model);
        }

        


        [HttpPost] //TODO: [COMPLETE]  разметить везде, где GET, а где POST
        public ActionResult TradeHistory(TradeHistoryFilterModel model) 
        {
            THService tHService = new THService();

            tHService.Load(model.Account, model.Coin, model.StartDate, model.EndDate);

            DateTime StartDate = DateTime.Parse(model.StartDate);
            DateTime EndDate = DateTime.Parse(model.EndDate);

            model.Orders = tHService.AccountTradeHistories
                .OrderByDescending(x => x.Time)
                .Where(x => x.Time >= StartDate && x.Time <= EndDate)
                .ToList();

            model.TotalProfit = tHService.TotalProfit;
            model.TotalPercentProfit = tHService.TotalPercentProfit;
            

            return View(model);
        }

        private string CurrentDate()
        {
            var currentDate = DateTime.Now;

            string dd = currentDate.Day < 10 ? "0" + currentDate.Day.ToString() : currentDate.Day.ToString();
            string mm = currentDate.Month < 10 ? "0" + currentDate.Month.ToString() : currentDate.Month.ToString();
            string yy = currentDate.Year.ToString();
            string curDate = yy + "-" + mm + "-" + dd;
            return curDate;
        }
    }
}