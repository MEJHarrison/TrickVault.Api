using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrickVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixingTrickUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tricks_AspNetUsers_UserId",
                table: "Tricks");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tricks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tricks_AspNetUsers_UserId",
                table: "Tricks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tricks_AspNetUsers_UserId",
                table: "Tricks");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tricks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Tricks_AspNetUsers_UserId",
                table: "Tricks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
