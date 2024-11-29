using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update720 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorageShareRate",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    StorageShareRateId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShareRate = table.Column<long>(nullable: false),
                    TariffId = table.Column<long>(nullable: false),
                    TariffInformationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageShareRate", x => x.StorageShareRateId);
                    table.ForeignKey(
                        name: "FK_StorageShareRate_Tariff_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariff",
                        principalColumn: "TariffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageShareRate_TariffInformation_TariffInformationId",
                        column: x => x.TariffInformationId,
                        principalTable: "TariffInformation",
                        principalColumn: "TariffInformationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageShareRate_TariffId",
                table: "StorageShareRate",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageShareRate_TariffInformationId",
                table: "StorageShareRate",
                column: "TariffInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageShareRate");
        }
    }
}
