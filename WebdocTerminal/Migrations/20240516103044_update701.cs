using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update701 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CSEmptyContainerReceiveId",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CYContainer_CSEmptyContainerReceiveId",
                table: "CYContainer",
                column: "CSEmptyContainerReceiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_CYContainer_CSEmptyContainerReceive_CSEmptyContainerReceiveId",
                table: "CYContainer",
                column: "CSEmptyContainerReceiveId",
                principalTable: "CSEmptyContainerReceive",
                principalColumn: "CSEmptyContainerReceiveId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CYContainer_CSEmptyContainerReceive_CSEmptyContainerReceiveId",
                table: "CYContainer");

            migrationBuilder.DropIndex(
                name: "IX_CYContainer_CSEmptyContainerReceiveId",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "CSEmptyContainerReceiveId",
                table: "CYContainer");
        }
    }
}
