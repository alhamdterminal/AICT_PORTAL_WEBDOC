using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update574 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "PGateInOut",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitReason",
                table: "PGateInOut",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorCompany",
                table: "PGateInOut",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "PGateInOut");

            migrationBuilder.DropColumn(
                name: "VisitReason",
                table: "PGateInOut");

            migrationBuilder.DropColumn(
                name: "VisitorCompany",
                table: "PGateInOut");
        }
    }
}
