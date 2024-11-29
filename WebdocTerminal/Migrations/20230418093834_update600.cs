using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update600 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PortOfLoadingId",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PortOfLoadingId",
                table: "ContainerIndex",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PortOfLoading",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    PortOfLoadingId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PortOfLoadingName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortOfLoading", x => x.PortOfLoadingId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CYContainer_PortOfLoadingId",
                table: "CYContainer",
                column: "PortOfLoadingId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerIndex_PortOfLoadingId",
                table: "ContainerIndex",
                column: "PortOfLoadingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerIndex_PortOfLoading_PortOfLoadingId",
                table: "ContainerIndex",
                column: "PortOfLoadingId",
                principalTable: "PortOfLoading",
                principalColumn: "PortOfLoadingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CYContainer_PortOfLoading_PortOfLoadingId",
                table: "CYContainer",
                column: "PortOfLoadingId",
                principalTable: "PortOfLoading",
                principalColumn: "PortOfLoadingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerIndex_PortOfLoading_PortOfLoadingId",
                table: "ContainerIndex");

            migrationBuilder.DropForeignKey(
                name: "FK_CYContainer_PortOfLoading_PortOfLoadingId",
                table: "CYContainer");

            migrationBuilder.DropTable(
                name: "PortOfLoading");

            migrationBuilder.DropIndex(
                name: "IX_CYContainer_PortOfLoadingId",
                table: "CYContainer");

            migrationBuilder.DropIndex(
                name: "IX_ContainerIndex_PortOfLoadingId",
                table: "ContainerIndex");

            migrationBuilder.DropColumn(
                name: "PortOfLoadingId",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "PortOfLoadingId",
                table: "ContainerIndex");
        }
    }
}
