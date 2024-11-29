using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update629 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Company",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "Company",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EditedBy",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaxNo",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NTN",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Company",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "FaxNo",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "NTN",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Company");
        }
    }
}
