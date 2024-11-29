using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update545 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContainerLocation",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerLocation",
                table: "ContainerIndex",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerLocation",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "ContainerLocation",
                table: "ContainerIndex");
        }
    }
}
