using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update567 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportorName",
                table: "EmptyGateOutContainer");

            migrationBuilder.AddColumn<long>(
                name: "TransporterId",
                table: "EmptyGateOutContainer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmptyGateOutContainer_TransporterId",
                table: "EmptyGateOutContainer",
                column: "TransporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmptyGateOutContainer_Transporter_TransporterId",
                table: "EmptyGateOutContainer",
                column: "TransporterId",
                principalTable: "Transporter",
                principalColumn: "TransporterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmptyGateOutContainer_Transporter_TransporterId",
                table: "EmptyGateOutContainer");

            migrationBuilder.DropIndex(
                name: "IX_EmptyGateOutContainer_TransporterId",
                table: "EmptyGateOutContainer");

            migrationBuilder.DropColumn(
                name: "TransporterId",
                table: "EmptyGateOutContainer");

            migrationBuilder.AddColumn<string>(
                name: "TransportorName",
                table: "EmptyGateOutContainer",
                nullable: true);
        }
    }
}
