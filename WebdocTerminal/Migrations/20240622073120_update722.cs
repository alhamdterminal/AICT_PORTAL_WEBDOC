using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update722 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TariffPerBoxRate_PortAndTerminal_PortAndTerminalId",
                table: "TariffPerBoxRate");

            migrationBuilder.AlterColumn<long>(
                name: "PortAndTerminalId",
                table: "TariffPerBoxRate",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_TariffPerBoxRate_PortAndTerminal_PortAndTerminalId",
                table: "TariffPerBoxRate",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TariffPerBoxRate_PortAndTerminal_PortAndTerminalId",
                table: "TariffPerBoxRate");

            migrationBuilder.AlterColumn<long>(
                name: "PortAndTerminalId",
                table: "TariffPerBoxRate",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffPerBoxRate_PortAndTerminal_PortAndTerminalId",
                table: "TariffPerBoxRate",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
