using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update589 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShipperId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShippingLineId",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "ShippingLineId",
                table: "LoadingProgram");
        }
    }
}
