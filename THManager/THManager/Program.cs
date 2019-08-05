using System;

namespace THManager
{
    public class Program
    {

        private static bool isDailyLoadEnded;

        static void Main(string[] args)
        {
            var startTimeSpan = TimeSpan.Zero;
            // TODO: schedule with cron, rename project to jobs
            var DailyPeriodTimeSpan = TimeSpan.FromHours(24);
            var DailyTimer = new System.Threading.Timer((e) =>
            {
                isDailyLoadEnded = false;
                DailyCalculate();
                isDailyLoadEnded = true;
            }, null, startTimeSpan, DailyPeriodTimeSpan);


            var regularPeriodTimeSpan = TimeSpan.FromMinutes(2);
            var RegularTimer = new System.Threading.Timer((e) =>
            {
                if(isDailyLoadEnded)
                    EveryTwoMinCalculate();
            }, null, startTimeSpan, regularPeriodTimeSpan);



            Console.ReadKey();
        }

        

    }
}
