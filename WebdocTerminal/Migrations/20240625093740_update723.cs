using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update723 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalArriveIndexes",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Query",
                table: "CRSummaryHeadDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalArriveIndexes",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Query",
                table: "CRSummaryHeadDetail");
        }
    }
}
