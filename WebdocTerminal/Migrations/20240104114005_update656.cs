using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update656 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAgentExport_NatureOfTariff_NatureOfTariffId",
                table: "ShippingAgentExport");

            migrationBuilder.DropForeignKey(
                name: "FK_VesselExport_ShippingLineExport_ShippingLineExportId",
                table: "VesselExport");

            migrationBuilder.DropIndex(
                name: "IX_VesselExport_ShippingLineExportId",
                table: "VesselExport");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAgentExport_NatureOfTariffId",
                table: "ShippingAgentExport");

            migrationBuilder.DropIndex(
                name: "IX_Cargo_LoadingProgramDetailId",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "ShippingLineExportId",
                table: "VesselExport");

            migrationBuilder.DropColumn(
                name: "NatureOfTariffId",
                table: "ShippingAgentExport");

            migrationBuilder.AddColumn<string>(
                name: "CargoType",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GoodsHeadExportId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoodsHeadExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    GoodsHeadExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsHeadName = table.Column<string>(nullable: true),
                    StorageFreeDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsHeadExport", x => x.GoodsHeadExportId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_GoodsHeadExportId",
                table: "LoadingProgram",
                column: "GoodsHeadExportId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_LoadingProgramDetailId",
                table: "Cargo",
                column: "LoadingProgramDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_GoodsHeadExport_GoodsHeadExportId",
                table: "LoadingProgram",
                column: "GoodsHeadExportId",
                principalTable: "GoodsHeadExport",
                principalColumn: "GoodsHeadExportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_GoodsHeadExport_GoodsHeadExportId",
                table: "LoadingProgram");

            migrationBuilder.DropTable(
                name: "GoodsHeadExport");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_GoodsHeadExportId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_Cargo_LoadingProgramDetailId",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "CargoType",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "GoodsHeadExportId",
                table: "LoadingProgram");

            migrationBuilder.AddColumn<long>(
                name: "ShippingLineExportId",
                table: "VesselExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NatureOfTariffId",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VesselExport_ShippingLineExportId",
                table: "VesselExport",
                column: "ShippingLineExportId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgentExport_NatureOfTariffId",
                table: "ShippingAgentExport",
                column: "NatureOfTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_LoadingProgramDetailId",
                table: "Cargo",
                column: "LoadingProgramDetailId",
                unique: true,
                filter: "[LoadingProgramDetailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAgentExport_NatureOfTariff_NatureOfTariffId",
                table: "ShippingAgentExport",
                column: "NatureOfTariffId",
                principalTable: "NatureOfTariff",
                principalColumn: "NatureOfTariffId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VesselExport_ShippingLineExport_ShippingLineExportId",
                table: "VesselExport",
                column: "ShippingLineExportId",
                principalTable: "ShippingLineExport",
                principalColumn: "ShippingLineExportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
