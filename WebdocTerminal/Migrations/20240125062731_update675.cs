using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update675 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShippingAgentId",
                table: "PartyLedger",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartyLedger_ShippingAgentId",
                table: "PartyLedger",
                column: "ShippingAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyLedger_ShippingAgent_ShippingAgentId",
                table: "PartyLedger",
                column: "ShippingAgentId",
                principalTable: "ShippingAgent",
                principalColumn: "ShippingAgentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyLedger_ShippingAgent_ShippingAgentId",
                table: "PartyLedger");

            migrationBuilder.DropIndex(
                name: "IX_PartyLedger_ShippingAgentId",
                table: "PartyLedger");

            migrationBuilder.DropColumn(
                name: "ShippingAgentId",
                table: "PartyLedger");
        }
    }
}
