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

        public UserContext()
        {
        }
    }
}
