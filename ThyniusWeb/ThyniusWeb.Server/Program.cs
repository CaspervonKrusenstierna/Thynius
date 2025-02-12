
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
using Amazon;


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
            var awsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var awsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var awsRegion = Environment.GetEnvironmentVariable("AWS_REGION") ?? "us-west-2";
            var isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";

            var awsOptions = builder.Configuration.GetAWSOptions();

#if DEBUG
            // If we're in development (or testing), configure Minio
            var minioUrl = Environment.GetEnvironmentVariable("MINIO_URL") ?? "http://localhost:9000";
            var minioAccessKey = Environment.GetEnvironmentVariable("MINIO_ACCESS_KEY") ?? "minioaccesskey";
            var minioSecretKey = Environment.GetEnvironmentVariable("MINIO_SECRET_KEY") ?? "miniosecretkey";

            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(awsRegion),
                ServiceURL = minioUrl,
                ForcePathStyle = true,
                UseHttp = true
            };

            builder.Services.AddSingleton<IAmazonS3>(sp =>
            {
                return new AmazonS3Client(minioAccessKey, minioSecretKey, config);
            });
#else
            // If we're in production, use AWS default options (either IAM roles or credentials from environment variables)
            builder.Services.AddAWSService<IAmazonS3>();
#endif

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
            var mysqlHost = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? throw new InvalidOperationException("MYSQL_HOST Environment variable not found.");
            var mysqlPort = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? throw new InvalidOperationException("MYSQL_PORT Environment variable not found.");
            var mysqlDatabase = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new InvalidOperationException("MYSQL_DATABASE Environment variable not found.");
            var mysqlUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new InvalidOperationException("MYSQL_USER Environment variable not found.");
            var mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new InvalidOperationException("MYSQL_PASSWORD Environment variable not found.");

            var connectionString = $"Server={mysqlHost};Port={mysqlPort};Database={mysqlDatabase};User={mysqlUser};Password={mysqlPassword};SslMode=none;";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );
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
            await Seeding.SeedDatabase(app);
#endif
            app.Run();

        }
    }
}
