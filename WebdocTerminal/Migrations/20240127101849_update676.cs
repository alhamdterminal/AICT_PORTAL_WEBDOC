using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update676 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShippingAgentId",
                table: "ChequeRecived",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChequeRecived_ShippingAgentId",
                table: "ChequeRecived",
                column: "ShippingAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChequeRecived_ShippingAgent_ShippingAgentId",
                table: "ChequeRecived",
                column: "ShippingAgentId",
                principalTable: "ShippingAgent",
                principalColumn: "ShippingAgentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChequeRecived_ShippingAgent_ShippingAgentId",
                table: "ChequeRecived");

            migrationBuilder.DropIndex(
                name: "IX_ChequeRecived_ShippingAgentId",
                table: "ChequeRecived");

            migrationBuilder.DropColumn(
                name: "ShippingAgentId",
                table: "ChequeRecived");
        }
    }
}
