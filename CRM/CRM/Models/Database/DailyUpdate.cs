using System;

namespace CRM.Services.Database
{
    public class DailyUpdate
    {
        public int Id { get; set; }
        public TimeSpan dailyTrigger { get; set; }
    }
}