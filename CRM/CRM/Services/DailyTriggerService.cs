using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace CRM.Services
{
    public class DailyTriggerService
    {
        public static TimeSpan TriggerHour { get; private set; }

        public DailyTriggerService()
        {
            StartDeilyTrigger();
        }

        public static void StartDeilyTrigger()
        {
            UpdateDailyTrigger();
            InitiateAsync();
        }

        private static void UpdateDailyTrigger()
        {
            using (CRMContext context = new CRMContext())
            {
                foreach (var item in context.DailyUpdates)
                {
                    TriggerHour = item.dailyTrigger;
                    break;
                }

                Debug.WriteLine($"Daily update time changed to {TriggerHour}");
            }
        }

        async static void InitiateAsync()
        {
            while (true)
            {
                var triggerTime = DateTime.Today + TriggerHour - DateTime.Now;
                if (triggerTime < TimeSpan.Zero)
                    triggerTime = triggerTime.Add(new TimeSpan(24, 0, 0));
                await Task.Delay(triggerTime);
                try
                {
                    OnTimeTriggered?.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }

        public static event Action OnTimeTriggered;

        public static void ChangeDailyTrigger(TimeSpan newDailyTrigger)
        {
            using (CRMContext context = new CRMContext())
            {
                context.DailyUpdates.FirstOrDefault(x => x.Id == 1).dailyTrigger = newDailyTrigger;
                context.SaveChanges();
            }

            StartDeilyTrigger();
        }
    }
}