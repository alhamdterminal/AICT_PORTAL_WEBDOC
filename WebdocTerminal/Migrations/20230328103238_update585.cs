using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update585 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.DropIndex(
                name: "IX_ExportContainers_ShippingLineId",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "ShippingLineId",
                table: "ExportContainers");




            migrationBuilder.DropForeignKey(
                name: "FK_GateoutExports_PortAndTerminal_PortAndTerminalId",
                table: "GateoutExports");

            migrationBuilder.DropIndex(
                name: "IX_GateoutExports_PortAndTerminalId",
                table: "GateoutExports");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "GateoutExports");

             
            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainerItem_Shipper_ShipperId",
                table: "ExportContainerItem");

            migrationBuilder.DropIndex(
                name: "IX_ExportContainerItem_ShipperId",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "ExportContainerItem");

            //Startup loading progam


            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "LoadingProgram");


            migrationBuilder.DropForeignKey(
              name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
              table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "ShippingLineId",
                table: "LoadingProgram");


            migrationBuilder.DropForeignKey(
              name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
              table: "LoadingProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoadingProgram_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropColumn(
                name: "PortAndTerminalId",
                table: "LoadingProgram");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 
             
            migrationBuilder.AddColumn<long>(
             name: "ShippingLineId",
             table: "ExportContainers",
             nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainers_ShippingLineId",
                table: "ExportContainers",
                column: "ShippingLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainers_ShippingLine_ShippingLineId",
                table: "ExportContainers",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);



            migrationBuilder.AddColumn<long>(
             name: "PortAndTerminalId",
             table: "GateoutExports",
             nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GateoutExports_PortAndTerminalId",
                table: "GateoutExports",
                column: "PortAndTerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateoutExports_PortAndTerminal_PortAndTerminalId",
                table: "GateoutExports",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);
            
            
            
            
            migrationBuilder.AddColumn<long>(
             name: "ShipperId",
             table: "ExportContainerItem",
             nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainerItem_ShipperId",
                table: "ExportContainerItem",
                column: "ShipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainerItem_Shipper_ShipperId",
                table: "ExportContainerItem",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);

            //Startup loading progam ShipperId


            migrationBuilder.AddColumn<long>(
           name: "ShipperId",
           table: "LoadingProgram",
           nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);


            //Startup loading progam ShippingLineId


            migrationBuilder.AddColumn<long>(
           name: "ShippingLineId",
           table: "LoadingProgram",
           nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);


            //Startup loading progam PortAndTerminal


            migrationBuilder.AddColumn<long>(
           name: "PortAndTerminalId",
           table: "LoadingProgram",
           nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoadingProgram_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
