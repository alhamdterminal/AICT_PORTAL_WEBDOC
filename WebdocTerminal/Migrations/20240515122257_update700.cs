﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update700 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TireWeight",
                table: "CSEmptyContainerReceive",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TireWeight",
                table: "CSEmptyContainerReceive");
        }
    }
}
