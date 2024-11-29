using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update660 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExportDropOfTicket",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ExportDropOfTicketId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GDNumber = table.Column<string>(nullable: true),
                    ShippingLine = table.Column<string>(nullable: true),
                    TruckNumber = table.Column<string>(nullable: true),
                    ManifestedSealNumber = table.Column<string>(nullable: true),
                    InTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportDropOfTicket", x => x.ExportDropOfTicketId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportDropOfTicket");
        }
    }
}
