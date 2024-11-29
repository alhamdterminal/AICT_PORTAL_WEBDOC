using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update556 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DGFreeDays",
                table: "TariffInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GDCargo",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GDCargo",
                table: "ContainerIndex",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DGFreeDays",
                table: "TariffInformation");

            migrationBuilder.DropColumn(
                name: "GDCargo",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "GDCargo",
                table: "ContainerIndex");
        }
    }
}
