using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update584 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "test",
                table: "GateoutExports");

            migrationBuilder.DropColumn(
                name: "test",
                table: "ExportContainers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "LoadingProgram",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "GateoutExports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "ExportContainers",
                nullable: true);
        }
    }
}
