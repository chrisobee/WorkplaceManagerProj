using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkplaceManager.Migrations
{
    public partial class ChangedNamePropertyForSeniorManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SeniorManagers");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "SeniorManagers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "SeniorManagers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1044e8d0-e0d0-4ec9-822e-e9c12f94f8bd",
                column: "ConcurrencyStamp",
                value: "b4d5fe13-245c-4d40-a76e-bbf1744b1599");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "SeniorManagers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "SeniorManagers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SeniorManagers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
