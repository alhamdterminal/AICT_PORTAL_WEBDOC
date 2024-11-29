using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update685 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PercentForAictBillingToLine",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    PercentForAictBillingToLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PerformedDate = table.Column<DateTime>(nullable: false),
                    UserNmae = table.Column<string>(nullable: true),
                    SystemAddress = table.Column<string>(nullable: true),
                    StoragePercentToLine = table.Column<long>(nullable: false),
                    ServiceChargesPercentToLine = table.Column<long>(nullable: false),
                    ShippingAgentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PercentForAictBillingToLine", x => x.PercentForAictBillingToLineId);
                    table.ForeignKey(
                        name: "FK_PercentForAictBillingToLine_ShippingAgent_ShippingAgentId",
                        column: x => x.ShippingAgentId,
                        principalTable: "ShippingAgent",
                        principalColumn: "ShippingAgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PercentForAictBillingToLine_ShippingAgentId",
                table: "PercentForAictBillingToLine",
                column: "ShippingAgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PercentForAictBillingToLine");
        }
    }
}
