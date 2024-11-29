﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update615 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Waiver",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Waiver");
        }
    }
}
