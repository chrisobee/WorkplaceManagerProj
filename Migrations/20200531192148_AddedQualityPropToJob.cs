using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkplaceManager.Migrations
{
    public partial class AddedQualityPropToJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quality",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Jobs");
        }
    }
}
