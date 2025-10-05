using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Models;

namespace TrickVault.Api.Data
{
    public class TrickVaultDbContext : DbContext
    {
        public TrickVaultDbContext(DbContextOptions<TrickVaultDbContext> options) : base(options)
        {

        }

        public DbSet<Trick> Tricks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trick>()
                .HasMany(t => t.Categories)
                .WithMany(c => c.Tricks)
                .UsingEntity(j => j.ToTable("TrickCategory"));

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Card Magic" },
                new Category { Id = 2, Name = "Coin Magic" },
                new Category { Id = 3, Name = "Mentalism" },
                new Category { Id = 4, Name = "Bizarre Magic" },
                new Category { Id = 5, Name = "Illusions" },
                new Category { Id = 6, Name = "Escapology" },
                new Category { Id = 7, Name = "Children's Magic" },
                new Category { Id = 8, Name = "Comedy Magic" },
                new Category { Id = 9, Name = "Utility / Sleights" }
            );
        }
    }
}
