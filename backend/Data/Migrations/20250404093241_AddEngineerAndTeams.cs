using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plantool.Migrations
{
    /// <inheritdoc />
    public partial class AddEngineerAndTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetenceTeam",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceTeam", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Engineer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanDelegate = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ProductTeamName = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTeam",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompetenceTeamName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ProductLeadId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTeam", x => x.Name);
                    table.ForeignKey(
                        name: "FK_ProductTeam_CompetenceTeam_CompetenceTeamName",
                        column: x => x.CompetenceTeamName,
                        principalTable: "CompetenceTeam",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTeam_Engineer_ProductLeadId",
                        column: x => x.ProductLeadId,
                        principalTable: "Engineer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Engineer_ProductTeamName",
                table: "Engineer",
                column: "ProductTeamName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTeam_CompetenceTeamName",
                table: "ProductTeam",
                column: "CompetenceTeamName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTeam_ProductLeadId",
                table: "ProductTeam",
                column: "ProductLeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Engineer_ProductTeam_ProductTeamName",
                table: "Engineer",
                column: "ProductTeamName",
                principalTable: "ProductTeam",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engineer_ProductTeam_ProductTeamName",
                table: "Engineer");

            migrationBuilder.DropTable(
                name: "ProductTeam");

            migrationBuilder.DropTable(
                name: "CompetenceTeam");

            migrationBuilder.DropTable(
                name: "Engineer");
        }
    }
}
