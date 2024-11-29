using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update572 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllowExaminationAutoChrges",
                table: "ShippingAgent",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AutoExaminationCharge",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    AutoExaminationChargeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutoExaminationChargeName = table.Column<string>(nullable: true),
                    ExaminationChargeAmount20 = table.Column<double>(nullable: false),
                    ExaminationChargeAmount40 = table.Column<double>(nullable: false),
                    ExaminationChargeAmount45 = table.Column<double>(nullable: false),
                    ExaminationChargeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoExaminationCharge", x => x.AutoExaminationChargeId);
                    table.ForeignKey(
                        name: "FK_AutoExaminationCharge_ExaminationCharge_ExaminationChargeId",
                        column: x => x.ExaminationChargeId,
                        principalTable: "ExaminationCharge",
                        principalColumn: "ExaminationChargeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoExaminationCharge_ExaminationChargeId",
                table: "AutoExaminationCharge",
                column: "ExaminationChargeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoExaminationCharge");

            migrationBuilder.DropColumn(
                name: "AllowExaminationAutoChrges",
                table: "ShippingAgent");
        }
    }
}
