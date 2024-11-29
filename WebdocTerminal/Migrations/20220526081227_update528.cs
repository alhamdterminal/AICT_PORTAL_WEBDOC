using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update528 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcessAmountRefundSettlement",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ExcessAmountRefundSettlementId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ChequeNumber = table.Column<string>(nullable: true),
                    ChequeDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<long>(nullable: false),
                    InFavorof = table.Column<string>(nullable: true),
                    AICTServiceCharges = table.Column<long>(nullable: false),
                    OnllineAccountNumber = table.Column<string>(nullable: true),
                    OnllineAccountTitle = table.Column<string>(nullable: true),
                    OnllineAccountAmount = table.Column<long>(nullable: false),
                    BankId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcessAmountRefundSettlement", x => x.ExcessAmountRefundSettlementId);
                    table.ForeignKey(
                        name: "FK_ExcessAmountRefundSettlement_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcessAmountRefundSettlement_BankId",
                table: "ExcessAmountRefundSettlement",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcessAmountRefundSettlement");
        }
    }
}
