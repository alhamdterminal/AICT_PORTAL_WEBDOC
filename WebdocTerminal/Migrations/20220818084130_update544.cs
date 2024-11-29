using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update544 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgentsChalNo",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeAddress",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeNTN",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeName",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpType",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackagesUnit",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PerformedDate",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Performedby",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordSequence",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingLineCode",
                table: "ElectronicDeliveryOrder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalRecords",
                table: "ElectronicDeliveryOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentsChalNo",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "ConsigneeAddress",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "ConsigneeNTN",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "ConsigneeName",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "OpType",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "PackagesUnit",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "PerformedDate",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "Performedby",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "RecordSequence",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "ShippingLineCode",
                table: "ElectronicDeliveryOrder");

            migrationBuilder.DropColumn(
                name: "TotalRecords",
                table: "ElectronicDeliveryOrder");
        }
    }
}
