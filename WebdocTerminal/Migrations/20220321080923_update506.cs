using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update506 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TellySheet_ShippingAgent_ShippingAgentId",
                table: "TellySheet");

            migrationBuilder.AlterColumn<long>(
                name: "ShippingAgentId",
                table: "TellySheet",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_TellySheet_ShippingAgent_ShippingAgentId",
                table: "TellySheet",
                column: "ShippingAgentId",
                principalTable: "ShippingAgent",
                principalColumn: "ShippingAgentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TellySheet_ShippingAgent_ShippingAgentId",
                table: "TellySheet");

            migrationBuilder.AlterColumn<long>(
                name: "ShippingAgentId",
                table: "TellySheet",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TellySheet_ShippingAgent_ShippingAgentId",
                table: "TellySheet",
                column: "ShippingAgentId",
                principalTable: "ShippingAgent",
                principalColumn: "ShippingAgentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
