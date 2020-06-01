using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkplaceManager.Migrations
{
    public partial class AddedQualityOfWorkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QualityOfWorks",
                columns: table => new
                {
                    QualityOfWorkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quality = table.Column<double>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: true),
                    BranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityOfWorks", x => x.QualityOfWorkId);
                    table.ForeignKey(
                        name: "FK_QualityOfWorks_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QualityOfWorks_BranchId",
                table: "QualityOfWorks",
                column: "BranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QualityOfWorks");
        }
    }
}
