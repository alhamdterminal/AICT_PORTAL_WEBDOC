using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update652 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Detention",
                table: "PaymentUpdateDetail",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LOLOIncInTHC",
                table: "PaymentUpdateDetail",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detention",
                table: "PaymentUpdateDetail");

            migrationBuilder.DropColumn(
                name: "LOLOIncInTHC",
                table: "PaymentUpdateDetail");
        }
    }
}
