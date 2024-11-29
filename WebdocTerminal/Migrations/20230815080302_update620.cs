using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update620 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HoldType",
                table: "Hold",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IndexNo",
                table: "Hold",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Nature",
                table: "Hold",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemoveInstruction",
                table: "Hold",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Hold",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VirNo",
                table: "Hold",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoldType",
                table: "Hold");

            migrationBuilder.DropColumn(
                name: "IndexNo",
                table: "Hold");

            migrationBuilder.DropColumn(
                name: "Nature",
                table: "Hold");

            migrationBuilder.DropColumn(
                name: "RemoveInstruction",
                table: "Hold");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Hold");

            migrationBuilder.DropColumn(
                name: "VirNo",
                table: "Hold");
        }
    }
}
