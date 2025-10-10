using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TrickVault.Api.Constants;
using TrickVault.Api.Models;

namespace TrickVault.Api.Data.Configurations
{
    public class TrickCategoryConfiguration : IEntityTypeConfiguration<TrickCategory>
    {
        public void Configure(EntityTypeBuilder<TrickCategory> builder)
        {
            builder.ToTable("TrickCategory");

            builder.HasKey(tc => new { tc.TrickId, tc.CategoryId });

            builder.HasOne(tc => tc.Trick)
                .WithMany(t => t.TrickCategories)
                .HasForeignKey(tc => tc.TrickId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tc => tc.Category)
                .WithMany(c => c.TrickCategories)
                .HasForeignKey(tc => tc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            if (TrickVaultDbContext.UseTestData)
            {
                builder.HasData(
                    new { TrickId = TrickIds.PullRabbit, CategoryId = CategoryIds.ChildrensMagic },
                    new { TrickId = TrickIds.PullRabbit, CategoryId = CategoryIds.ComedyMagic },
                    new { TrickId = TrickIds.PickACard, CategoryId = CategoryIds.CardMagic }
                );
            }

        }
    }
}
