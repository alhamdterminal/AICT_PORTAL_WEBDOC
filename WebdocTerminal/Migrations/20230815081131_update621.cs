using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update621 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hold_CYContainer_CYContainerId",
                table: "Hold");

            migrationBuilder.DropForeignKey(
                name: "FK_Hold_ContainerIndex_ContainerIndexId",
                table: "Hold");

            migrationBuilder.DropIndex(
                name: "IX_Hold_CYContainerId",
                table: "Hold");

            migrationBuilder.DropIndex(
                name: "IX_Hold_ContainerIndexId",
                table: "Hold");

            migrationBuilder.DropColumn(
                name: "CYContainerId",
                table: "Hold");

            migrationBuilder.DropColumn(
                name: "ContainerIndexId",
                table: "Hold");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CYContainerId",
                table: "Hold",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ContainerIndexId",
                table: "Hold",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hold_CYContainerId",
                table: "Hold",
                column: "CYContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hold_ContainerIndexId",
                table: "Hold",
                column: "ContainerIndexId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hold_CYContainer_CYContainerId",
                table: "Hold",
                column: "CYContainerId",
                principalTable: "CYContainer",
                principalColumn: "CYContainerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hold_ContainerIndex_ContainerIndexId",
                table: "Hold",
                column: "ContainerIndexId",
                principalTable: "ContainerIndex",
                principalColumn: "ContainerIndexId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
