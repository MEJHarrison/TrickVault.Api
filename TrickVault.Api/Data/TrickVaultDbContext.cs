using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TrickVault.Api.Constants;
using TrickVault.Api.Models;

namespace TrickVault.Api.Data
{
    public class TrickVaultDbContext : IdentityDbContext<ApplicationUser>
    {
        public TrickVaultDbContext(DbContextOptions<TrickVaultDbContext> options) : base(options)
        {
        }

        public static bool UseTestData { get; set; } = true;

        public DbSet<Trick> Tricks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TrickCategory> TrickCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
