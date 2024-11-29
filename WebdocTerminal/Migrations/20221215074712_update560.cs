using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update560 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MasterShippingAgentId",
                table: "ShippingAgent",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MasterShippingAgent",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    MasterShippingAgentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MasterShippingAgentCode = table.Column<long>(nullable: false),
                    MasterShippingAgentName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterShippingAgent", x => x.MasterShippingAgentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgent_MasterShippingAgentId",
                table: "ShippingAgent",
                column: "MasterShippingAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAgent_MasterShippingAgent_MasterShippingAgentId",
                table: "ShippingAgent",
                column: "MasterShippingAgentId",
                principalTable: "MasterShippingAgent",
                principalColumn: "MasterShippingAgentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAgent_MasterShippingAgent_MasterShippingAgentId",
                table: "ShippingAgent");

            migrationBuilder.DropTable(
                name: "MasterShippingAgent");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAgent_MasterShippingAgentId",
                table: "ShippingAgent");

            migrationBuilder.DropColumn(
                name: "MasterShippingAgentId",
                table: "ShippingAgent");
        }
    }
}
