using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update552 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryOrder_CYContainerId",
                table: "DeliveryOrder");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryOrder_ContainerIndexId",
                table: "DeliveryOrder");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_CYContainerId",
                table: "DeliveryOrder",
                column: "CYContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_ContainerIndexId",
                table: "DeliveryOrder",
                column: "ContainerIndexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryOrder_CYContainerId",
                table: "DeliveryOrder");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryOrder_ContainerIndexId",
                table: "DeliveryOrder");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_CYContainerId",
                table: "DeliveryOrder",
                column: "CYContainerId",
                unique: true,
                filter: "[CYContainerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_ContainerIndexId",
                table: "DeliveryOrder",
                column: "ContainerIndexId",
                unique: true,
                filter: "[ContainerIndexId] IS NOT NULL");
        }
    }
}
