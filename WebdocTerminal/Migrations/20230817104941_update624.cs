using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update624 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConsigneeAddress",
                table: "Consigne",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneePhoneNumber",
                table: "Consigne",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsigneeAddress",
                table: "Consigne");

            migrationBuilder.DropColumn(
                name: "ConsigneePhoneNumber",
                table: "Consigne");
        }
    }
}
