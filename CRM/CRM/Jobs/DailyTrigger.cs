using CRM.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Jobs
{
    public class DailyTrigger : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            DailyTriggerService dailyTrigger = new DailyTriggerService();
            
        }
    }
}
