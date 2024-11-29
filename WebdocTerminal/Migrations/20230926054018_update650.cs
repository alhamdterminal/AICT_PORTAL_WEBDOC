using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update650 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PaymentReceivedId",
                table: "CreditAllowRefoundSettlement",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CreditAllowRefoundSettlement_PaymentReceivedId",
                table: "CreditAllowRefoundSettlement",
                column: "PaymentReceivedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAllowRefoundSettlement_PaymentReceived_PaymentReceivedId",
                table: "CreditAllowRefoundSettlement",
                column: "PaymentReceivedId",
                principalTable: "PaymentReceived",
                principalColumn: "PaymentReceivedId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditAllowRefoundSettlement_PaymentReceived_PaymentReceivedId",
                table: "CreditAllowRefoundSettlement");

            migrationBuilder.DropIndex(
                name: "IX_CreditAllowRefoundSettlement_PaymentReceivedId",
                table: "CreditAllowRefoundSettlement");

            migrationBuilder.DropColumn(
                name: "PaymentReceivedId",
                table: "CreditAllowRefoundSettlement");
        }
    }
}
