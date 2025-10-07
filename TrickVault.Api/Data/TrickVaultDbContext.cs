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

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Trick>()
                .HasIndex(c => c.Title)
                .IsUnique();

            modelBuilder.Entity<Trick>()
                .HasMany(t => t.Categories)
                .WithMany(c => c.Tricks)
                .UsingEntity<Dictionary<string, object>>(
                    "TrickCategory",
                    j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId").HasConstraintName("FK_TrickCategory_CategoryId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Trick>().WithMany().HasForeignKey("TrickId").HasConstraintName("FK_TrickCategory_TrickId").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("TrickId", "CategoryId"); // composite primary key
                        j.ToTable("TrickCategory");
                    }
                );

            // --- Category Seed Data ---
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = CategoryIds.CardMagic, Name = "Card Magic" },
                new Category { Id = CategoryIds.CoinMagic, Name = "Coin Magic" },
                new Category { Id = CategoryIds.Mentalism, Name = "Mentalism" },
                new Category { Id = CategoryIds.BizarreMagic, Name = "Bizarre Magic" },
                new Category { Id = CategoryIds.Illusions, Name = "Illusions" },
                new Category { Id = CategoryIds.Escapology, Name = "Escapology" },
                new Category { Id = CategoryIds.ChildrensMagic, Name = "Children's Magic" },
                new Category { Id = CategoryIds.ComedyMagic, Name = "Comedy Magic" },
                new Category { Id = CategoryIds.UtilitySleights, Name = "Utility / Sleights" }
            );

            // --- Trick Seed Data ---
            modelBuilder.Entity<Trick>().HasData(
                new Trick
                {
                    Id = TrickIds.PullRabbit,
                    Title = "Pull Rabbit from Hat",
                    Effect = "The magician shows an empty hat, then pulls a rabbit out of it",
                    Setup = "Put rabbit in hat",
                    Method = "Magician shows an empty hat, then reaches into secret compartment and removes hidden rabbit",
                    Patter = "Look, the hat is completely empty. Except for this here ribbit!",
                    Comments = "This is great with children.",
                    Credits = "Many poor magicians."
                },
                new Trick
                {
                    Id = TrickIds.PickACard,
                    Title = "Pick a Card",
                    Effect = "Magician has audience member select a random card. The card is shuffled back into the deck. Then the magician finds the selected card.",
                    Method = "Spectator selects a card. The card is returned to the deck and the deck is then shuffled. Then through secret means (not given here), the magician is able to find the selected card.",
                    Patter = "Pick a card, any card! Show the audience. Now put it back anywhere in the deck. I'm going to shuffle the cards a few times. Now, I couldn't possibly know the location of your card, right? Yet here it is!"
                }
            );

            // --- TrickCategory (many-to-many) ---
            modelBuilder.Entity("TrickCategory").HasData(
                new { TrickId = TrickIds.PullRabbit, CategoryId = CategoryIds.ChildrensMagic },
                new { TrickId = TrickIds.PullRabbit, CategoryId = CategoryIds.ComedyMagic },
                new { TrickId = TrickIds.PickACard, CategoryId = CategoryIds.CardMagic }
            );
        }
    }

    // --- ID Constants ---
    public static class CategoryIds
    {
        public const int CardMagic = 1;
        public const int CoinMagic = 2;
        public const int Mentalism = 3;
        public const int BizarreMagic = 4;
        public const int Illusions = 5;
        public const int Escapology = 6;
        public const int ChildrensMagic = 7;
        public const int ComedyMagic = 8;
        public const int UtilitySleights = 9;
    }

    public static class TrickIds
    {
        public const int PullRabbit = 1;
        public const int PickACard = 2;
    }
}
