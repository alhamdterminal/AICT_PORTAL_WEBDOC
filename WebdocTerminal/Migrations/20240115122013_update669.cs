using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update669 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageSlabExport_TariffExport_TariffExportId",
                table: "StorageSlabExport");

            migrationBuilder.DropIndex(
                name: "IX_StorageSlabExport_TariffExportId",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "TariffExportId",
                table: "StorageSlabExport");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TariffExportId",
                table: "StorageSlabExport",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageSlabExport_TariffExportId",
                table: "StorageSlabExport",
                column: "TariffExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageSlabExport_TariffExport_TariffExportId",
                table: "StorageSlabExport",
                column: "TariffExportId",
                principalTable: "TariffExport",
                principalColumn: "TariffExportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
