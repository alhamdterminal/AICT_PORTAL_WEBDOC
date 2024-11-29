using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update668 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageSlabExport_ExportTariff_ExportTariffId",
                table: "StorageSlabExport");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageSlabExport_TariffExport_TariffExportId",
                table: "StorageSlabExport");

            migrationBuilder.AlterColumn<long>(
                name: "TariffExportId",
                table: "StorageSlabExport",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ExportTariffId",
                table: "StorageSlabExport",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AICTRate",
                table: "StorageSlabExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AICTRate20",
                table: "StorageSlabExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AICTRate40",
                table: "StorageSlabExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AICTRate45",
                table: "StorageSlabExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FFRate",
                table: "StorageSlabExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FFRate20",
                table: "StorageSlabExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FFRate40",
                table: "StorageSlabExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FFRate45",
                table: "StorageSlabExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageSlabExport_ExportTariff_ExportTariffId",
                table: "StorageSlabExport",
                column: "ExportTariffId",
                principalTable: "ExportTariff",
                principalColumn: "ExportTariffId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageSlabExport_TariffExport_TariffExportId",
                table: "StorageSlabExport",
                column: "TariffExportId",
                principalTable: "TariffExport",
                principalColumn: "TariffExportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageSlabExport_ExportTariff_ExportTariffId",
                table: "StorageSlabExport");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageSlabExport_TariffExport_TariffExportId",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "AICTRate",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "AICTRate20",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "AICTRate40",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "AICTRate45",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "FFRate",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "FFRate20",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "FFRate40",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "FFRate45",
                table: "StorageSlabExport");

            migrationBuilder.AlterColumn<long>(
                name: "TariffExportId",
                table: "StorageSlabExport",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ExportTariffId",
                table: "StorageSlabExport",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_StorageSlabExport_ExportTariff_ExportTariffId",
                table: "StorageSlabExport",
                column: "ExportTariffId",
                principalTable: "ExportTariff",
                principalColumn: "ExportTariffId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageSlabExport_TariffExport_TariffExportId",
                table: "StorageSlabExport",
                column: "TariffExportId",
                principalTable: "TariffExport",
                principalColumn: "TariffExportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
