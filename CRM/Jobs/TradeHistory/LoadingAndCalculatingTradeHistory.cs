﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business;
using Business.Contexts;

namespace Jobs
{
    public class LoadingAndCalculatingTradeHistory
    {
        public static void EveryTwoMinCalculate()
        {
            Loader loader = new Loader();
            Console.WriteLine($"\n{DateTime.Now} |EveryTwoMinCalculate| Load Orders started");
            var Orders = loader.LoadOrders().Result;
            Console.WriteLine($"{DateTime.Now} |EveryTwoMinCalculate| Load Orders ended");

            Changer changer = new Changer();
            Console.WriteLine($"\n{DateTime.Now} |EveryTwoMinCalculate| Change Orders started");
            var TH = changer.ChangeOrdersBeforeCalculate(Orders);
            Console.WriteLine($"{DateTime.Now} |EveryTwoMinCalculate| Change Orders ended");

            ProfitUpdater profitUpdater = new ProfitUpdater();
            Console.WriteLine($"\n{DateTime.Now} |EveryTwoMinCalculate| Update Profit and load to DB started");
            profitUpdater.UpdateProfit(TH);
            Console.WriteLine($"{DateTime.Now} |EveryTwoMinCalculate| Update Profit and load to DB ended");

            DrawLine();
        }

        public static async System.Threading.Tasks.Task DailyCalculateAsync()
        {
            DrawStars();

            Loader loader = new Loader();
            Console.WriteLine($"\n{DateTime.Now} |DailyCalculate| Load Orders started");
            var Orders = await loader.LoadOrders();

            Console.WriteLine($"{DateTime.Now} |DailyCalculate| Load Orders ended");

            Changer changer = new Changer();
            Console.WriteLine($"\n{DateTime.Now} |DailyCalculate| Change Orders started");
            var TH = changer.ChangeOrdersBeforeCalculate(Orders, false);
            Console.WriteLine($"{DateTime.Now} |DailyCalculate| Change Orders ended");

            ProfitUpdater profitUpdater = new ProfitUpdater();
            Console.WriteLine($"\n{DateTime.Now} |DailyCalculate| Update Profit and load to DB started");
            profitUpdater.UpdateProfit(TH, false);
            Console.WriteLine($"{DateTime.Now} |DailyCalculate| Update Profit and load to DB ended");

            DrawStars();
        }

        static void DrawLine()
        {
            var lineStr = new string('-', 80);
            Console.Write('\n' + lineStr);
        }

        static void DrawStars()
        {
            var starStr = new string('*', 80);
            Console.Write('\n' + starStr);
        }
    }
}
