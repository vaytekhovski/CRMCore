using Microsoft.EntityFrameworkCore;
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

        public CRMContext()
        {
        }

    }
}
