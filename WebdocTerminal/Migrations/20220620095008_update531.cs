using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update531 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectronicDeliveryOrder",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ElectronicDeliveryOrderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ref_No = table.Column<string>(nullable: true),
                    Doc_Tran_No = table.Column<string>(nullable: true),
                    Do_Date = table.Column<DateTime>(nullable: true),
                    Valid_Date = table.Column<DateTime>(nullable: true),
                    Agent_Name = table.Column<string>(nullable: true),
                    IGM_No = table.Column<string>(nullable: true),
                    Index_No = table.Column<string>(nullable: true),
                    BL_No = table.Column<string>(nullable: true),
                    Container_No = table.Column<string>(nullable: true),
                    CONT_Size = table.Column<string>(nullable: true),
                    Vessel_Name = table.Column<string>(nullable: true),
                    Voyage = table.Column<string>(nullable: true),
                    NetWeight = table.Column<double>(nullable: false),
                    Port_of_Arrival = table.Column<string>(nullable: true),
                    Marks_No = table.Column<string>(nullable: true),
                    No_Of_Pkgs = table.Column<string>(nullable: true),
                    Gross_Weight = table.Column<double>(nullable: false),
                    PKg_Description = table.Column<string>(nullable: true),
                    Goods_Desc = table.Column<string>(nullable: true),
                    Oracle = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicDeliveryOrder", x => x.ElectronicDeliveryOrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectronicDeliveryOrder");
        }
    }
}
