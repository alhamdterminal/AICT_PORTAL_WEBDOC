using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update566 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllowSpecialChargeCY",
                table: "ShippingAgent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AllowSpecialChargeFCL",
                table: "ShippingAgent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AllowSpecialChargeLCL",
                table: "ShippingAgent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowSpecialChargeCY",
                table: "ShippingAgent");

            migrationBuilder.DropColumn(
                name: "AllowSpecialChargeFCL",
                table: "ShippingAgent");

            migrationBuilder.DropColumn(
                name: "AllowSpecialChargeLCL",
                table: "ShippingAgent");
        }
    }
}
