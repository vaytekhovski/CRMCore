using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Jobs;
using Jobs.API;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;

namespace QuartzSampleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            RunProgramRunExample().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

        private static async Task RunProgramRunExample()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                await scheduler.Start();
                
                IJobDetail everyTwoMinLoading = JobBuilder.Create<EveryTwoMinLoadingJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                IJobDetail dailyLoading = JobBuilder.Create<DailyLoadingJob>()
                    .WithIdentity("job2", "group2")
                    .Build();

                IJobDetail APILoading = JobBuilder.Create<GetDataFromAPI>()
                   .WithIdentity("job3", "group3")
                   .Build();


                ITrigger everyTwoMinTrigger = TriggerBuilder.Create()
                   .WithIdentity("trigger1", "group1")
                   .StartNow()
                   .WithSchedule(CronScheduleBuilder.CronSchedule("0 1/2 * ? * *"))
                   .Build();

                ITrigger dailyTrigger = TriggerBuilder.Create()
                    .WithIdentity("trigger2", "group2")
                    .StartNow()
                    .WithSchedule(CronScheduleBuilder.CronSchedule("0 1/2 ? * *"))
                    .Build();

                ITrigger APITrigger = TriggerBuilder.Create()
                    .WithIdentity("trigger3", "group3")
                    .StartNow()
                    .WithSchedule(CronScheduleBuilder.CronSchedule("25 56 13 ? * *"))
                    .Build();



                //await scheduler.ScheduleJob(everyTwoMinLoading, everyTwoMinTrigger);
                await scheduler.ScheduleJob(dailyLoading, dailyTrigger);
                //await scheduler.ScheduleJob(APILoading, APITrigger);


                // and last shut down the scheduler when you are ready to close your program
                //await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        // simple log provider to get something to the console
        private class ConsoleLogProvider : ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (level >= LogLevel.Info && func != null)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                    }
                    return true;
                };

            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class EveryTwoMinLoadingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            LoadingAndCalculatingTradeHistory.EveryTwoMinCalculate();
        }
    }
    public class DailyLoadingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            LoadingAndCalculatingTradeHistory.DailyCalculate();
        }
    }

    public class GetDataFromAPI : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            LoadingDataFromAPI loadingDataFromAPI = new LoadingDataFromAPI();
        }
    }

}