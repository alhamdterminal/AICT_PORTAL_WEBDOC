using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update622 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EDOHold_ContainerIndex_ContainerIndexId",
                table: "EDOHold");

            migrationBuilder.DropIndex(
                name: "IX_EDOHold_ContainerIndexId",
                table: "EDOHold");

            migrationBuilder.RenameColumn(
                name: "ContainerIndexId",
                table: "EDOHold",
                newName: "IndexNo");

            migrationBuilder.AddColumn<string>(
                name: "VirNo",
                table: "EDOHold",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VirNo",
                table: "EDOHold");

            migrationBuilder.RenameColumn(
                name: "IndexNo",
                table: "EDOHold",
                newName: "ContainerIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_EDOHold_ContainerIndexId",
                table: "EDOHold",
                column: "ContainerIndexId");

            migrationBuilder.AddForeignKey(
                name: "FK_EDOHold_ContainerIndex_ContainerIndexId",
                table: "EDOHold",
                column: "ContainerIndexId",
                principalTable: "ContainerIndex",
                principalColumn: "ContainerIndexId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
