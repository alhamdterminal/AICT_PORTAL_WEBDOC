using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class udpate673 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OtherChargeAssigningExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    OtherChargeAssigningExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GDNumber = table.Column<string>(nullable: true),
                    ChargeName = table.Column<string>(nullable: true),
                    AmountType = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ChargeQuantity = table.Column<double>(nullable: false),
                    ChargeAmount = table.Column<double>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    InvoiceCreated = table.Column<bool>(nullable: false),
                    AmountAssign = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherChargeAssigningExport", x => x.OtherChargeAssigningExportId);
                });

            migrationBuilder.CreateTable(
                name: "OtherChargeExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    OtherChargeExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChargeName = table.Column<string>(nullable: true),
                    ChargeAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherChargeExport", x => x.OtherChargeExportId);
                });

            migrationBuilder.CreateTable(
                name: "PortChargeExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    PortChargeExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GDNumber = table.Column<string>(nullable: true),
                    DemurrageCharges = table.Column<double>(nullable: false),
                    WeighmentCharges = table.Column<double>(nullable: false),
                    OverWeightPenalty = table.Column<double>(nullable: false),
                    DetentionChargesOrBulletSeal = table.Column<double>(nullable: false),
                    ThcPhcKdlpCharges = table.Column<double>(nullable: false),
                    LoloCharges = table.Column<double>(nullable: false),
                    QictCharges = table.Column<double>(nullable: false),
                    OtherCharges = table.Column<double>(nullable: false),
                    WashAndCleanCharges = table.Column<double>(nullable: false),
                    ANF = table.Column<double>(nullable: false),
                    Pallet = table.Column<double>(nullable: false),
                    Recover = table.Column<double>(nullable: false),
                    TransportCharges = table.Column<double>(nullable: false),
                    TotalCharges = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortChargeExport", x => x.PortChargeExportId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherChargeAssigningExport");

            migrationBuilder.DropTable(
                name: "OtherChargeExport");

            migrationBuilder.DropTable(
                name: "PortChargeExport");
        }
    }
}
