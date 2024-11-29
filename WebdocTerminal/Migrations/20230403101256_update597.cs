using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update597 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillDateTypeCY",
                table: "Consigne",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillDateTypeFCL",
                table: "Consigne",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillDateTypeLCL",
                table: "Consigne",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillDateTypeCY",
                table: "ClearingAgent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillDateTypeFCL",
                table: "ClearingAgent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillDateTypeLCL",
                table: "ClearingAgent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillDateTypeCY",
                table: "Consigne");

            migrationBuilder.DropColumn(
                name: "BillDateTypeFCL",
                table: "Consigne");

            migrationBuilder.DropColumn(
                name: "BillDateTypeLCL",
                table: "Consigne");

            migrationBuilder.DropColumn(
                name: "BillDateTypeCY",
                table: "ClearingAgent");

            migrationBuilder.DropColumn(
                name: "BillDateTypeFCL",
                table: "ClearingAgent");

            migrationBuilder.DropColumn(
                name: "BillDateTypeLCL",
                table: "ClearingAgent");
        }
    }
}
