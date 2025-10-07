using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrickVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrickCategory_Categories_CategoriesId",
                table: "TrickCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TrickCategory_Tricks_TricksId",
                table: "TrickCategory");

            migrationBuilder.RenameColumn(
                name: "TricksId",
                table: "TrickCategory",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "TrickCategory",
                newName: "TrickId");

            migrationBuilder.RenameIndex(
                name: "IX_TrickCategory_TricksId",
                table: "TrickCategory",
                newName: "IX_TrickCategory_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tricks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Tricks",
                columns: new[] { "Id", "Comments", "Credits", "Effect", "Method", "Patter", "Setup", "Title" },
                values: new object[,]
                {
                    { 1, "This is great with children.", "Many poor magicians.", "The magician shows an empty hat, then pulls a rabbit out of it", "Magician shows an empty hat, then reaches into secret compartment and removes hidden rabbit", "Look, the hat is completely empty. Except for this here ribbit!", "Put rabbit in hat", "Pull Rabbit from Hat" },
                    { 2, null, null, "Magician has audience member select a random card. The card is shuffled back into the deck. Then the magician finds the selected card.", "Spectator selects a card. The card is returned to the deck and the deck is then shuffled. Then through secret means (not given here), the magician is able to find the selected card.", "Pick a card, any card! Show the audience. Now put it back anywhere in the deck. I'm going to shuffle the cards a few times. Now, I couldn't possibly know the location of your card, right? Yet here it is!", null, "Pick a Card" }
                });

            migrationBuilder.InsertData(
                table: "TrickCategory",
                columns: new[] { "CategoryId", "TrickId" },
                values: new object[,]
                {
                    { 7, 1 },
                    { 8, 1 },
                    { 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tricks_Title",
                table: "Tricks",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrickCategory_CategoryId",
                table: "TrickCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TrickCategory_TrickId",
                table: "TrickCategory");

            migrationBuilder.DropIndex(
                name: "IX_Tricks_Title",
                table: "Tricks");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "TrickCategory",
                keyColumns: new[] { "CategoryId", "TrickId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "TrickCategory",
                keyColumns: new[] { "CategoryId", "TrickId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "TrickCategory",
                keyColumns: new[] { "CategoryId", "TrickId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Tricks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tricks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "TrickCategory",
                newName: "TricksId");

            migrationBuilder.RenameColumn(
                name: "TrickId",
                table: "TrickCategory",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_TrickCategory_CategoryId",
                table: "TrickCategory",
                newName: "IX_TrickCategory_TricksId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tricks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_TrickCategory_Categories_CategoriesId",
                table: "TrickCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrickCategory_Tricks_TricksId",
                table: "TrickCategory",
                column: "TricksId",
                principalTable: "Tricks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
