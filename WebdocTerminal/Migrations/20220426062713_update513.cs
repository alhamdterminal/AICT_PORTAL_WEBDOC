using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update513 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TerminalAndFFShareRate_ClearingAgent_ClearingAgentId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalAndFFShareRate_Consigne_ConsigneId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalAndFFShareRate_ISOCodeHead_ISOCodeHeadId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalAndFFShareRate_PortAndTerminal_PortAndTerminalId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalAndFFShareRate_Shipper_ShipperId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropIndex(
                name: "IX_TerminalAndFFShareRate_ClearingAgentId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropIndex(
                name: "IX_TerminalAndFFShareRate_ConsigneId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropIndex(
                name: "IX_TerminalAndFFShareRate_ISOCodeHeadId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropIndex(
                name: "IX_TerminalAndFFShareRate_PortAndTerminalId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropIndex(
                name: "IX_TerminalAndFFShareRate_ShipperId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropColumn(
                name: "ClearingAgentId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropColumn(
                name: "ConsigneId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropColumn(
                name: "ISOCodeHeadId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "TerminalAndFFShareRate");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "TerminalAndFFShareRate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClearingAgentId",
                table: "TerminalAndFFShareRate",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConsigneId",
                table: "TerminalAndFFShareRate",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ISOCodeHeadId",
                table: "TerminalAndFFShareRate",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PortAndTerminalId",
                table: "TerminalAndFFShareRate",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShipperId",
                table: "TerminalAndFFShareRate",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TerminalAndFFShareRate_ClearingAgentId",
                table: "TerminalAndFFShareRate",
                column: "ClearingAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalAndFFShareRate_ConsigneId",
                table: "TerminalAndFFShareRate",
                column: "ConsigneId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalAndFFShareRate_ISOCodeHeadId",
                table: "TerminalAndFFShareRate",
                column: "ISOCodeHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalAndFFShareRate_PortAndTerminalId",
                table: "TerminalAndFFShareRate",
                column: "PortAndTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalAndFFShareRate_ShipperId",
                table: "TerminalAndFFShareRate",
                column: "ShipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalAndFFShareRate_ClearingAgent_ClearingAgentId",
                table: "TerminalAndFFShareRate",
                column: "ClearingAgentId",
                principalTable: "ClearingAgent",
                principalColumn: "ClearingAgentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalAndFFShareRate_Consigne_ConsigneId",
                table: "TerminalAndFFShareRate",
                column: "ConsigneId",
                principalTable: "Consigne",
                principalColumn: "ConsigneId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalAndFFShareRate_ISOCodeHead_ISOCodeHeadId",
                table: "TerminalAndFFShareRate",
                column: "ISOCodeHeadId",
                principalTable: "ISOCodeHead",
                principalColumn: "ISOCodeHeadId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalAndFFShareRate_PortAndTerminal_PortAndTerminalId",
                table: "TerminalAndFFShareRate",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalAndFFShareRate_Shipper_ShipperId",
                table: "TerminalAndFFShareRate",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
