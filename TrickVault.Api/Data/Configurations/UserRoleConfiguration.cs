using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrickVault.Api.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            if (!TrickVaultDbContext.UseTestData)
            {
                return;
            }

            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "db386051-e294-4d5a-baa5-295a3254be6a",
                    RoleId = "adef6595-4396-41ad-b366-57082464818b"
                },
                new IdentityUserRole<string>
                {
                    UserId = "a7fef032-3e48-4d96-bbc2-5590b37367e3",
                    RoleId = "6751d2bd-b836-430c-8e9a-166853e05c7f"
                }
            );
        }
    }
}
