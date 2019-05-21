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
            Console.WriteLine($"\n{DateTime.Now} | Load Orders started");
            var Orders = Loader.LoadOrders();
            Console.WriteLine($"{DateTime.Now} | Load Orders ended\n");

            Console.WriteLine($"{DateTime.Now} | Change Orders started");
            var TH = Changer.ChangeOrdersBeforeCalculate(Orders);
            Console.WriteLine($"{DateTime.Now} | Change Orders ended\n");

            Console.WriteLine($"{DateTime.Now} | Update Profit and load to DB started");
            ProfitUpdater.UpdateProfit(TH);
            Console.WriteLine($"{DateTime.Now} | Update Profit and load to DB ended\n");
            
            DrowLine();
        }

        static void DrowLine()
        {
            for (int i = 0; i < 70; i++)
            {
                Console.Write("*");
            }
        }
    }
}
