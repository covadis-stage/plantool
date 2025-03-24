using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plantool.Migrations
{
    /// <inheritdoc />
    public partial class FixShadowProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_WorkBreakdownStructure_WorkBreakdownStructureId1",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_WorkBreakdownStructureId1",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "WorkBreakdownStructureId1",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkBreakdownStructureId1",
                table: "Activities",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_WorkBreakdownStructureId1",
                table: "Activities",
                column: "WorkBreakdownStructureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_WorkBreakdownStructure_WorkBreakdownStructureId1",
                table: "Activities",
                column: "WorkBreakdownStructureId1",
                principalTable: "WorkBreakdownStructure",
                principalColumn: "Id");
        }
    }
}
