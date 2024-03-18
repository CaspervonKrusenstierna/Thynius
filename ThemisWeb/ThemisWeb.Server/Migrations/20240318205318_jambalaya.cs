using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThemisWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class jambalaya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Submittments",
                newName: "AssignmentId");

            migrationBuilder.AddColumn<string>(
                name: "TimeSubmitted",
                table: "Submittments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Submittments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "_RawData",
                table: "Submittments",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "DateCreated",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submittments_AssignmentId",
                table: "Submittments",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_GroupId",
                table: "Assignments",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submittments_Assignments_AssignmentId",
                table: "Submittments",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submittments_Assignments_AssignmentId",
                table: "Submittments");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Submittments_AssignmentId",
                table: "Submittments");

            migrationBuilder.DropColumn(
                name: "TimeSubmitted",
                table: "Submittments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Submittments");

            migrationBuilder.DropColumn(
                name: "_RawData",
                table: "Submittments");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "Submittments",
                newName: "GroupId");
        }
    }
}
