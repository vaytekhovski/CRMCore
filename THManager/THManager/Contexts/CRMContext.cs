﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using THManager.Models;

namespace Contexts
{
    public class CRMContext : DbContext 
    {

        public DbSet<ExchangeKey> ExchangeKeys { get; set; }

        public DbSet<AccountTradeHistory> AccountTradeHistories { get; set; }

        public CRMContext(DbContextOptions<CRMContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=134.209.231.178; Database=CRMCoreDB; User Id=sa;Password = newPassw0rd; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18, 6)";
            }
        }

        public CRMContext()
        {
        }

    }
}
