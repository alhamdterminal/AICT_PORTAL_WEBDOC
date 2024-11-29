using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update541 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRefound",
                table: "CreditAllowed",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CreditAllowRefoundSettlement",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    CreditAllowRefoundSettlementId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankId = table.Column<long>(nullable: true),
                    AICTAccountNumber = table.Column<string>(nullable: true),
                    ReceivedAmount = table.Column<long>(nullable: false),
                    InsturmentNo = table.Column<long>(nullable: false),
                    InoviceNo = table.Column<string>(nullable: true),
                    KnockOffInvoiceNo = table.Column<string>(nullable: true),
                    KnockOffAmount = table.Column<long>(nullable: false),
                    NatureOfAmount = table.Column<string>(nullable: true),
                    PaymentFor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditAllowRefoundSettlement", x => x.CreditAllowRefoundSettlementId);
                    table.ForeignKey(
                        name: "FK_CreditAllowRefoundSettlement_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditAllowRefoundSettlement_BankId",
                table: "CreditAllowRefoundSettlement",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditAllowRefoundSettlement");

            migrationBuilder.DropColumn(
                name: "IsRefound",
                table: "CreditAllowed");
        }
    }
}
