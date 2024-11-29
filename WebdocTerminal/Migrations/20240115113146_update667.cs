using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update667 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExportTariffId",
                table: "StorageSlabExport",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExportTariff",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ExportTariffId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TariffDate = table.Column<DateTime>(nullable: true),
                    ImplementFrom = table.Column<DateTime>(nullable: true),
                    ImplementTo = table.Column<DateTime>(nullable: true),
                    IsCBMorRate = table.Column<bool>(nullable: true),
                    Rate20 = table.Column<double>(nullable: false),
                    Rate40 = table.Column<double>(nullable: false),
                    Rate45 = table.Column<double>(nullable: false),
                    CBMRate = table.Column<double>(nullable: false),
                    WeightRate = table.Column<double>(nullable: false),
                    PerIndexRate = table.Column<double>(nullable: false),
                    DevededIndexAmount = table.Column<double>(nullable: false),
                    TypeOfTarifff = table.Column<string>(nullable: true),
                    TypeOfImplementation = table.Column<string>(nullable: true),
                    RoundCBMCode = table.Column<bool>(nullable: true),
                    DailyCharges = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsGeneral = table.Column<bool>(nullable: false),
                    IsDollerRate = table.Column<bool>(nullable: false),
                    IsFixedRate = table.Column<bool>(nullable: false),
                    TariffHeadExportId = table.Column<long>(nullable: true),
                    PortAndTerminalExportId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportTariff", x => x.ExportTariffId);
                    table.ForeignKey(
                        name: "FK_ExportTariff_PortAndTerminalExport_PortAndTerminalExportId",
                        column: x => x.PortAndTerminalExportId,
                        principalTable: "PortAndTerminalExport",
                        principalColumn: "PortAndTerminalExportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExportTariff_TariffHeadExport_TariffHeadExportId",
                        column: x => x.TariffHeadExportId,
                        principalTable: "TariffHeadExport",
                        principalColumn: "TariffHeadExportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TariffInformationExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    TariffInformationExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsEnableDisabled = table.Column<bool>(nullable: false),
                    ShippingLineExportId = table.Column<long>(nullable: true),
                    PortAndTerminalExportId = table.Column<long>(nullable: true),
                    ClearingAgentExportId = table.Column<long>(nullable: true),
                    ExportConsigneeId = table.Column<long>(nullable: true),
                    ShippingAgentExportId = table.Column<long>(nullable: true),
                    GoodsHeadExportId = table.Column<long>(nullable: true),
                    IndexCargoType = table.Column<string>(nullable: true),
                    StorageFreeDays = table.Column<long>(nullable: true),
                    DGFreeDays = table.Column<long>(nullable: true),
                    PercentAgeShippingAgentExportId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffInformationExport", x => x.TariffInformationExportId);
                    table.ForeignKey(
                        name: "FK_TariffInformationExport_ClearingAgentExport_ClearingAgentExportId",
                        column: x => x.ClearingAgentExportId,
                        principalTable: "ClearingAgentExport",
                        principalColumn: "ClearingAgentExportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffInformationExport_ExportConsignee_ExportConsigneeId",
                        column: x => x.ExportConsigneeId,
                        principalTable: "ExportConsignee",
                        principalColumn: "ExportConsigneeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffInformationExport_GoodsHeadExport_GoodsHeadExportId",
                        column: x => x.GoodsHeadExportId,
                        principalTable: "GoodsHeadExport",
                        principalColumn: "GoodsHeadExportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffInformationExport_PortAndTerminalExport_PortAndTerminalExportId",
                        column: x => x.PortAndTerminalExportId,
                        principalTable: "PortAndTerminalExport",
                        principalColumn: "PortAndTerminalExportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffInformationExport_ShippingAgentExport_ShippingAgentExportId",
                        column: x => x.ShippingAgentExportId,
                        principalTable: "ShippingAgentExport",
                        principalColumn: "ShippingAgentExportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffInformationExport_ShippingLineExport_ShippingLineExportId",
                        column: x => x.ShippingLineExportId,
                        principalTable: "ShippingLineExport",
                        principalColumn: "ShippingLineExportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TariffInofrmationTariffExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    TariffInofrmationTariffExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TariffInformationExportId = table.Column<long>(nullable: false),
                    ExportTariffId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffInofrmationTariffExport", x => x.TariffInofrmationTariffExportId);
                    table.ForeignKey(
                        name: "FK_TariffInofrmationTariffExport_ExportTariff_ExportTariffId",
                        column: x => x.ExportTariffId,
                        principalTable: "ExportTariff",
                        principalColumn: "ExportTariffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffInofrmationTariffExport_TariffInformationExport_TariffInformationExportId",
                        column: x => x.TariffInformationExportId,
                        principalTable: "TariffInformationExport",
                        principalColumn: "TariffInformationExportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageSlabExport_ExportTariffId",
                table: "StorageSlabExport",
                column: "ExportTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportTariff_PortAndTerminalExportId",
                table: "ExportTariff",
                column: "PortAndTerminalExportId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportTariff_TariffHeadExportId",
                table: "ExportTariff",
                column: "TariffHeadExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffInformationExport_ClearingAgentExportId",
                table: "TariffInformationExport",
                column: "ClearingAgentExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffInformationExport_ExportConsigneeId",
                table: "TariffInformationExport",
                column: "ExportConsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffInformationExport_GoodsHeadExportId",
                table: "TariffInformationExport",
                column: "GoodsHeadExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffInformationExport_PortAndTerminalExportId",
                table: "TariffInformationExport",
                column: "PortAndTerminalExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffInformationExport_ShippingAgentExportId",
                table: "TariffInformationExport",
                column: "ShippingAgentExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffInformationExport_ShippingLineExportId",
                table: "TariffInformationExport",
                column: "ShippingLineExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffInofrmationTariffExport_ExportTariffId",
                table: "TariffInofrmationTariffExport",
                column: "ExportTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffInofrmationTariffExport_TariffInformationExportId",
                table: "TariffInofrmationTariffExport",
                column: "TariffInformationExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageSlabExport_ExportTariff_ExportTariffId",
                table: "StorageSlabExport",
                column: "ExportTariffId",
                principalTable: "ExportTariff",
                principalColumn: "ExportTariffId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageSlabExport_ExportTariff_ExportTariffId",
                table: "StorageSlabExport");

            migrationBuilder.DropTable(
                name: "TariffInofrmationTariffExport");

            migrationBuilder.DropTable(
                name: "ExportTariff");

            migrationBuilder.DropTable(
                name: "TariffInformationExport");

            migrationBuilder.DropIndex(
                name: "IX_StorageSlabExport_ExportTariffId",
                table: "StorageSlabExport");

            migrationBuilder.DropColumn(
                name: "ExportTariffId",
                table: "StorageSlabExport");
        }
    }
}
