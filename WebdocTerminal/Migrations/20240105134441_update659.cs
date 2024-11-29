using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update659 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExportConsigneeId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalExportId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_ExportConsigneeId",
                table: "LoadingProgram",
                column: "ExportConsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_PortAndTerminalExportId",
                table: "LoadingProgram",
                column: "PortAndTerminalExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ExportConsignee_ExportConsigneeId",
                table: "LoadingProgram",
                column: "ExportConsigneeId",
                principalTable: "ExportConsignee",
                principalColumn: "ExportConsigneeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_PortAndTerminalExport_PortAndTerminalExportId",
                table: "LoadingProgram",
                column: "PortAndTerminalExportId",
                principalTable: "PortAndTerminalExport",
                principalColumn: "PortAndTerminalExportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ExportConsignee_ExportConsigneeId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_PortAndTerminalExport_PortAndTerminalExportId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_ExportConsigneeId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_PortAndTerminalExportId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "ExportConsigneeId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalExportId",
                table: "LoadingProgram");
        }
    }
}
