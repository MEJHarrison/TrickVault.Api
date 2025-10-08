using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrickVault.Api.Constants;
using TrickVault.Api.Models;

namespace TrickVault.Api.Data.Configurations
{
    public class CatagoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(c => c.Name)
                .IsUnique();

            // --- Category Seed Data ---
            builder.HasData(
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
        }
    }
}
