using System;

namespace THManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(2);

            var timer = new System.Threading.Timer((e) =>
            {
                Calculate();
            }, null, startTimeSpan, periodTimeSpan);
            
            Console.ReadKey();
        }

        static void Calculate()
        {
            Loader loader = new Loader();
            Console.WriteLine($"\n{DateTime.Now} | Load Orders started");
            var Orders = loader.LoadOrders();
            Console.WriteLine($"{DateTime.Now} | Load Orders ended\n");

            Changer changer = new Changer();
            Console.WriteLine($"{DateTime.Now} | Change Orders started");
            var TH = changer.ChangeOrdersBeforeCalculate(Orders);
            Console.WriteLine($"{DateTime.Now} | Change Orders ended\n");

            ProfitUpdater profitUpdater = new ProfitUpdater();
            Console.WriteLine($"{DateTime.Now} | Update Profit and load to DB started");
            profitUpdater.UpdateProfit(TH);
            Console.WriteLine($"{DateTime.Now} | Update Profit and load to DB ended\n");
            
            DrowLine();
        }

        static void DrowLine()
        {
            var starsStr = new string('*', 70);
            Console.Write(starsStr);
        }
    }
}
