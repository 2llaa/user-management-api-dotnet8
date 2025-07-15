using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using user_management_api_dotnet8.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace user_management_api_dotnet8.Data
{
       public class AppDbContext : IdentityDbContext<User>
       {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> AppUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
       }
}
