using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update588 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalId",
                table: "GateoutExports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GateoutExports_PortAndTerminalId",
                table: "GateoutExports",
                column: "PortAndTerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateoutExports_PortAndTerminal_PortAndTerminalId",
                table: "GateoutExports",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateoutExports_PortAndTerminal_PortAndTerminalId",
                table: "GateoutExports");

            migrationBuilder.DropIndex(
                name: "IX_GateoutExports_PortAndTerminalId",
                table: "GateoutExports");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "GateoutExports");
        }
    }
}
