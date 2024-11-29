using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update711 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TariffInformationId",
                table: "ScheduleOfCharge",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "OtherChargeAssigning",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "OtherChargeAssigning",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TariffInformationId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleOfCharge_TariffInformationId",
                table: "ScheduleOfCharge",
                column: "TariffInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherChargeAssigning_InvoiceId",
                table: "OtherChargeAssigning",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_TariffInformationId",
                table: "Invoice",
                column: "TariffInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_TariffInformation_TariffInformationId",
                table: "Invoice",
                column: "TariffInformationId",
                principalTable: "TariffInformation",
                principalColumn: "TariffInformationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherChargeAssigning_Invoice_InvoiceId",
                table: "OtherChargeAssigning",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleOfCharge_TariffInformation_TariffInformationId",
                table: "ScheduleOfCharge",
                column: "TariffInformationId",
                principalTable: "TariffInformation",
                principalColumn: "TariffInformationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_TariffInformation_TariffInformationId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherChargeAssigning_Invoice_InvoiceId",
                table: "OtherChargeAssigning");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleOfCharge_TariffInformation_TariffInformationId",
                table: "ScheduleOfCharge");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleOfCharge_TariffInformationId",
                table: "ScheduleOfCharge");

            migrationBuilder.DropIndex(
                name: "IX_OtherChargeAssigning_InvoiceId",
                table: "OtherChargeAssigning");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_TariffInformationId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TariffInformationId",
                table: "ScheduleOfCharge");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "OtherChargeAssigning");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "OtherChargeAssigning");

            migrationBuilder.DropColumn(
                name: "TariffInformationId",
                table: "Invoice");
        }
    }
}
