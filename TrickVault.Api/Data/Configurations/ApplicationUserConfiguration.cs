using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrickVault.Api.Models;

namespace TrickVault.Api.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            if (!TrickVaultDbContextBase.UseTestData)
            {
                return;
            }

            builder.HasData(
                new ApplicationUser
                {
                    Id = "db386051-e294-4d5a-baa5-295a3254be6a",
                    UserName = "user@localhost.com",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    FirstName = "Mark",
                    LastName = "Harrison",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    SecurityStamp = "STATIC_STAMP_1",
                    ConcurrencyStamp = "STATIC_CONCURRENCY_1",
                    PasswordHash = "AQAAAAIAAYagAAAAEPyniOKSJj/jUut30zEpVFnW+I1+psLTHk70aJQFi7znb6ozfZIKDZn/c+pEnqAAZQ=="
                },
                new ApplicationUser
                {
                    Id = "a7fef032-3e48-4d96-bbc2-5590b37367e3",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "Mark",
                    LastName = "Harrison",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    SecurityStamp = "STATIC_STAMP_2",
                    ConcurrencyStamp = "STATIC_CONCURRENCY_2",
                    PasswordHash = "AQAAAAIAAYagAAAAEF/vTUisrB1TuNdT7rpi1TtN0ZKkArMMDJ3dX/d1k5z515m/pstgR9SwYl/tqvt0sw=="
                }
            );
        }
    }
}
