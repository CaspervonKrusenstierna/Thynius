
using ReactApp1.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThyniusWeb.Server.Models;
using Microsoft.Extensions.DependencyInjection;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Repository;
using Amazon.S3;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace ThyniusWeb.Server
{
    public class Program
    {
        private static void AddRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddScoped<IUserTextRepository, UserTextRepository>();
        }

        private static void SetupAWS(WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
            builder.Services.AddAWSService<IAmazonS3>();
        }

        private static void ConfigureLocalization(WebApplicationBuilder builder)
        {
            builder.Services.Configure<RequestLocalizationOptions>(
             opts =>
             {
                 var supportedCultures = new List<CultureInfo>
             {
                new CultureInfo("en"),
                new CultureInfo("sv-SE"),
             };

                 opts.DefaultRequestCulture = new RequestCulture("en");
                 opts.SupportedCultures = supportedCultures;
                 opts.SupportedUICultures = supportedCultures;
             });
        }

        private static void SetupDatabase(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Defaultconnection' not found.");
            object value = builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        private static async void SeedDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();

                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roles = new[] { "Admin", "OrganizationAdmin", "Teacher", "VerifiedUser" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                string AdminAccEmail = "";
                string AdminAccPassword = "";

                if (await userManager.FindByEmailAsync(AdminAccEmail) == null)
                {
                    var user = new ApplicationUser();
                    user.UserName = AdminAccEmail;
                    user.Email = AdminAccEmail;
                    user.FullName = "Casper vk";
                    user.OrganizationEmailExtension = "outlook.com";

                    await userManager.CreateAsync(user, AdminAccPassword);
                    await userManager.AddToRoleAsync(user, "Admin");
                    await userManager.AddToRoleAsync(user, "VerifiedUser");
                }

                ApplicationUser applicationUser = await userManager.FindByEmailAsync("teacher.testaccount@outlook.com");
                if (applicationUser != null)
                {
                    await userManager.AddToRoleAsync(applicationUser, "Teacher");
                }

            }
        }
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SetupDatabase(builder);
            builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            ConfigureLocalization(builder);
            AddRepositories(builder);
            SetupAWS(builder);

            var app = builder.Build();

            app.UseRequestLocalization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapCustomIdentityApi<ApplicationUser>();

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

            #if DEBUG
            SeedDatabase(app);
            #endif
            app.Run();

        }
    }
}
