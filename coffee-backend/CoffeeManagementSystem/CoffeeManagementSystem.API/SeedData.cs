using CoffeeManagementSystem.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;

namespace CoffeeManagementSystem.API
{
    public class SeedData
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider,IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            // Ensure Roles Exist
            var roles = new[] { "Admin", "Customer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create Admin User (from appsettings.json or hardcoded)
            string adminName = configuration["AdminUser:FullName"];
            string adminEmail = configuration["AdminUser:Email"];
            string adminPassword = configuration["AdminUser:Password"];

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            {
                Console.WriteLine("[Seed] AdminUser config missing or empty (check appsettings).");
                return;
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new AppUser
                {
                    UserName = adminEmail,
                    FullName = adminName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };



                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

        }
    }
}
