using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update697 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CSEmptyContainerReceive",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    CSEmptyContainerReceiveId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContainerNo = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    ISOCodeHeadId = table.Column<long>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    ConsigneId = table.Column<long>(nullable: true),
                    ClearingAgentId = table.Column<long>(nullable: true),
                    ReceivedDate = table.Column<DateTime>(nullable: false),
                    TrukNo = table.Column<string>(nullable: true),
                    TransporterId = table.Column<long>(nullable: true),
                    DamageType = table.Column<string>(nullable: true),
                    OtherRemarks = table.Column<string>(nullable: true),
                    OutDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSEmptyContainerReceive", x => x.CSEmptyContainerReceiveId);
                    table.ForeignKey(
                        name: "FK_CSEmptyContainerReceive_ClearingAgent_ClearingAgentId",
                        column: x => x.ClearingAgentId,
                        principalTable: "ClearingAgent",
                        principalColumn: "ClearingAgentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CSEmptyContainerReceive_Consigne_ConsigneId",
                        column: x => x.ConsigneId,
                        principalTable: "Consigne",
                        principalColumn: "ConsigneId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CSEmptyContainerReceive_ISOCodeHead_ISOCodeHeadId",
                        column: x => x.ISOCodeHeadId,
                        principalTable: "ISOCodeHead",
                        principalColumn: "ISOCodeHeadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CSEmptyContainerReceive_Transporter_TransporterId",
                        column: x => x.TransporterId,
                        principalTable: "Transporter",
                        principalColumn: "TransporterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CSEmptyContainerReceive_ClearingAgentId",
                table: "CSEmptyContainerReceive",
                column: "ClearingAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_CSEmptyContainerReceive_ConsigneId",
                table: "CSEmptyContainerReceive",
                column: "ConsigneId");

            migrationBuilder.CreateIndex(
                name: "IX_CSEmptyContainerReceive_ISOCodeHeadId",
                table: "CSEmptyContainerReceive",
                column: "ISOCodeHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_CSEmptyContainerReceive_TransporterId",
                table: "CSEmptyContainerReceive",
                column: "TransporterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CSEmptyContainerReceive");
        }
    }
}
