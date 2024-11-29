using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update467 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChequePrinting",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    EditedBy = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ChequePrintingId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VoucherNo = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    VoucherDate = table.Column<DateTime>(nullable: false),
                    ChequeNo = table.Column<string>(nullable: true),
                    PartyName = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    AmountInWords = table.Column<string>(nullable: true),
                    VoucherId = table.Column<long>(nullable: false),
                    VoucherDetailId = table.Column<long>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false),
                    Count = table.Column<long>(nullable: false),
                    IsPrinted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequePrinting", x => x.ChequePrintingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChequePrinting");
        }
    }
}
