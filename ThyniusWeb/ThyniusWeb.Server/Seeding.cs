using ReactApp1.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThyniusWeb.Server.Models;

namespace ThyniusWeb.Server
{
    public static class Seeding
    {
        private async static Task SeedUser(UserManager<ApplicationUser> userManager, string email, string fullName, string password, string organization, string[] roles)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser();
                user.UserName = email;
                user.Email = email;
                user.FullName = fullName;
                user.OrganizationEmailExtension = organization;

                await userManager.CreateAsync(user, password);
                foreach (var role in roles)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
        private static async Task SeedUsers(IServiceScope scope, IConfiguration config)
        {

            UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await SeedUser(userManager, config["Seeding:AdminEmail"], config["Seeding:AdminFullname"], config["Seeding:AdminPassword"], config["Seeding:Organization"], ["Admin", "VerifiedUser"]);
            await SeedUser(userManager, config["Seeding:TeacherEmail"], config["Seeding:TeacherFullname"], config["Seeding:TeacherPassword"], config["Seeding:Organization"], ["Teacher", "VerifiedUser"]);
            await SeedUser(userManager, config["Seeding:StudentEmail"], config["Seeding:StudentFullname"], config["Seeding:StudentPassword"], config["Seeding:Organization"], ["VerifiedUser"]);
        }
        private static async Task SeedRoles(IServiceScope scope)
        {
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "OrganizationAdmin", "Teacher", "VerifiedUser" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public static async Task SeedDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                await dbContext.Database.MigrateAsync();

                await SeedRoles(scope);
                await SeedUsers(scope, config);

            }
        }
    }
}