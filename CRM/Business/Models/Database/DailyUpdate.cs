using System;

namespace Business
{
    public class DailyUpdate
    {
        public int Id { get; set; }
        public TimeSpan dailyTrigger { get; set; }
    }
}