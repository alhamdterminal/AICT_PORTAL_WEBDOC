using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update554 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StorageFreeDays",
                table: "TariffInformation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageFreeDays",
                table: "TariffInformation");
        }
    }
}
