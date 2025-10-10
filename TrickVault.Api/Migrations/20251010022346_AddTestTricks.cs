using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrickVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTestTricks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tricks",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Tricks_UserId",
                table: "Tricks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tricks_AspNetUsers_UserId",
                table: "Tricks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tricks_AspNetUsers_UserId",
                table: "Tricks");

            migrationBuilder.DropIndex(
                name: "IX_Tricks_UserId",
                table: "Tricks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tricks");
        }
    }
}
