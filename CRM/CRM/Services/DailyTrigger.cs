using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace CRM.Services
{
    public class DailyTriggerService
    {
        private static TimeSpan triggerHour;

        public static TimeSpan TriggerHour => triggerHour;

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
                    triggerHour = item.dailyTrigger;
                    break;
                }

                Debug.WriteLine($"Daily update time changed to {triggerHour}");
            }
        }

        async static void InitiateAsync()
        {
            while (true)
            {
                var triggerTime = DateTime.Today + triggerHour - DateTime.Now;
                if (triggerTime < TimeSpan.Zero)
                    triggerTime = triggerTime.Add(new TimeSpan(24, 0, 0));
                await Task.Delay(triggerTime);
                OnTimeTriggered?.Invoke();
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