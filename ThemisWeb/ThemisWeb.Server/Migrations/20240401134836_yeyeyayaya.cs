using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThemisWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class yeyeyayaya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submittments");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "UserTexts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeSubmitted",
                table: "UserTexts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarningLevel",
                table: "UserTexts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTexts_AssignmentId",
                table: "UserTexts",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTexts_Assignments_AssignmentId",
                table: "UserTexts",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTexts_Assignments_AssignmentId",
                table: "UserTexts");

            migrationBuilder.DropIndex(
                name: "IX_UserTexts_AssignmentId",
                table: "UserTexts");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "UserTexts");

            migrationBuilder.DropColumn(
                name: "TimeSubmitted",
                table: "UserTexts");

            migrationBuilder.DropColumn(
                name: "WarningLevel",
                table: "UserTexts");

            migrationBuilder.CreateTable(
                name: "Submittments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    TimeSubmitted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTextId = table.Column<int>(type: "int", nullable: false),
                    WarningLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submittments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submittments_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submittments_AssignmentId",
                table: "Submittments",
                column: "AssignmentId");
        }
    }
}
