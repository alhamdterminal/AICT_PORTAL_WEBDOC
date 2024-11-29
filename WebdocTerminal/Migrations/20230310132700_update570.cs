using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update570 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EDOHold",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    EDOHoldId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsEDOHold = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    ContainerIndexId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDOHold", x => x.EDOHoldId);
                    table.ForeignKey(
                        name: "FK_EDOHold_ContainerIndex_ContainerIndexId",
                        column: x => x.ContainerIndexId,
                        principalTable: "ContainerIndex",
                        principalColumn: "ContainerIndexId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EDOHold_ContainerIndexId",
                table: "EDOHold",
                column: "ContainerIndexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EDOHold");
        }
    }
}
