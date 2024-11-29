using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update507 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClearingAgentId",
                table: "GroundingFreeDay",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GoodsHeadId",
                table: "GroundingFreeDay",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StorageFreeFreeDays",
                table: "GroundingFreeDay",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroundingFreeDay_ClearingAgentId",
                table: "GroundingFreeDay",
                column: "ClearingAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroundingFreeDay_GoodsHeadId",
                table: "GroundingFreeDay",
                column: "GoodsHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroundingFreeDay_ClearingAgent_ClearingAgentId",
                table: "GroundingFreeDay",
                column: "ClearingAgentId",
                principalTable: "ClearingAgent",
                principalColumn: "ClearingAgentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroundingFreeDay_GoodsHead_GoodsHeadId",
                table: "GroundingFreeDay",
                column: "GoodsHeadId",
                principalTable: "GoodsHead",
                principalColumn: "GoodsHeadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroundingFreeDay_ClearingAgent_ClearingAgentId",
                table: "GroundingFreeDay");

            migrationBuilder.DropForeignKey(
                name: "FK_GroundingFreeDay_GoodsHead_GoodsHeadId",
                table: "GroundingFreeDay");

            migrationBuilder.DropIndex(
                name: "IX_GroundingFreeDay_ClearingAgentId",
                table: "GroundingFreeDay");

            migrationBuilder.DropIndex(
                name: "IX_GroundingFreeDay_GoodsHeadId",
                table: "GroundingFreeDay");

            migrationBuilder.DropColumn(
                name: "ClearingAgentId",
                table: "GroundingFreeDay");

            migrationBuilder.DropColumn(
                name: "GoodsHeadId",
                table: "GroundingFreeDay");

            migrationBuilder.DropColumn(
                name: "StorageFreeFreeDays",
                table: "GroundingFreeDay");
        }
    }
}
