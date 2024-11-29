using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update543 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EDI_Tradelens_APIResponse",
                columns: table => new
                {
                    EDI_Tradelens_APIResponseId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    message = table.Column<string>(nullable: true),
                    messageId = table.Column<long>(nullable: false),
                    eventTransactionId = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    timestamp = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDI_Tradelens_APIResponse", x => x.EDI_Tradelens_APIResponseId);
                });

            migrationBuilder.CreateTable(
                name: "EDI_Tradelens_Lookup",
                columns: table => new
                {
                    EDI_Tradelens_LookupId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyPoint = table.Column<DateTime>(nullable: true),
                    CopyToMearskEnabled = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDI_Tradelens_Lookup", x => x.EDI_Tradelens_LookupId);
                });

            migrationBuilder.CreateTable(
                name: "EDI_Tradelens_Message",
                columns: table => new
                {
                    EDI_Tradelens_MessageId = table.Column<long>(nullable: false)
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
                    CONTAINER_LINE_CODE = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDI_Tradelens_Message", x => x.EDI_Tradelens_MessageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EDI_Tradelens_APIResponse");

            migrationBuilder.DropTable(
                name: "EDI_Tradelens_Lookup");

            migrationBuilder.DropTable(
                name: "EDI_Tradelens_Message");
        }
    }
}
