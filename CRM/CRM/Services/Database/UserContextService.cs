using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class UserContext : DbContext 
    {
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<ExchangeKey> ExchangeKeys { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=134.209.231.178; Database=CRMCoreDB; User Id=sa;Password = newPassw0rd; ");
        }

        public UserContext()
        {
        }

    }
}
