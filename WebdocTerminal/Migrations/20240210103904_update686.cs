using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update686 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AictBilling",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    AictBillingId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AictBillingNumber = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    ShippingAgentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AictBilling", x => x.AictBillingId);
                    table.ForeignKey(
                        name: "FK_AictBilling_ShippingAgent_ShippingAgentId",
                        column: x => x.ShippingAgentId,
                        principalTable: "ShippingAgent",
                        principalColumn: "ShippingAgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AictBillingItem",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    AictBillingItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VirNo = table.Column<string>(nullable: true),
                    IndexNo = table.Column<long>(nullable: false),
                    LineInvoiceno = table.Column<string>(nullable: true),
                    LineInvoiceDate = table.Column<DateTime>(nullable: true),
                    AictInvoiceNo = table.Column<string>(nullable: true),
                    AictInvoiceDate = table.Column<DateTime>(nullable: true),
                    LineStorage = table.Column<long>(nullable: false),
                    LineServiceChages = table.Column<long>(nullable: false),
                    LineTotalCharges = table.Column<long>(nullable: false),
                    StoragePercentToLine = table.Column<long>(nullable: false),
                    ServiceChargesPercentToLine = table.Column<long>(nullable: false),
                    AICTBillingToLine = table.Column<long>(nullable: false),
                    AictBillingId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AictBillingItem", x => x.AictBillingItemId);
                    table.ForeignKey(
                        name: "FK_AictBillingItem_AictBilling_AictBillingId",
                        column: x => x.AictBillingId,
                        principalTable: "AictBilling",
                        principalColumn: "AictBillingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AictBilling_ShippingAgentId",
                table: "AictBilling",
                column: "ShippingAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AictBillingItem_AictBillingId",
                table: "AictBillingItem",
                column: "AictBillingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AictBillingItem");

            migrationBuilder.DropTable(
                name: "AictBilling");
        }
    }
}
