using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update590 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.DropForeignKey(
                name: "FK_GateoutExports_PortAndTerminal_PortAndTerminalId",
                table: "GateoutExports");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_VesselExport_ShippingLine_ShippingLineId",
                table: "VesselExport");

            migrationBuilder.DropForeignKey(
                name: "FK_VoyageExport_PortAndTerminal_PortAndTerminalId",
                table: "VoyageExport");

            migrationBuilder.DropIndex(
                name: "IX_VoyageExport_PortAndTerminalId",
                table: "VoyageExport");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_GateoutExports_PortAndTerminalId",
                table: "GateoutExports");

            migrationBuilder.DropIndex(
                name: "IX_ExportContainers_ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "VoyageExport");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "GateoutExports");

            migrationBuilder.DropColumn(
                name: "ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.RenameColumn(
                name: "ShippingLineId",
                table: "VesselExport",
                newName: "ShippingLineExportId");

            migrationBuilder.RenameIndex(
                name: "IX_VesselExport_ShippingLineId",
                table: "VesselExport",
                newName: "IX_VesselExport_ShippingLineExportId");

            migrationBuilder.CreateTable(
                name: "ExportContainerItem",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ExportContainerItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GDNumber = table.Column<string>(nullable: true),
                    ContainerNumber = table.Column<string>(nullable: true),
                    NumberOfPackages = table.Column<int>(nullable: true),
                    AllowLoading = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    Ischecked = table.Column<bool>(nullable: true),
                    IsSubmitted = table.Column<bool>(nullable: false),
                    IsGateOut = table.Column<bool>(nullable: false),
                    IsOvams = table.Column<bool>(nullable: false),
                    GateOutDate = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    SubmitedDate = table.Column<DateTime>(nullable: true),
                    OrderNumber = table.Column<long>(nullable: true),
                    ShipperName = table.Column<string>(nullable: true),
                    isAmountReceived = table.Column<string>(nullable: true),
                    ExportContainerId = table.Column<long>(nullable: true),
                    VesselExportId = table.Column<long>(nullable: true),
                    VoyageExportId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportContainerItem", x => x.ExportContainerItemId);
                    table.ForeignKey(
                        name: "FK_ExportContainerItem_ExportContainers_ExportContainerId",
                        column: x => x.ExportContainerId,
                        principalTable: "ExportContainers",
                        principalColumn: "ExportContainerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExportContainerItem_VesselExport_VesselExportId",
                        column: x => x.VesselExportId,
                        principalTable: "VesselExport",
                        principalColumn: "VesselExportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExportContainerItem_VoyageExport_VoyageExportId",
                        column: x => x.VoyageExportId,
                        principalTable: "VoyageExport",
                        principalColumn: "VoyageExportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainerItem_ExportContainerId",
                table: "ExportContainerItem",
                column: "ExportContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainerItem_VesselExportId",
                table: "ExportContainerItem",
                column: "VesselExportId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainerItem_VoyageExportId",
                table: "ExportContainerItem",
                column: "VoyageExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_VesselExport_ShippingLineExport_ShippingLineExportId",
                table: "VesselExport",
                column: "ShippingLineExportId",
                principalTable: "ShippingLineExport",
                principalColumn: "ShippingLineExportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VesselExport_ShippingLineExport_ShippingLineExportId",
                table: "VesselExport");

            migrationBuilder.DropTable(
                name: "ExportContainerItem");

            migrationBuilder.RenameColumn(
                name: "ShippingLineExportId",
                table: "VesselExport",
                newName: "ShippingLineId");

            migrationBuilder.RenameIndex(
                name: "IX_VesselExport_ShippingLineExportId",
                table: "VesselExport",
                newName: "IX_VesselExport_ShippingLineId");

            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalId",
                table: "VoyageExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShipperId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShippingLineId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalId",
                table: "GateoutExports",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShippingLineId",
                table: "ExportContainers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoyageExport_PortAndTerminalId",
                table: "VoyageExport",
                column: "PortAndTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId");

            migrationBuilder.CreateIndex(
                name: "IX_GateoutExports_PortAndTerminalId",
                table: "GateoutExports",
                column: "PortAndTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainers_ShippingLineId",
                table: "ExportContainers",
                column: "ShippingLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GateoutExports_PortAndTerminal_PortAndTerminalId",
                table: "GateoutExports",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
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
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VesselExport_ShippingLine_ShippingLineId",
                table: "VesselExport",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoyageExport_PortAndTerminal_PortAndTerminalId",
                table: "VoyageExport",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
