using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update519 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "CreditAllowed",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsSettled",
                table: "CreditAllowed",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "CreditAllowed");

            migrationBuilder.DropColumn(
                name: "IsSettled",
                table: "CreditAllowed");
        }
    }
}
