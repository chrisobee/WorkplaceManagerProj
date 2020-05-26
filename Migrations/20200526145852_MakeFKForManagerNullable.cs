using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkplaceManager.Migrations
{
    public partial class MakeFKForManagerNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Branches_BranchId",
                table: "Managers");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Managers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Branches_BranchId",
                table: "Managers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Branches_BranchId",
                table: "Managers");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Managers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Branches_BranchId",
                table: "Managers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
