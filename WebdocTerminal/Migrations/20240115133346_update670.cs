using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update670 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TariffPercentageExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    TariffPercentageExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RateOnPersent = table.Column<long>(nullable: false),
                    TariffPercentType = table.Column<string>(nullable: true),
                    TariffHeadExportId = table.Column<long>(nullable: false),
                    ShippingAgentExportId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffPercentageExport", x => x.TariffPercentageExportId);
                    table.ForeignKey(
                        name: "FK_TariffPercentageExport_ShippingAgentExport_ShippingAgentExportId",
                        column: x => x.ShippingAgentExportId,
                        principalTable: "ShippingAgentExport",
                        principalColumn: "ShippingAgentExportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffPercentageExport_TariffHeadExport_TariffHeadExportId",
                        column: x => x.TariffHeadExportId,
                        principalTable: "TariffHeadExport",
                        principalColumn: "TariffHeadExportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TariffPercentageExport_ShippingAgentExportId",
                table: "TariffPercentageExport",
                column: "ShippingAgentExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffPercentageExport_TariffHeadExportId",
                table: "TariffPercentageExport",
                column: "TariffHeadExportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TariffPercentageExport");
        }
    }
}
