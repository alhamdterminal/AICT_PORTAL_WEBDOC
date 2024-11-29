using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update587 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShipperId",
                table: "ExportContainerItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainerItem_ShipperId",
                table: "ExportContainerItem",
                column: "ShipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainerItem_Shipper_ShipperId",
                table: "ExportContainerItem",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainerItem_Shipper_ShipperId",
                table: "ExportContainerItem");

            migrationBuilder.DropIndex(
                name: "IX_ExportContainerItem_ShipperId",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "ExportContainerItem");
        }
    }
}
