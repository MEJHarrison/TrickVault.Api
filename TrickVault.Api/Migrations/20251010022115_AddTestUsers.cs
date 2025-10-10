using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrickVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTestUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a7fef032-3e48-4d96-bbc2-5590b37367e3", 0, "STATIC_CONCURRENCY_2", "admin@localhost.com", true, "Mark", "Harrison", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEF/vTUisrB1TuNdT7rpi1TtN0ZKkArMMDJ3dX/d1k5z515m/pstgR9SwYl/tqvt0sw==", null, false, "STATIC_STAMP_2", false, "admin@localhost.com" },
                    { "db386051-e294-4d5a-baa5-295a3254be6a", 0, "STATIC_CONCURRENCY_1", "user@localhost.com", true, "Mark", "Harrison", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEPyniOKSJj/jUut30zEpVFnW+I1+psLTHk70aJQFi7znb6ozfZIKDZn/c+pEnqAAZQ==", null, false, "STATIC_STAMP_1", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6751d2bd-b836-430c-8e9a-166853e05c7f", "a7fef032-3e48-4d96-bbc2-5590b37367e3" },
                    { "adef6595-4396-41ad-b366-57082464818b", "db386051-e294-4d5a-baa5-295a3254be6a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6751d2bd-b836-430c-8e9a-166853e05c7f", "a7fef032-3e48-4d96-bbc2-5590b37367e3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "adef6595-4396-41ad-b366-57082464818b", "db386051-e294-4d5a-baa5-295a3254be6a" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a7fef032-3e48-4d96-bbc2-5590b37367e3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "db386051-e294-4d5a-baa5-295a3254be6a");
        }
    }
}
