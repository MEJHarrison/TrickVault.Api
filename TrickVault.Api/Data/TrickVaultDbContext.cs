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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (UseTestData)
            {
                modelBuilder.Entity("TrickCategory").HasData(
                    new { TrickId = TrickIds.PullRabbit, CategoryId = CategoryIds.ChildrensMagic },
                    new { TrickId = TrickIds.PullRabbit, CategoryId = CategoryIds.ComedyMagic },
                    new { TrickId = TrickIds.PickACard, CategoryId = CategoryIds.CardMagic }
                );
            }
        }
    }
}
