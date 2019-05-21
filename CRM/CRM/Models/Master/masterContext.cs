using System;
using CRM.Models.Binance;
using CRM.Models.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRM.Master
{
    public partial class masterContext : DbContext
    {
        public masterContext()
        {
        }

        public masterContext(DbContextOptions<masterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }

        public virtual DbSet<SignalsPrivate> SignalsPrivate { get; set; }

        public virtual DbSet<TradeHistoryDelta> TradeHistoryDelta { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=159.65.126.124;user=readonly;password=0nTM0NoPqeK7VxWZ;database=master");
            }
        }

    }
}
