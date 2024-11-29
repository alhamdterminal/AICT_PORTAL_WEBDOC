using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update546 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConsigneeNTN",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeName",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeNTN",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeName",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeNTN",
                table: "ContainerIndex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeName",
                table: "ContainerIndex",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsigneeNTN",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ConsigneeName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ConsigneeNTN",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "ConsigneeName",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "ConsigneeNTN",
                table: "ContainerIndex");

            migrationBuilder.DropColumn(
                name: "ConsigneeName",
                table: "ContainerIndex");
        }
    }
}
