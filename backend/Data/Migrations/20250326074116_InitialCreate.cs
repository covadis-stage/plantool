using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plantool.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationShortText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatestStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LatestFinishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OriginalFinishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TimeEstimated = table.Column<long>(type: "bigint", nullable: true),
                    TimeSpent = table.Column<long>(type: "bigint", nullable: true),
                    TeamLeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityTypeCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WorkBreakdownStructure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Network = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    GeneralRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActualFinishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AbsoluteWorkload = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityTypes_ActivityTypeCode",
                        column: x => x.ActivityTypeCode,
                        principalTable: "ActivityTypes",
                        principalColumn: "Key");
                    table.ForeignKey(
                        name: "FK_Activities_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityTypeCode",
                table: "Activities",
                column: "ActivityTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ProjectId",
                table: "Activities",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
