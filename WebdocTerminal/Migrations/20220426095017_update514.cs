using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update514 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AICTPerCBMRateShareRate",
                table: "AICTAndLineIndexTariff",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AICTPerIndexRateShareRate",
                table: "AICTAndLineIndexTariff",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FFPerCBMRateShareRate",
                table: "AICTAndLineIndexTariff",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FFPerIndexRateShareRate",
                table: "AICTAndLineIndexTariff",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AICTPerCBMRateShareRate",
                table: "AICTAndLineIndexTariff");

            migrationBuilder.DropColumn(
                name: "AICTPerIndexRateShareRate",
                table: "AICTAndLineIndexTariff");

            migrationBuilder.DropColumn(
                name: "FFPerCBMRateShareRate",
                table: "AICTAndLineIndexTariff");

            migrationBuilder.DropColumn(
                name: "FFPerIndexRateShareRate",
                table: "AICTAndLineIndexTariff");
        }
    }
}
