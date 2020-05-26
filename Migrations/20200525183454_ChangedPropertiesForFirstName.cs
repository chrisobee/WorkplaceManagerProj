using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkplaceManager.Migrations
{
    public partial class ChangedPropertiesForFirstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Managers_ManagerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Managers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Managers",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Managers_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Managers_ManagerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Managers_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
