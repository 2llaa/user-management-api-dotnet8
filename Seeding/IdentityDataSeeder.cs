using Microsoft.AspNetCore.Identity;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Seeding
{
    public static class IdentityDataSeeder
    {
   
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public static async Task SeedAdminUserAsync(UserManager<User> userManager, IConfiguration config)
        {
            var adminEmail = config["AdminUser:Email"];
            var adminPassword = config["AdminUser:Password"];

            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                var adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}


