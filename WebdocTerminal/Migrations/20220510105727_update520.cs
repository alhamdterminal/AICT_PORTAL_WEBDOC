using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update520 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExcessAmount",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PaymentReceived",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    PaymentReceivedId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AICTBankName = table.Column<string>(nullable: true),
                    AICTAccountNumber = table.Column<string>(nullable: true),
                    ReceivedAmount = table.Column<long>(nullable: false),
                    InsturmentNo = table.Column<long>(nullable: false),
                    CreditAllowed = table.Column<long>(nullable: false),
                    InoviceNo = table.Column<string>(nullable: true),
                    KnockOffAmount = table.Column<long>(nullable: false),
                    NatureOfAmount = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReceived", x => x.PaymentReceivedId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentReceived");

            migrationBuilder.DropColumn(
                name: "ExcessAmount",
                table: "Invoice");
        }
    }
}
