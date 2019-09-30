using Business.Models.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Contexts
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Orders> Orders { get; set; }

        public virtual DbSet<SignalsPrivate> SignalsPrivate { get; set; }

        public virtual DbSet<TradeHistoryDelta> TradeHistoryDelta { get; set; }

        public virtual DbSet<TradeHistory> TradeHistory { get; set; }

        public virtual DbSet<IndicatorPoints> IndicatorPoints { get; set; }

        public virtual DbSet<IndicatorValues> IndicatorValues { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=159.65.126.124;user=readonly;password=0nTM0NoPqeK7VxWZ;database=master");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndicatorPoints>().HasKey(p => new { p.Exchange, p.Base, p.Quote, p.Time });
            modelBuilder.Entity<TradeHistory>().HasKey(p => new { p.Exchange, p.Base, p.Quote, p.Time });
            modelBuilder.Entity<IndicatorValues>().HasKey(p => new { p.Exchange, p.Base, p.Quote, p.Time });
        }
    }
}

