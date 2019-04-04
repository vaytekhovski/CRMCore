using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class UserContext : DbContext //TODO: [COMPLETE] move from here to service/database
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
