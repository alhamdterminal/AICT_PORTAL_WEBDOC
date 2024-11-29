using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update638 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChartOfAccount",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    EditedBy = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ChartOfAccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccCode = table.Column<string>(nullable: true),
                    AccPCode = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    AccountTypeId = table.Column<long>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartOfAccount", x => x.ChartOfAccountId);
                    table.ForeignKey(
                        name: "FK_ChartOfAccount_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherServiceTypeDetail",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    VoucherServiceTypeDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VoucherServiceTypeId = table.Column<long>(nullable: false),
                    ChartOfAccountId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherServiceTypeDetail", x => x.VoucherServiceTypeDetailId);
                    table.ForeignKey(
                        name: "FK_VoucherServiceTypeDetail_ChartOfAccount_ChartOfAccountId",
                        column: x => x.ChartOfAccountId,
                        principalTable: "ChartOfAccount",
                        principalColumn: "ChartOfAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherServiceTypeDetail_VoucherServiceType_VoucherServiceTypeId",
                        column: x => x.VoucherServiceTypeId,
                        principalTable: "VoucherServiceType",
                        principalColumn: "VoucherServiceTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccount_AccountTypeId",
                table: "ChartOfAccount",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherServiceTypeDetail_ChartOfAccountId",
                table: "VoucherServiceTypeDetail",
                column: "ChartOfAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherServiceTypeDetail_VoucherServiceTypeId",
                table: "VoucherServiceTypeDetail",
                column: "VoucherServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherServiceTypeDetail");

            migrationBuilder.DropTable(
                name: "ChartOfAccount");
        }
    }
}
