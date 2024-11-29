using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update503 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewRemarks",
                table: "WorkOrderCSD");

            migrationBuilder.DropColumn(
                name: "NewRemarksec",
                table: "WorkOrderCSD");

            migrationBuilder.DropColumn(
                name: "NewLocation",
                table: "BRT");

            migrationBuilder.AddColumn<string>(
                name: "Vessel",
                table: "PreAlert",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vessel",
                table: "PreAlert");

            migrationBuilder.AddColumn<string>(
                name: "NewRemarks",
                table: "WorkOrderCSD",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewRemarksec",
                table: "WorkOrderCSD",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewLocation",
                table: "BRT",
                nullable: true);
        }
    }
}
