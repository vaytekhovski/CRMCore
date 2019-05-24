using CRM.Models.Binance;
using CRM.Models.Database;
using CRM.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{

    public class CRMContext : DbContext
    {
        public DbSet<OrderBookModel> OrderBookModels { get; set; }
        public DbSet<TradeDeltaModel> TradeDeltaModels { get; set; }
        public DbSet<TradeHistoryModel> TradeHistoryModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<DailyUpdate> DailyUpdates { get; set; }

        public DbSet<AccountTradeHistory> AccountTradeHistories { get; set; }
        public DbSet<SecondAccountTH> SecondAccountTHs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=134.209.231.178; Database=CRMCoreDB; User Id=sa;Password = newPassw0rd; ");
        }

        public CRMContext()
        {
        }
    }

}