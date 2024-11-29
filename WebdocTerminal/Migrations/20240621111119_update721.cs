using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update721 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TariffPerBoxRate",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    TariffPerBoxRateId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rate20 = table.Column<double>(nullable: false),
                    Rate40 = table.Column<double>(nullable: false),
                    Rate45 = table.Column<double>(nullable: false),
                    CBMRate = table.Column<double>(nullable: false),
                    WeightRate = table.Column<double>(nullable: false),
                    PerIndex = table.Column<double>(nullable: false),
                    DividedIndex = table.Column<double>(nullable: false),
                    ImplementFrom = table.Column<DateTime>(nullable: true),
                    ImplementTo = table.Column<DateTime>(nullable: true),
                    TypeOfImplementation = table.Column<string>(nullable: true),
                    TariffInformationId = table.Column<long>(nullable: false),
                    PortAndTerminalId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffPerBoxRate", x => x.TariffPerBoxRateId);
                    table.ForeignKey(
                        name: "FK_TariffPerBoxRate_PortAndTerminal_PortAndTerminalId",
                        column: x => x.PortAndTerminalId,
                        principalTable: "PortAndTerminal",
                        principalColumn: "PortAndTerminalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffPerBoxRate_TariffInformation_TariffInformationId",
                        column: x => x.TariffInformationId,
                        principalTable: "TariffInformation",
                        principalColumn: "TariffInformationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TariffPerBoxRate_PortAndTerminalId",
                table: "TariffPerBoxRate",
                column: "PortAndTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffPerBoxRate_TariffInformationId",
                table: "TariffPerBoxRate",
                column: "TariffInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TariffPerBoxRate");
        }
    }
}
