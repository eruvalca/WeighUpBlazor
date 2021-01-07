using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeighUpBlazor.Server.Migrations
{
    public partial class WeighInDeadlineModelAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeighInDeadlines",
                columns: table => new
                {
                    WeighInDeadlineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeighInDeadlines", x => x.WeighInDeadlineId);
                    table.ForeignKey(
                        name: "FK_WeighInDeadlines_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeighInDeadlines_CompetitionId",
                table: "WeighInDeadlines",
                column: "CompetitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeighInDeadlines");
        }
    }
}
