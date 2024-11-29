using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update655 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShippingAgentId",
                table: "WorkOrderCSD",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderCSD_ShippingAgentId",
                table: "WorkOrderCSD",
                column: "ShippingAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderCSD_ShippingAgent_ShippingAgentId",
                table: "WorkOrderCSD",
                column: "ShippingAgentId",
                principalTable: "ShippingAgent",
                principalColumn: "ShippingAgentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderCSD_ShippingAgent_ShippingAgentId",
                table: "WorkOrderCSD");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderCSD_ShippingAgentId",
                table: "WorkOrderCSD");

            migrationBuilder.DropColumn(
                name: "ShippingAgentId",
                table: "WorkOrderCSD");
        }
    }
}
