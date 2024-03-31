using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThemisWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class yeyeya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarningLevel",
                table: "Submittments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarningLevel",
                table: "Submittments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assignments");
        }
    }
}
