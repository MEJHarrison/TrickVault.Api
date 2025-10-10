using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrickVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedExplicitTrickCategoriesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrickCategory_CategoryId",
                table: "TrickCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TrickCategory_TrickId",
                table: "TrickCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_TrickCategory_Categories_CategoryId",
                table: "TrickCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrickCategory_Tricks_TrickId",
                table: "TrickCategory",
                column: "TrickId",
                principalTable: "Tricks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrickCategory_Categories_CategoryId",
                table: "TrickCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TrickCategory_Tricks_TrickId",
                table: "TrickCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_TrickCategory_CategoryId",
                table: "TrickCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrickCategory_TrickId",
                table: "TrickCategory",
                column: "TrickId",
                principalTable: "Tricks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
