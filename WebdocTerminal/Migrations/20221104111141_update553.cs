using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update553 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankBranchId",
                table: "PreAlertPayOrder",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "PreAlertPayOrder",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Bvp",
                table: "PreAlertPayOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChequeNumber",
                table: "PreAlertPayOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankBranchId",
                table: "PreAlertPayOrder");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "PreAlertPayOrder");

            migrationBuilder.DropColumn(
                name: "Bvp",
                table: "PreAlertPayOrder");

            migrationBuilder.DropColumn(
                name: "ChequeNumber",
                table: "PreAlertPayOrder");
        }
    }
}
