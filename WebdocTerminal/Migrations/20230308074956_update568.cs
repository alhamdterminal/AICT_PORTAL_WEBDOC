using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update568 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EDIMaerskMessageTOS",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    EDIMaerskMessageTOSId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CONTAINER_ID = table.Column<long>(nullable: true),
                    EVENT_ID = table.Column<string>(nullable: true),
                    SHIPPING_LINE_ID = table.Column<long>(nullable: true),
                    MAERSKPARTNERID = table.Column<string>(nullable: true),
                    VOYAGE_NO = table.Column<string>(nullable: true),
                    VESSEL_CODE = table.Column<string>(nullable: true),
                    CONTAINER_NO = table.Column<string>(nullable: true),
                    CN_ISO_CODE = table.Column<string>(nullable: true),
                    BL_NO = table.Column<string>(nullable: true),
                    GATE_IN_DATE = table.Column<string>(nullable: true),
                    GATE_OUT_DATE = table.Column<string>(nullable: true),
                    DESTUFF_DATE = table.Column<string>(nullable: true),
                    BL_GROSS_WEIGHT = table.Column<decimal>(nullable: true),
                    MAN_SEAL_NO = table.Column<string>(nullable: true),
                    REMARKS = table.Column<string>(nullable: true),
                    TRUCK_NO = table.Column<string>(nullable: true),
                    CONTAINER_COUNT = table.Column<long>(nullable: true),
                    SEGMENT_NO = table.Column<long>(nullable: true),
                    SEQUENCE_NO = table.Column<string>(nullable: true),
                    MESSAGE_SEND_DATETIME = table.Column<string>(nullable: true),
                    CARRIER_ID = table.Column<string>(nullable: true),
                    VESSEL_CALL_SIGN = table.Column<string>(nullable: true),
                    CONTAINER_TYPE = table.Column<string>(nullable: true),
                    IS_MARSK = table.Column<bool>(nullable: true),
                    CONTAINER_STATUS = table.Column<string>(nullable: true),
                    CONTAINER_HISTORY_DATE = table.Column<string>(nullable: true),
                    IS_PROCESSED = table.Column<bool>(nullable: true),
                    PROCESSING_COMMENTS = table.Column<string>(nullable: true),
                    CONTAINER_LINE_CODE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDIMaerskMessageTOS", x => x.EDIMaerskMessageTOSId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EDIMaerskMessageTOS");
        }
    }
}
