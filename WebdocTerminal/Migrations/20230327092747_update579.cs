using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update579 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingLineCode",
                table: "ShippingLineExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShippingLineExportId",
                table: "ExportContainers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainers_ShippingLineExportId",
                table: "ExportContainers",
                column: "ShippingLineExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainers_ShippingLineExport_ShippingLineExportId",
                table: "ExportContainers",
                column: "ShippingLineExportId",
                principalTable: "ShippingLineExport",
                principalColumn: "ShippingLineExportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainers_ShippingLineExport_ShippingLineExportId",
                table: "ExportContainers");

            migrationBuilder.DropIndex(
                name: "IX_ExportContainers_ShippingLineExportId",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "ShippingLineCode",
                table: "ShippingLineExport");

            migrationBuilder.DropColumn(
                name: "ShippingLineExportId",
                table: "ExportContainers");
        }
    }
}
