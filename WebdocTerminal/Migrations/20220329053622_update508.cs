using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update508 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "GroundingFreeDays",
                table: "GroundingFreeDay",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "GroundingFreeDays",
                table: "GroundingFreeDay",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
