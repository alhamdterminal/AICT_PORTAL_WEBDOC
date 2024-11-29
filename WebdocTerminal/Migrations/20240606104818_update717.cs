using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update717 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ActualWT",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AictPerBoxRate",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DestuffCBM",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ExaminationCBM",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "FFStorageShare",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "IGMCBM",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MFTWT",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualWT",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "AictPerBoxRate",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "DestuffCBM",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ExaminationCBM",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "FFStorageShare",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "IGMCBM",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "MFTWT",
                table: "Invoice");
        }
    }
}
