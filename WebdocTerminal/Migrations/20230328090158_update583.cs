using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update583 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.AlterColumn<long>(
                name: "ShippingLineId",
                table: "LoadingProgram",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ShipperId",
                table: "LoadingProgram",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "PortAndTerminalId",
                table: "LoadingProgram",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ShippingLineId",
                table: "ExportContainers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.AlterColumn<long>(
                name: "ShippingLineId",
                table: "LoadingProgram",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ShipperId",
                table: "LoadingProgram",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PortAndTerminalId",
                table: "LoadingProgram",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ShippingLineId",
                table: "ExportContainers",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
