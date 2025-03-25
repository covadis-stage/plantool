using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plantool.Data.Migrations
{
    /// <inheritdoc />
    public partial class DenormalizeModelsForPerformance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_WorkBreakdownStructure_WorkBreakdownStructureId",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "WorkBreakdownStructure");

            migrationBuilder.DropTable(
                name: "Network");

            migrationBuilder.RenameColumn(
                name: "WorkBreakdownStructureId",
                table: "Activities",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_WorkBreakdownStructureId",
                table: "Activities",
                newName: "IX_Activities_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "Network",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkBreakdownStructure",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Projects_ProjectId",
                table: "Activities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Projects_ProjectId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Network",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "WorkBreakdownStructure",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Activities",
                newName: "WorkBreakdownStructureId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_ProjectId",
                table: "Activities",
                newName: "IX_Activities_WorkBreakdownStructureId");

            migrationBuilder.CreateTable(
                name: "Network",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Network", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Network_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkBreakdownStructure",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NetworkId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkBreakdownStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkBreakdownStructure_Network_NetworkId",
                        column: x => x.NetworkId,
                        principalTable: "Network",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Network_ProjectId",
                table: "Network",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkBreakdownStructure_NetworkId",
                table: "WorkBreakdownStructure",
                column: "NetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_WorkBreakdownStructure_WorkBreakdownStructureId",
                table: "Activities",
                column: "WorkBreakdownStructureId",
                principalTable: "WorkBreakdownStructure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
