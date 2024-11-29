using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update610 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VehicleMeasurementId",
                table: "DestuffedContainer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DestuffedContainer_VehicleMeasurementId",
                table: "DestuffedContainer",
                column: "VehicleMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_DestuffedContainer_VehicleMeasurement_VehicleMeasurementId",
                table: "DestuffedContainer",
                column: "VehicleMeasurementId",
                principalTable: "VehicleMeasurement",
                principalColumn: "VehicleMeasurementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestuffedContainer_VehicleMeasurement_VehicleMeasurementId",
                table: "DestuffedContainer");

            migrationBuilder.DropIndex(
                name: "IX_DestuffedContainer_VehicleMeasurementId",
                table: "DestuffedContainer");

            migrationBuilder.DropColumn(
                name: "VehicleMeasurementId",
                table: "DestuffedContainer");
        }
    }
}
