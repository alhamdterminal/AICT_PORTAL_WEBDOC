using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update646 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    EditedBy = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    VoucherId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VoucherNo = table.Column<string>(nullable: true),
                    VoucherTypeId = table.Column<long>(nullable: false),
                    Month = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    VoucherDate = table.Column<DateTime>(nullable: false),
                    Narration = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CompanyId = table.Column<long>(nullable: false),
                    VerifyById = table.Column<long>(nullable: true),
                    VerifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.VoucherId);
                });

            migrationBuilder.CreateTable(
                name: "VoucherDetail",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    EditedBy = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    VoucherDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TranDate = table.Column<DateTime>(nullable: false),
                    VoucherId = table.Column<long>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false),
                    VoucherTypeId = table.Column<long>(nullable: false),
                    DepartmentId = table.Column<long>(nullable: false),
                    VoucherServiceTypeId = table.Column<long>(nullable: false),
                    ChartOfAccountId = table.Column<long>(nullable: false),
                    AccCode = table.Column<string>(nullable: true),
                    Debit = table.Column<decimal>(nullable: true),
                    Credit = table.Column<decimal>(nullable: true),
                    ChequeOrReference = table.Column<string>(nullable: true),
                    Narration = table.Column<string>(nullable: true),
                    LineNumber = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDetail", x => x.VoucherDetailId);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_ChartOfAccount_ChartOfAccountId",
                        column: x => x.ChartOfAccountId,
                        principalTable: "ChartOfAccount",
                        principalColumn: "ChartOfAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_VoucherServiceType_VoucherServiceTypeId",
                        column: x => x.VoucherServiceTypeId,
                        principalTable: "VoucherServiceType",
                        principalColumn: "VoucherServiceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_VoucherType_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalTable: "VoucherType",
                        principalColumn: "VoucherTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_ChartOfAccountId",
                table: "VoucherDetail",
                column: "ChartOfAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_CustomerId",
                table: "VoucherDetail",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_VoucherId",
                table: "VoucherDetail",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_VoucherServiceTypeId",
                table: "VoucherDetail",
                column: "VoucherServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_VoucherTypeId",
                table: "VoucherDetail",
                column: "VoucherTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherDetail");

            migrationBuilder.DropTable(
                name: "Voucher");
        }
    }
}
