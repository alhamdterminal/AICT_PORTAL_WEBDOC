using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update557 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GDCargo",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "GDCargo",
                table: "ContainerIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GDCargo",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GDCargo",
                table: "ContainerIndex",
                nullable: true);
        }
    }
}
