using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update658 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RateAmount20",
                table: "PortAndTerminalExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RateAmount40",
                table: "PortAndTerminalExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RateAmount45",
                table: "PortAndTerminalExport",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RateAmount20",
                table: "PortAndTerminalExport");

            migrationBuilder.DropColumn(
                name: "RateAmount40",
                table: "PortAndTerminalExport");

            migrationBuilder.DropColumn(
                name: "RateAmount45",
                table: "PortAndTerminalExport");
        }
    }
}
