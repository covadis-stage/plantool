using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plantool.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engineer_ProductTeam_ProductTeamName",
                table: "Engineer");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTeam_CompetenceTeam_CompetenceTeamName",
                table: "ProductTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTeam_Engineer_ProductLeadId",
                table: "ProductTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTeam",
                table: "ProductTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engineer",
                table: "Engineer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetenceTeam",
                table: "CompetenceTeam");

            migrationBuilder.RenameTable(
                name: "ProductTeam",
                newName: "ProductTeams");

            migrationBuilder.RenameTable(
                name: "Engineer",
                newName: "Engineers");

            migrationBuilder.RenameTable(
                name: "CompetenceTeam",
                newName: "CompetenceTeams");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTeam_ProductLeadId",
                table: "ProductTeams",
                newName: "IX_ProductTeams_ProductLeadId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTeam_CompetenceTeamName",
                table: "ProductTeams",
                newName: "IX_ProductTeams_CompetenceTeamName");

            migrationBuilder.RenameIndex(
                name: "IX_Engineer_ProductTeamName",
                table: "Engineers",
                newName: "IX_Engineers_ProductTeamName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductLeadId",
                table: "ProductTeams",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTeams",
                table: "ProductTeams",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engineers",
                table: "Engineers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetenceTeams",
                table: "CompetenceTeams",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Engineers_ProductTeams_ProductTeamName",
                table: "Engineers",
                column: "ProductTeamName",
                principalTable: "ProductTeams",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTeams_CompetenceTeams_CompetenceTeamName",
                table: "ProductTeams",
                column: "CompetenceTeamName",
                principalTable: "CompetenceTeams",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTeams_Engineers_ProductLeadId",
                table: "ProductTeams",
                column: "ProductLeadId",
                principalTable: "Engineers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engineers_ProductTeams_ProductTeamName",
                table: "Engineers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTeams_CompetenceTeams_CompetenceTeamName",
                table: "ProductTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTeams_Engineers_ProductLeadId",
                table: "ProductTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTeams",
                table: "ProductTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engineers",
                table: "Engineers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetenceTeams",
                table: "CompetenceTeams");

            migrationBuilder.RenameTable(
                name: "ProductTeams",
                newName: "ProductTeam");

            migrationBuilder.RenameTable(
                name: "Engineers",
                newName: "Engineer");

            migrationBuilder.RenameTable(
                name: "CompetenceTeams",
                newName: "CompetenceTeam");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTeams_ProductLeadId",
                table: "ProductTeam",
                newName: "IX_ProductTeam_ProductLeadId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTeams_CompetenceTeamName",
                table: "ProductTeam",
                newName: "IX_ProductTeam_CompetenceTeamName");

            migrationBuilder.RenameIndex(
                name: "IX_Engineers_ProductTeamName",
                table: "Engineer",
                newName: "IX_Engineer_ProductTeamName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductLeadId",
                table: "ProductTeam",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTeam",
                table: "ProductTeam",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engineer",
                table: "Engineer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetenceTeam",
                table: "CompetenceTeam",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Engineer_ProductTeam_ProductTeamName",
                table: "Engineer",
                column: "ProductTeamName",
                principalTable: "ProductTeam",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTeam_CompetenceTeam_CompetenceTeamName",
                table: "ProductTeam",
                column: "CompetenceTeamName",
                principalTable: "CompetenceTeam",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTeam_Engineer_ProductLeadId",
                table: "ProductTeam",
                column: "ProductLeadId",
                principalTable: "Engineer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
