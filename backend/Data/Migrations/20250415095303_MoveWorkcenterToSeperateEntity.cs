using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plantool.Migrations
{
    /// <inheritdoc />
    public partial class MoveWorkcenterToSeperateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkCenter",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "WorkCenterKey",
                table: "Activities",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkCenters",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReadableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCenters", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_WorkCenterKey",
                table: "Activities",
                column: "WorkCenterKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_WorkCenters_WorkCenterKey",
                table: "Activities",
                column: "WorkCenterKey",
                principalTable: "WorkCenters",
                principalColumn: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_WorkCenters_WorkCenterKey",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "WorkCenters");

            migrationBuilder.DropIndex(
                name: "IX_Activities_WorkCenterKey",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "WorkCenterKey",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "WorkCenter",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
