using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update672 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImplementTo",
                table: "ShippingAgentExport",
                newName: "WeightAllow");

            migrationBuilder.RenameColumn(
                name: "ImplementFrom",
                table: "ShippingAgentExport",
                newName: "VehcileAmountAllow");

            migrationBuilder.AddColumn<string>(
                name: "AllowSpecialChargeFCL",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AllowSpecialChargeLCL",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillDateType",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillDateTypeFCL",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChargesName",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NTNNumber",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OtherCharges",
                table: "ShippingAgentExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "WeightAmount",
                table: "ShippingAgentExport",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowSpecialChargeFCL",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "AllowSpecialChargeLCL",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "BillDateType",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "BillDateTypeFCL",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "ChargesName",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "NTNNumber",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "OtherCharges",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "WeightAmount",
                table: "ShippingAgentExport");

            migrationBuilder.RenameColumn(
                name: "WeightAllow",
                table: "ShippingAgentExport",
                newName: "ImplementTo");

            migrationBuilder.RenameColumn(
                name: "VehcileAmountAllow",
                table: "ShippingAgentExport",
                newName: "ImplementFrom");
        }
    }
}
