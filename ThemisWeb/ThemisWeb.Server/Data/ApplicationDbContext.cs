using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Models;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace ReactApp1.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserText> UserTexts { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}