using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update586 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShippingLineId",
                table: "ExportContainers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainers_ShippingLineId",
                table: "ExportContainers",
                column: "ShippingLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.DropIndex(
                name: "IX_ExportContainers_ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "ShippingLineId",
                table: "ExportContainers");
        }
    }
}
