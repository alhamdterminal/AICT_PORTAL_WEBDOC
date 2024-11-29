using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update510 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StorageFreeDays",
                table: "StorageFreeDay",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StorageFreeDays",
                table: "StorageFreeDay",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
