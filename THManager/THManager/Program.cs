using System;

namespace THManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now} | Load Orders started");
            var Orders = Loader.LoadOrders();
            Console.WriteLine($"{DateTime.Now} | Load Orders ended");

            Console.WriteLine($"{DateTime.Now} | Change Orders started");
            var TH = Changer.ChangeOrdersBeforeCalculate(Orders);
            Console.WriteLine($"{DateTime.Now} | Change Orders ended");

            Console.WriteLine($"{DateTime.Now} | Update Profit and load toDB started");
            ProfitUpdater.UpdateProfit(TH);
            Console.WriteLine($"{DateTime.Now} | Update Profit and load toDB ended");

            Console.WriteLine("Done!");

            Console.ReadKey();
        }
    }
}
