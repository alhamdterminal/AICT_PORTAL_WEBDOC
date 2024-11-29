using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update724 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TransporterId",
                table: "GateoutExports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GateoutExports_TransporterId",
                table: "GateoutExports",
                column: "TransporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateoutExports_Transporter_TransporterId",
                table: "GateoutExports",
                column: "TransporterId",
                principalTable: "Transporter",
                principalColumn: "TransporterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateoutExports_Transporter_TransporterId",
                table: "GateoutExports");

            migrationBuilder.DropIndex(
                name: "IX_GateoutExports_TransporterId",
                table: "GateoutExports");

            migrationBuilder.DropColumn(
                name: "TransporterId",
                table: "GateoutExports");
        }
    }
}
