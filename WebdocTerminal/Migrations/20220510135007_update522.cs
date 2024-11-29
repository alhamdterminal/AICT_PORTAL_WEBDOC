using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update522 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "PaymentReceived",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceived_BankId",
                table: "PaymentReceived",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceived_Bank_BankId",
                table: "PaymentReceived",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceived_Bank_BankId",
                table: "PaymentReceived");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceived_BankId",
                table: "PaymentReceived");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "PaymentReceived");
        }
    }
}
