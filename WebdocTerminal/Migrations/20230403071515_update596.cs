using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update596 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ChargeQuantity40",
                table: "ExaminationChargeAssign",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ChargeQuantity45",
                table: "ExaminationChargeAssign",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeQuantity40",
                table: "ExaminationChargeAssign");

            migrationBuilder.DropColumn(
                name: "ChargeQuantity45",
                table: "ExaminationChargeAssign");
        }
    }
}
