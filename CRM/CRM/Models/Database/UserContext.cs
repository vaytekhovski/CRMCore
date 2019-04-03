using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Models
{
    public class UserContext : DbContext //TODO: move from heere to service/database
    {
        public DbSet<UserModel> UserModels { get; set; }
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
