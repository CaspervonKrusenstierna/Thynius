using ReactApp1.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThyniusWeb.Server.Models;
using ThyniusWeb.Server.Repository;
using ThyniusWeb.Server.Interfaces;

namespace ThyniusWeb.Server
{
    public static class Seeding
    {
        private static async Task SeedUser(UserManager<ApplicationUser> userManager, string email, string fullName, string password, string organization, string[] roles)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = fullName,
                    OrganizationEmailExtension = organization
                };

                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    // Log error if user creation failed
                    throw new Exception($"Error creating user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            foreach (var role in roles)
            {
                if (!await userManager.IsInRoleAsync(user, role))
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
            IOrganizationRepository orgRepo = scope.ServiceProvider.GetRequiredService<IOrganizationRepository>();
            var orgs = await orgRepo.GetAllAsync();
            Organization org = orgs.First();
            ApplicationUser admin = await userManager.FindByEmailAsync(config["Seeding:AdminEmail"]);
            org.OwnerId = admin.Id;
            orgRepo.Update(org);
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

        private static async Task SeedOrganization(IServiceScope scope, IConfiguration config)
        {
            UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            IOrganizationRepository repo = scope.ServiceProvider.GetRequiredService<IOrganizationRepository>();
            if(await repo.GetByEmailExtensionAsync(config["Seeding:Organization"]) != null)
            {
                return;
            }
            Organization toAdd = new Organization();
            toAdd.EmailExtension = config["Seeding:Organization"];
            repo.Add(toAdd);
        }
        public static async Task SeedDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                await dbContext.Database.MigrateAsync();

                await SeedRoles(scope);
                await SeedOrganization(scope, config);
                await SeedUsers(scope, config);
            }
        }
    }
}