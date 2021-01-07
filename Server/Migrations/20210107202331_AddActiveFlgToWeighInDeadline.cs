using Microsoft.EntityFrameworkCore.Migrations;

namespace WeighUpBlazor.Server.Migrations
{
    public partial class AddActiveFlgToWeighInDeadline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WeighInDeadlines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WeighInDeadlines");
        }
    }
}
