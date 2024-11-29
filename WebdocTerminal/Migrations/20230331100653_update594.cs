using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update594 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExaminationDate",
                table: "ExaminationRequest",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GroundingDate",
                table: "ExaminationRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExaminationDate",
                table: "ExaminationRequest");

            migrationBuilder.DropColumn(
                name: "GroundingDate",
                table: "ExaminationRequest");
        }
    }
}
