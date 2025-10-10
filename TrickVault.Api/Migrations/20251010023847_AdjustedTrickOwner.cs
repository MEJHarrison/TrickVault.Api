using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrickVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedTrickOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tricks",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "db386051-e294-4d5a-baa5-295a3254be6a");

            migrationBuilder.UpdateData(
                table: "Tricks",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "db386051-e294-4d5a-baa5-295a3254be6a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tricks",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "a7fef032-3e48-4d96-bbc2-5590b37367e3");

            migrationBuilder.UpdateData(
                table: "Tricks",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "a7fef032-3e48-4d96-bbc2-5590b37367e3");
        }
    }
}
