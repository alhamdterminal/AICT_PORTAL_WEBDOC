using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update679 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineInvoiceDetail",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    LineInvoiceDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceCode = table.Column<string>(nullable: true),
                    MainHeaderDetail = table.Column<string>(nullable: true),
                    SubHeaderDetail = table.Column<string>(nullable: true),
                    MainFooter = table.Column<string>(nullable: true),
                    SubFooter = table.Column<string>(nullable: true),
                    MainLogo = table.Column<string>(nullable: true),
                    SubLogo = table.Column<string>(nullable: true),
                    ShippingAgentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineInvoiceDetail", x => x.LineInvoiceDetailId);
                    table.ForeignKey(
                        name: "FK_LineInvoiceDetail_ShippingAgent_ShippingAgentId",
                        column: x => x.ShippingAgentId,
                        principalTable: "ShippingAgent",
                        principalColumn: "ShippingAgentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineInvoiceDetail_ShippingAgentId",
                table: "LineInvoiceDetail",
                column: "ShippingAgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineInvoiceDetail");
        }
    }
}
