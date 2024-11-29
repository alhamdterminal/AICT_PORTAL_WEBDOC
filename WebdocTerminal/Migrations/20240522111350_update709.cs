using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update709 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMarkComplete",
                table: "IndexWeighment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "IndexWeighment",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Sequence",
                table: "IndexWeighment",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMarkComplete",
                table: "IndexWeighment");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "IndexWeighment");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "IndexWeighment");
        }
    }
}
