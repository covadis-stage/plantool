using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plantool.Data.Migrations
{
    /// <inheritdoc />
    public partial class RealInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationShortText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectManager = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Network",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Network", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Network_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkBreakdownStructure",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetworkId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkBreakdownStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkBreakdownStructure_Network_NetworkId",
                        column: x => x.NetworkId,
                        principalTable: "Network",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LatestStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LatestFinishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OriginalFinishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TimeEstimated = table.Column<TimeSpan>(type: "time", nullable: true),
                    TimeSpent = table.Column<TimeSpan>(type: "time", nullable: true),
                    TeamLeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityTypeCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    WorkBreakdownStructureId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    GeneralRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActualFinishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AbsoluteWorkload = table.Column<TimeSpan>(type: "time", nullable: true),
                    WorkBreakdownStructureId1 = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityTypes_ActivityTypeCode",
                        column: x => x.ActivityTypeCode,
                        principalTable: "ActivityTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_WorkBreakdownStructure_WorkBreakdownStructureId",
                        column: x => x.WorkBreakdownStructureId,
                        principalTable: "WorkBreakdownStructure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_WorkBreakdownStructure_WorkBreakdownStructureId1",
                        column: x => x.WorkBreakdownStructureId1,
                        principalTable: "WorkBreakdownStructure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityTypeCode",
                table: "Activities",
                column: "ActivityTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_WorkBreakdownStructureId",
                table: "Activities",
                column: "WorkBreakdownStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_WorkBreakdownStructureId1",
                table: "Activities",
                column: "WorkBreakdownStructureId1");

            migrationBuilder.CreateIndex(
                name: "IX_Network_ProjectId",
                table: "Network",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkBreakdownStructure_NetworkId",
                table: "WorkBreakdownStructure",
                column: "NetworkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "WorkBreakdownStructure");

            migrationBuilder.DropTable(
                name: "Network");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
