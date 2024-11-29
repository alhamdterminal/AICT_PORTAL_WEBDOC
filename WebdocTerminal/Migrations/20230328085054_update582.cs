using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update582 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateoutExports_PortAndTerminal_PortAndTerminalId",
                table: "GateoutExports");

            migrationBuilder.AlterColumn<long>(
                name: "PortAndTerminalId",
                table: "GateoutExports",
                nullable: true,
                oldClrType: typeof(long));

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

            migrationBuilder.AlterColumn<long>(
                name: "PortAndTerminalId",
                table: "GateoutExports",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GateoutExports_PortAndTerminal_PortAndTerminalId",
                table: "GateoutExports",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
