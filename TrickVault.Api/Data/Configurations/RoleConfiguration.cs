using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrickVault.Api.Constants;

namespace TrickVault.Api.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "6751d2bd-b836-430c-8e9a-166853e05c7f",
                    Name = RoleNames.Administrator,
                    NormalizedName = RoleNames.Administrator.ToUpper()
                },
                new IdentityRole
                {
                    Id = "adef6595-4396-41ad-b366-57082464818b",
                    Name = RoleNames.User,
                    NormalizedName = RoleNames.User.ToUpper()
                }
            );
        }
    }
}
