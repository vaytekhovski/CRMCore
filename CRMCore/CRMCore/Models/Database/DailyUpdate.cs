﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMCore.Services.Database
{
    public class DailyUpdate
    {
        public int Id { get; set; }
        public TimeSpan dailyTrigger { get; set; }
    }
}