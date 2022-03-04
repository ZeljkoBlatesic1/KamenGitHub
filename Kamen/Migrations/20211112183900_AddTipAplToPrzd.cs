using Microsoft.EntityFrameworkCore.Migrations;

namespace Kamen.Migrations
{
    public partial class AddTipAplToPrzd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipAplId",
                table: "Proizvod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_TipAplId",
                table: "Proizvod",
                column: "TipAplId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proizvod_TipApl_TipAplId",
                table: "Proizvod",
                column: "TipAplId",
                principalTable: "TipApl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proizvod_TipApl_TipAplId",
                table: "Proizvod");

            migrationBuilder.DropIndex(
                name: "IX_Proizvod_TipAplId",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "TipAplId",
                table: "Proizvod");
        }
    }
}
