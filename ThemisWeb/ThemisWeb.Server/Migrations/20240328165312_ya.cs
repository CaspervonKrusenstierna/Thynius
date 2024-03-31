using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThemisWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class ya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextSessions");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "characterCount",
                table: "UserTexts");

            migrationBuilder.DropColumn(
                name: "wordCount",
                table: "UserTexts");

            migrationBuilder.CreateTable(
                name: "TextSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    S3Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextSessions", x => x.Id);
                });
        }
    }
}
