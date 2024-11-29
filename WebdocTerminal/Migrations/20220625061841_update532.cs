using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update532 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCrossStuffed",
                table: "CYContainer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GDCR",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    GDCRId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OldContainerNumber = table.Column<string>(nullable: true),
                    NewContainerNumber = table.Column<string>(nullable: true),
                    GDNumber = table.Column<string>(nullable: true),
                    BLNumber = table.Column<string>(nullable: true),
                    VirNumber = table.Column<string>(nullable: true),
                    ContainerConsolidation = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    OperationType = table.Column<string>(nullable: true),
                    Flag1 = table.Column<string>(nullable: true),
                    Flag2 = table.Column<string>(nullable: true),
                    Flag3 = table.Column<string>(nullable: true),
                    IsContainerStuffed = table.Column<bool>(nullable: false),
                    IsSubmit = table.Column<bool>(nullable: false),
                    IsGateOut = table.Column<bool>(nullable: false),
                    Performed = table.Column<DateTime>(nullable: true),
                    MessageFrom = table.Column<string>(nullable: true),
                    MessageTo = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    IsProcessed = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GDCR", x => x.GDCRId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GDCR");

            migrationBuilder.DropColumn(
                name: "IsCrossStuffed",
                table: "CYContainer");
        }
    }
}
