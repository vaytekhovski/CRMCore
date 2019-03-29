using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Models
{
    public class UserContext : DbContext
    {
        public DbSet<UserModel> UserModels { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
