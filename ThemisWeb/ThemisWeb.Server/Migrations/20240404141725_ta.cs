using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThemisWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class ta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "characterCount",
                table: "UserTexts");

            migrationBuilder.DropColumn(
                name: "wordCount",
                table: "UserTexts");

            migrationBuilder.AddColumn<decimal>(
                name: "guid",
                table: "UserTexts",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "guid",
                table: "UserTexts");

            migrationBuilder.AddColumn<int>(
                name: "characterCount",
                table: "UserTexts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "wordCount",
                table: "UserTexts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
