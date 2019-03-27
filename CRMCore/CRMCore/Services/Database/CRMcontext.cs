using CRMCore.Models.Database;
using CRMCore.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Services
{

    public class CRMCoreContext : DbContext
    {
        internal object first;

        public DbSet<OrderBookAsksModel> OrderBookAsksModels { get; set; }
        public DbSet<OrderBookBidsModel> OrderBookBidsModels { get; set; }
        public DbSet<TradeDeltaModel> TradeDeltaModels { get; set; }
        public DbSet<TradeHistoryModel> TradeHistoryModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<DailyUpdate> DailyUpdates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=134.209.231.178; Database=CRMCoreDB; User Id=sa;Password = newPassw0rd; ");
        }

        public CRMCoreContext()
        {
        }
    }

}