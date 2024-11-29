using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update578 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoReceived_LoadingPrograms_LoadingProgramId",
                table: "CargoReceived");

            migrationBuilder.DropForeignKey(
                name: "FK_EnteringCargo_LoadingPrograms_LoadingProgramId",
                table: "EnteringCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportCargo_LoadingPrograms_LoadingProgramId",
                table: "ExportCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgramDetail_LoadingPrograms_LoadingProgramId",
                table: "LoadingProgramDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_ClearingAgentExport_ClearingAgentExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_NatureOfTariff_NatureOfTariffId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_PortAndTerminal_PortAndTerminalId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_Shipper_ShipperId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_ShippingAgentExport_ShippingAgentExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_ShippingLineExport_ShippingLineExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_ShippingLine_ShippingLineId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_VesselExport_VesselExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_VoyageExport_VoyageExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_TPCargoDetails_LoadingPrograms_LoadingProgramId",
                table: "TPCargoDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoadingPrograms",
                table: "LoadingPrograms");

            migrationBuilder.RenameTable(
                name: "LoadingPrograms",
                newName: "LoadingProgram");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_VoyageExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_VoyageExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_VesselExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_VesselExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ShippingLineId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ShippingLineId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ShippingLineExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ShippingLineExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ShippingAgentExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ShippingAgentExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ShipperId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ShipperId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_PortAndTerminalId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_PortAndTerminalId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_NatureOfTariffId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_NatureOfTariffId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ClearingAgentExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ClearingAgentExportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoadingProgram",
                table: "LoadingProgram",
                column: "LoadingProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoReceived_LoadingProgram_LoadingProgramId",
                table: "CargoReceived",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnteringCargo_LoadingProgram_LoadingProgramId",
                table: "EnteringCargo",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportCargo_LoadingProgram_LoadingProgramId",
                table: "ExportCargo",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ClearingAgentExport_ClearingAgentExportId",
                table: "LoadingProgram",
                column: "ClearingAgentExportId",
                principalTable: "ClearingAgentExport",
                principalColumn: "ClearingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_NatureOfTariff_NatureOfTariffId",
                table: "LoadingProgram",
                column: "NatureOfTariffId",
                principalTable: "NatureOfTariff",
                principalColumn: "NatureOfTariffId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingAgentExport_ShippingAgentExportId",
                table: "LoadingProgram",
                column: "ShippingAgentExportId",
                principalTable: "ShippingAgentExport",
                principalColumn: "ShippingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingLineExport_ShippingLineExportId",
                table: "LoadingProgram",
                column: "ShippingLineExportId",
                principalTable: "ShippingLineExport",
                principalColumn: "ShippingLineExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_VesselExport_VesselExportId",
                table: "LoadingProgram",
                column: "VesselExportId",
                principalTable: "VesselExport",
                principalColumn: "VesselExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_VoyageExport_VoyageExportId",
                table: "LoadingProgram",
                column: "VoyageExportId",
                principalTable: "VoyageExport",
                principalColumn: "VoyageExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgramDetail_LoadingProgram_LoadingProgramId",
                table: "LoadingProgramDetail",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TPCargoDetails_LoadingProgram_LoadingProgramId",
                table: "TPCargoDetails",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoReceived_LoadingProgram_LoadingProgramId",
                table: "CargoReceived");

            migrationBuilder.DropForeignKey(
                name: "FK_EnteringCargo_LoadingProgram_LoadingProgramId",
                table: "EnteringCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportCargo_LoadingProgram_LoadingProgramId",
                table: "ExportCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ClearingAgentExport_ClearingAgentExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_NatureOfTariff_NatureOfTariffId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingAgentExport_ShippingAgentExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingLineExport_ShippingLineExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_VesselExport_VesselExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_VoyageExport_VoyageExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgramDetail_LoadingProgram_LoadingProgramId",
                table: "LoadingProgramDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TPCargoDetails_LoadingProgram_LoadingProgramId",
                table: "TPCargoDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoadingProgram",
                table: "LoadingProgram");

            migrationBuilder.RenameTable(
                name: "LoadingProgram",
                newName: "LoadingPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_VoyageExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_VoyageExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_VesselExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_VesselExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ShippingLineId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ShippingLineId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ShippingLineExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ShippingLineExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ShippingAgentExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ShippingAgentExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ShipperId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ShipperId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_PortAndTerminalId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_PortAndTerminalId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_NatureOfTariffId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_NatureOfTariffId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ClearingAgentExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ClearingAgentExportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoadingPrograms",
                table: "LoadingPrograms",
                column: "LoadingProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoReceived_LoadingPrograms_LoadingProgramId",
                table: "CargoReceived",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnteringCargo_LoadingPrograms_LoadingProgramId",
                table: "EnteringCargo",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportCargo_LoadingPrograms_LoadingProgramId",
                table: "ExportCargo",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgramDetail_LoadingPrograms_LoadingProgramId",
                table: "LoadingProgramDetail",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_ClearingAgentExport_ClearingAgentExportId",
                table: "LoadingPrograms",
                column: "ClearingAgentExportId",
                principalTable: "ClearingAgentExport",
                principalColumn: "ClearingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_NatureOfTariff_NatureOfTariffId",
                table: "LoadingPrograms",
                column: "NatureOfTariffId",
                principalTable: "NatureOfTariff",
                principalColumn: "NatureOfTariffId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_PortAndTerminal_PortAndTerminalId",
                table: "LoadingPrograms",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_Shipper_ShipperId",
                table: "LoadingPrograms",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_ShippingAgentExport_ShippingAgentExportId",
                table: "LoadingPrograms",
                column: "ShippingAgentExportId",
                principalTable: "ShippingAgentExport",
                principalColumn: "ShippingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_ShippingLineExport_ShippingLineExportId",
                table: "LoadingPrograms",
                column: "ShippingLineExportId",
                principalTable: "ShippingLineExport",
                principalColumn: "ShippingLineExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_ShippingLine_ShippingLineId",
                table: "LoadingPrograms",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_VesselExport_VesselExportId",
                table: "LoadingPrograms",
                column: "VesselExportId",
                principalTable: "VesselExport",
                principalColumn: "VesselExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_VoyageExport_VoyageExportId",
                table: "LoadingPrograms",
                column: "VoyageExportId",
                principalTable: "VoyageExport",
                principalColumn: "VoyageExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TPCargoDetails_LoadingPrograms_LoadingProgramId",
                table: "TPCargoDetails",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
