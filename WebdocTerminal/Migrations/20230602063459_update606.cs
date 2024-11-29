using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update606 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "CreditAllowed",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "CreditAllowed",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReject",
                table: "CreditAllowed",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StatusType",
                table: "CreditAllowed",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "CreditAllowed");

            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "CreditAllowed");

            migrationBuilder.DropColumn(
                name: "IsReject",
                table: "CreditAllowed");

            migrationBuilder.DropColumn(
                name: "StatusType",
                table: "CreditAllowed");
        }
    }
}
