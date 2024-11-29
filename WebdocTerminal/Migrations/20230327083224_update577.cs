using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update577 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalExportId",
                table: "VoyageExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShippingLineExportId",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoyageExport_PortAndTerminalExportId",
                table: "VoyageExport",
                column: "PortAndTerminalExportId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingPrograms_ShippingLineExportId",
                table: "LoadingPrograms",
                column: "ShippingLineExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_ShippingLineExport_ShippingLineExportId",
                table: "LoadingPrograms",
                column: "ShippingLineExportId",
                principalTable: "ShippingLineExport",
                principalColumn: "ShippingLineExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoyageExport_PortAndTerminalExport_PortAndTerminalExportId",
                table: "VoyageExport",
                column: "PortAndTerminalExportId",
                principalTable: "PortAndTerminalExport",
                principalColumn: "PortAndTerminalExportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_ShippingLineExport_ShippingLineExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_VoyageExport_PortAndTerminalExport_PortAndTerminalExportId",
                table: "VoyageExport");

            migrationBuilder.DropIndex(
                name: "IX_VoyageExport_PortAndTerminalExportId",
                table: "VoyageExport");

            migrationBuilder.DropIndex(
                name: "IX_LoadingPrograms_ShippingLineExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalExportId",
                table: "VoyageExport");

            migrationBuilder.DropColumn(
                name: "ShippingLineExportId",
                table: "LoadingPrograms");
        }
    }
}
