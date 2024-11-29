using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update671 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TerminalAndFFShareRateExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    TerminalAndFFShareRateExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AICTRate20 = table.Column<double>(nullable: false),
                    AICTRate40 = table.Column<double>(nullable: false),
                    AICTRate45 = table.Column<double>(nullable: false),
                    AICTCBMRate = table.Column<double>(nullable: false),
                    AICTPerIndexRate = table.Column<double>(nullable: false),
                    FFRate20 = table.Column<double>(nullable: false),
                    FFRate40 = table.Column<double>(nullable: false),
                    FFRate45 = table.Column<double>(nullable: false),
                    FFCBMRate = table.Column<double>(nullable: false),
                    FFPerIndexRate = table.Column<double>(nullable: false),
                    TotalAICTRate20 = table.Column<double>(nullable: false),
                    TotalAICTRate40 = table.Column<double>(nullable: false),
                    TotalAICTRate45 = table.Column<double>(nullable: false),
                    TotalAICTCBMRate = table.Column<double>(nullable: false),
                    TotalAICTPerIndexRate = table.Column<double>(nullable: false),
                    ShippingAgentExportId = table.Column<long>(nullable: true),
                    GoodsHeadExportId = table.Column<long>(nullable: true),
                    IndexCargoType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalAndFFShareRateExport", x => x.TerminalAndFFShareRateExportId);
                    table.ForeignKey(
                        name: "FK_TerminalAndFFShareRateExport_GoodsHeadExport_GoodsHeadExportId",
                        column: x => x.GoodsHeadExportId,
                        principalTable: "GoodsHeadExport",
                        principalColumn: "GoodsHeadExportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TerminalAndFFShareRateExport_ShippingAgentExport_ShippingAgentExportId",
                        column: x => x.ShippingAgentExportId,
                        principalTable: "ShippingAgentExport",
                        principalColumn: "ShippingAgentExportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TerminalAndFFShareRateExport_GoodsHeadExportId",
                table: "TerminalAndFFShareRateExport",
                column: "GoodsHeadExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalAndFFShareRateExport_ShippingAgentExportId",
                table: "TerminalAndFFShareRateExport",
                column: "ShippingAgentExportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TerminalAndFFShareRateExport");
        }
    }
}
