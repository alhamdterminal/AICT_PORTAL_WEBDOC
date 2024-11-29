using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update598 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "PGO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MessageTo",
                table: "PGO",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderNumber",
                table: "GDCR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CSCondition",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CSContainerNumber",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CSCountryCode",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CSReceivedDate",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CSSize",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CSTireWeight",
                table: "CYContainer",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CSVehicleNumer",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerStatus",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmptyReceivedShippingLineId",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCSEmtyptyRecieved",
                table: "CYContainer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BoundedTranspoter",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    BoundedTranspoterId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoundedNTN = table.Column<string>(nullable: true),
                    BoundedCarrierName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoundedTranspoter", x => x.BoundedTranspoterId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoundedTranspoter");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "PGO");

            migrationBuilder.DropColumn(
                name: "MessageTo",
                table: "PGO");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "GDCR");

            migrationBuilder.DropColumn(
                name: "CSCondition",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "CSContainerNumber",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "CSCountryCode",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "CSReceivedDate",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "CSSize",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "CSTireWeight",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "CSVehicleNumer",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "ContainerStatus",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "EmptyReceivedShippingLineId",
                table: "CYContainer");

            migrationBuilder.DropColumn(
                name: "IsCSEmtyptyRecieved",
                table: "CYContainer");
        }
    }
}
