using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update549 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ETADate",
                table: "PreAlterDetail",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "PreAlterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vessel",
                table: "PreAlterDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ETADate",
                table: "PreAlterDetail");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "PreAlterDetail");

            migrationBuilder.DropColumn(
                name: "Vessel",
                table: "PreAlterDetail");
        }
    }
}
