using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update534 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefoundAmount",
                table: "ExcessAmountRefundSettlement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ExcessAmountRefundSettlement",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefoundAmount",
                table: "ExcessAmountRefundSettlement");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ExcessAmountRefundSettlement");
        }
    }
}
