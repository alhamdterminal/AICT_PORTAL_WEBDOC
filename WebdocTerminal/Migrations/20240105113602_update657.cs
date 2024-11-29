using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update657 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnteringCargo_ClearingAgentExport_ClearingAgentExportId",
                table: "EnteringCargo");

            migrationBuilder.DropIndex(
                name: "IX_EnteringCargo_ClearingAgentExportId",
                table: "EnteringCargo");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "PortAndTerminalExport");

            migrationBuilder.DropColumn(
                name: "PortOfDischarge",
                table: "PortAndTerminalExport");

            migrationBuilder.DropColumn(
                name: "TerminalCode",
                table: "PortAndTerminalExport");

            migrationBuilder.DropColumn(
                name: "TerminalName",
                table: "PortAndTerminalExport");

            migrationBuilder.DropColumn(
                name: "ClearingAgentExportId",
                table: "EnteringCargo");

            migrationBuilder.CreateTable(
                name: "ExportConsignee",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ExportConsigneeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsigneName = table.Column<string>(nullable: true),
                    ConsigneNTN = table.Column<string>(nullable: true),
                    STRegistrationNo = table.Column<string>(nullable: true),
                    ConsigneCode = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    BillDateTypeLCL = table.Column<string>(nullable: true),
                    BillDateTypeFCL = table.Column<string>(nullable: true),
                    ConsigneePhoneNumber = table.Column<string>(nullable: true),
                    ConsigneeAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportConsignee", x => x.ExportConsigneeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportConsignee");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "PortAndTerminalExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortOfDischarge",
                table: "PortAndTerminalExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TerminalCode",
                table: "PortAndTerminalExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TerminalName",
                table: "PortAndTerminalExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClearingAgentExportId",
                table: "EnteringCargo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnteringCargo_ClearingAgentExportId",
                table: "EnteringCargo",
                column: "ClearingAgentExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnteringCargo_ClearingAgentExport_ClearingAgentExportId",
                table: "EnteringCargo",
                column: "ClearingAgentExportId",
                principalTable: "ClearingAgentExport",
                principalColumn: "ClearingAgentExportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
