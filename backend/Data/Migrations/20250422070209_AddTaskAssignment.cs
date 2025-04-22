using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plantool.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DelegatorId",
                table: "Activities",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngineerId",
                table: "Activities",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DelegatorId",
                table: "Activities",
                column: "DelegatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_EngineerId",
                table: "Activities",
                column: "EngineerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Engineers_DelegatorId",
                table: "Activities",
                column: "DelegatorId",
                principalTable: "Engineers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Engineers_EngineerId",
                table: "Activities",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Engineers_DelegatorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Engineers_EngineerId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_DelegatorId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_EngineerId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "DelegatorId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "EngineerId",
                table: "Activities");
        }
    }
}
