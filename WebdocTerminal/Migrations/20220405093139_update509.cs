using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update509 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TariffId",
                table: "StorageFreeDay",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageFreeDay_TariffId",
                table: "StorageFreeDay",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageFreeDay_Tariff_TariffId",
                table: "StorageFreeDay",
                column: "TariffId",
                principalTable: "Tariff",
                principalColumn: "TariffId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageFreeDay_Tariff_TariffId",
                table: "StorageFreeDay");

            migrationBuilder.DropIndex(
                name: "IX_StorageFreeDay_TariffId",
                table: "StorageFreeDay");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "StorageFreeDay");
        }
    }
}
