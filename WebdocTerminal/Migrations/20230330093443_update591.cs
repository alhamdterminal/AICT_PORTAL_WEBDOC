using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update591 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreAlterDetail_PortAndTerminal_PortAndTerminalId",
                table: "PreAlterDetail");

            migrationBuilder.DropIndex(
                name: "IX_PreAlterDetail_PortAndTerminalId",
                table: "PreAlterDetail");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "PreAlterDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalId",
                table: "PreAlterDetail",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PreAlterDetail_PortAndTerminalId",
                table: "PreAlterDetail",
                column: "PortAndTerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreAlterDetail_PortAndTerminal_PortAndTerminalId",
                table: "PreAlterDetail",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
