using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update533 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleOfCharge",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ScheduleOfChargeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineName = table.Column<string>(nullable: true),
                    VesselName = table.Column<string>(nullable: true),
                    Consignee = table.Column<string>(nullable: true),
                    NTNNo = table.Column<string>(nullable: true),
                    ClearingAgent = table.Column<string>(nullable: true),
                    ContainerNo = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    ManifestedM3 = table.Column<double>(nullable: false),
                    ManifestedWt = table.Column<double>(nullable: true),
                    CYStorageSizeAmount = table.Column<double>(nullable: false),
                    VoyageNo = table.Column<string>(nullable: true),
                    STNNo = table.Column<string>(nullable: true),
                    CYCFS = table.Column<string>(nullable: true),
                    ActualM3 = table.Column<double>(nullable: false),
                    ActualWt = table.Column<double>(nullable: true),
                    BLM3 = table.Column<double>(nullable: false),
                    Indexno = table.Column<string>(nullable: true),
                    IGMNo = table.Column<string>(nullable: true),
                    BLNo = table.Column<string>(nullable: true),
                    BillEnteryNo = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    ArriveDate = table.Column<DateTime>(nullable: true),
                    IGMDate = table.Column<DateTime>(nullable: true),
                    WaiverAmount = table.Column<double>(nullable: false),
                    StorageDays = table.Column<int>(nullable: false),
                    StorageAmount = table.Column<double>(nullable: false),
                    OtherChargeAmount = table.Column<double>(nullable: false),
                    Igmodesc = table.Column<string>(nullable: true),
                    TariffName = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    AfterWaiverAmount = table.Column<double>(nullable: false),
                    TotalCharges = table.Column<double>(nullable: false),
                    SalesTax = table.Column<double>(nullable: false),
                    SalesTaxAmount = table.Column<double>(nullable: false),
                    NetCharges = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleOfCharge", x => x.ScheduleOfChargeId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleOfChargeItem",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ScheduleOfChargeItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    ScheduleOfChargeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleOfChargeItem", x => x.ScheduleOfChargeItemId);
                    table.ForeignKey(
                        name: "FK_ScheduleOfChargeItem_ScheduleOfCharge_ScheduleOfChargeId",
                        column: x => x.ScheduleOfChargeId,
                        principalTable: "ScheduleOfCharge",
                        principalColumn: "ScheduleOfChargeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleOfChargeItem_ScheduleOfChargeId",
                table: "ScheduleOfChargeItem",
                column: "ScheduleOfChargeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleOfChargeItem");

            migrationBuilder.DropTable(
                name: "ScheduleOfCharge");
        }
    }
}
