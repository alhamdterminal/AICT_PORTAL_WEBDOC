using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update706 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllowAutoGroundingFCL",
                table: "GoodsHead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AllowAutoGroundingLCL",
                table: "GoodsHead",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowAutoGroundingFCL",
                table: "GoodsHead");

            migrationBuilder.DropColumn(
                name: "AllowAutoGroundingLCL",
                table: "GoodsHead");
        }
    }
}
