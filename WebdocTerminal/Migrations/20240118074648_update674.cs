using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update674 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemExport_TariffExport_TariffExportId",
                table: "InvoiceItemExport");

            migrationBuilder.RenameColumn(
                name: "TariffExportId",
                table: "InvoiceItemExport",
                newName: "ExportTariffId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItemExport_TariffExportId",
                table: "InvoiceItemExport",
                newName: "IX_InvoiceItemExport_ExportTariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemExport_ExportTariff_ExportTariffId",
                table: "InvoiceItemExport",
                column: "ExportTariffId",
                principalTable: "ExportTariff",
                principalColumn: "ExportTariffId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemExport_ExportTariff_ExportTariffId",
                table: "InvoiceItemExport");

            migrationBuilder.RenameColumn(
                name: "ExportTariffId",
                table: "InvoiceItemExport",
                newName: "TariffExportId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItemExport_ExportTariffId",
                table: "InvoiceItemExport",
                newName: "IX_InvoiceItemExport_TariffExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemExport_TariffExport_TariffExportId",
                table: "InvoiceItemExport",
                column: "TariffExportId",
                principalTable: "TariffExport",
                principalColumn: "TariffExportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
