using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update702 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmptyGateOutContainer_ContainerIndex_ContainerIndexId",
                table: "EmptyGateOutContainer");

            migrationBuilder.AlterColumn<long>(
                name: "ContainerIndexId",
                table: "EmptyGateOutContainer",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "CYContainerId",
                table: "EmptyGateOutContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "EmptyGateOutContainer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmptyGateOutContainer_CYContainerId",
                table: "EmptyGateOutContainer",
                column: "CYContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmptyGateOutContainer_CYContainer_CYContainerId",
                table: "EmptyGateOutContainer",
                column: "CYContainerId",
                principalTable: "CYContainer",
                principalColumn: "CYContainerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmptyGateOutContainer_ContainerIndex_ContainerIndexId",
                table: "EmptyGateOutContainer",
                column: "ContainerIndexId",
                principalTable: "ContainerIndex",
                principalColumn: "ContainerIndexId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmptyGateOutContainer_CYContainer_CYContainerId",
                table: "EmptyGateOutContainer");

            migrationBuilder.DropForeignKey(
                name: "FK_EmptyGateOutContainer_ContainerIndex_ContainerIndexId",
                table: "EmptyGateOutContainer");

            migrationBuilder.DropIndex(
                name: "IX_EmptyGateOutContainer_CYContainerId",
                table: "EmptyGateOutContainer");

            migrationBuilder.DropColumn(
                name: "CYContainerId",
                table: "EmptyGateOutContainer");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EmptyGateOutContainer");

            migrationBuilder.AlterColumn<long>(
                name: "ContainerIndexId",
                table: "EmptyGateOutContainer",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmptyGateOutContainer_ContainerIndex_ContainerIndexId",
                table: "EmptyGateOutContainer",
                column: "ContainerIndexId",
                principalTable: "ContainerIndex",
                principalColumn: "ContainerIndexId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
