
using ReactApp1.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThemisWeb.Server.Models;
using Microsoft.Extensions.DependencyInjection;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Repository;
using Amazon.S3;


namespace ThemisWeb.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
            var AWSOptions = builder.Configuration.GetAWSOptions();

            object value = builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDefaultAWSOptions(AWSOptions);
            builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<ISubmittmentRepository, SubmittmentRepository>();
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();

            builder.Services.AddAWSService<IAmazonS3>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapIdentityApi<ApplicationUser>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            using(var scope = app.Services.CreateScope())
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roles = new[] { "Admin", "OrganizationAdmin", "Teacher", "VerifiedUser" };

                foreach (var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));      
                    }
                }

                string AdminAccEmail = "cappelappe@outlook.com";
                string AdminAccPassword = "Dalmas0099!";
 
                if (await userManager.FindByEmailAsync(AdminAccEmail) == null)
                {
                    var user = new ApplicationUser();
                    user.UserName = AdminAccEmail;
                    user.Email = AdminAccEmail;

                    await userManager.CreateAsync(user, AdminAccPassword);
                    await userManager.AddToRoleAsync(user, "Admin");
                    await userManager.AddToRoleAsync(user, "VerifiedUser");
                }

            }
            app.Run();

        }
    }
}
