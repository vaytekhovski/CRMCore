using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using THManager.Models;

namespace THManager
{
    public class MySqlContext : DbContext
    {
        public MySqlContext()
        {
        }

        public MySqlContext(DbContextOptions<MySqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }

        public virtual DbSet<SignalsPrivate> SignalsPrivate { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=159.65.126.124;user=readonly;password=0nTM0NoPqeK7VxWZ;database=master; default command timeout=120");
            }
        }

    }
}
