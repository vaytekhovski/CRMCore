using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class UserContext : DbContext //TODO: [COMPLETE] move from here to service/database
    {
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<Role> Roles { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public UserContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            UserModel adminUser = new UserModel { Id = 1, Login = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<UserModel>().HasData(new UserModel[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
