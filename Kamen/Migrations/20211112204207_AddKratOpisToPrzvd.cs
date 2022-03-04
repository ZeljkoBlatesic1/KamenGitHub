using Microsoft.EntityFrameworkCore.Migrations;

namespace Kamen.Migrations
{
    public partial class AddKratOpisToPrzvd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KratOpis",
                table: "Proizvod",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KratOpis",
                table: "Proizvod");
        }
    }
}
