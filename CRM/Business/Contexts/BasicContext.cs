using Microsoft.EntityFrameworkCore;
using System.Linq;
using Business.Data;

namespace Business.Contexts
{
    public class BasicContext : DbContext
    {
        public BasicContext()
        {
        }

        public BasicContext(DbContextOptions<BasicContext> options)
            : base(options)
        {
        }
        public DbSet<OrderBookModel> OrderBookModels { get; set; }
        public DbSet<TradeDeltaModel> TradeDeltaModels { get; set; }
        public DbSet<Business.Data.TradeHistoryModel> TradeHistoryModels { get; set; }
        
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<DailyUpdate> DailyUpdates { get; set; }
        public DbSet<AccountTradeHistory> AccountTradeHistories { get; set; }
        public DbSet<WrongOrders> WrongOrders { get; set; }
        public DbSet<IgnoreIds> IgnoreIds { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ExchangeKey> ExchangeKeys { get; set; }

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

    }
}
