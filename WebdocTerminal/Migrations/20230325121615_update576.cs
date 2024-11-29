using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebdocTerminal.Migrations
{
    public partial class update576 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoReceived_LoadingProgram_LoadingProgramId",
                table: "CargoReceived");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportCargo_LoadingProgram_LoadingProgramId",
                table: "ExportCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ClearingAgentExport_ClearingAgentExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingAgentExport_ShippingAgentExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_VesselExport_VesselExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgram_VoyageExport_VoyageExportId",
                table: "LoadingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgramDetail_LoadingProgram_LoadingProgramId",
                table: "LoadingProgramDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TPCargoDetails_LoadingProgram_LoadingProgramId",
                table: "TPCargoDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoadingProgram",
                table: "LoadingProgram");

            migrationBuilder.RenameTable(
                name: "LoadingProgram",
                newName: "LoadingPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_VoyageExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_VoyageExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_VesselExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_VesselExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ShippingLineId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ShippingLineId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ShippingAgentExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ShippingAgentExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ShipperId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ShipperId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_PortAndTerminalId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_PortAndTerminalId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingProgram_ClearingAgentExportId",
                table: "LoadingPrograms",
                newName: "IX_LoadingPrograms_ClearingAgentExportId");

            migrationBuilder.AddColumn<string>(
                name: "TariffHeadExportType",
                table: "TariffHeadExport",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CBMMultiplyValue",
                table: "TariffExport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCBMorRate",
                table: "TariffExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NatureOfTariffId",
                table: "TariffExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TariffType",
                table: "TariffExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImplementFrom",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImplementTo",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NatureOfTariffId",
                table: "ShippingAgentExport",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KnockOfInvoice",
                table: "RefundAmountExport",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "BillNo",
                table: "PartyLedgerExport",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CBM",
                table: "LoadingProgramDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "LoadingProgramDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "LoadingProgramDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PLIDNumber",
                table: "LoadingProgramDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseLocation",
                table: "LoadingProgramDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "LoadingProgramDetail",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "InvoiceItemExport",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InvoiceItemExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NatureOfTariff",
                table: "InvoiceItemExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TariffExportId",
                table: "InvoiceItemExport",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "InvoiceExport",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "IndexTotal",
                table: "InvoiceExport",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "GrandTotal",
                table: "InvoiceExport",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "CBMTotal",
                table: "InvoiceExport",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AdditionalFreeDays",
                table: "InvoiceExport",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AfterDiscount",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AfterSalesTax",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "AtTimeOfInvoiceVesselExportId",
                table: "InvoiceExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AtTimeOfInvoiceVoyageExportId",
                table: "InvoiceExport",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BWTotal",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BalanceAmountTotal",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ConsigneeNTN",
                table: "InvoiceExport",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FFTotal",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAmountReceived",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaymentCollectAllow",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PreviousBillAmount",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TariffAmountTotal",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCharges",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "VesselExportId",
                table: "InvoiceExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VoyageExportId",
                table: "InvoiceExport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WaiverAmount",
                table: "InvoiceExport",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "InvoiceExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GDNumber",
                table: "GatePassExport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GDNumber",
                table: "ExportDeliveryOrders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeivered",
                table: "ExportDeliveryOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContainerCondition",
                table: "ExportContainers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ContainerTareWeight",
                table: "ExportContainers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "POD",
                table: "ExportContainers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecevingDate",
                table: "ExportContainers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShippingAgentExportId",
                table: "ExportContainers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerNumber",
                table: "ExportContainerItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ExportContainerItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GateOutDate",
                table: "ExportContainerItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGateOut",
                table: "ExportContainerItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOvams",
                table: "ExportContainerItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShipperName",
                table: "ExportContainerItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitedDate",
                table: "ExportContainerItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "isAmountReceived",
                table: "ExportContainerItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalFreeDays",
                table: "EnteringCargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "EnteringCargo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraFreeDaysRemarks",
                table: "EnteringCargo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "EnteringCargo",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LoadingProgramId",
                table: "EnteringCargo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TRNumber",
                table: "EnteringCargo",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WaiverAmount",
                table: "EnteringCargo",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CLearingAgentReprsentative",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoRecevingCondition",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClearingAgentCNIC",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommodityName",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverCNIC",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinalDestination",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GDNumber",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GateInDate",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NatureOfTariffId",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecevingEndTime",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecevingStartTime",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipperSeal",
                table: "LoadingPrograms",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoadingPrograms",
                table: "LoadingPrograms",
                column: "LoadingProgramId");

            migrationBuilder.CreateTable(
                name: "DisabledAgentTariffExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DisabledAgentTariffExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnteringCargoId = table.Column<long>(nullable: true),
                    ShippingAgentTariffExportId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabledAgentTariffExport", x => x.DisabledAgentTariffExportId);
                    table.ForeignKey(
                        name: "FK_DisabledAgentTariffExport_EnteringCargo_EnteringCargoId",
                        column: x => x.EnteringCargoId,
                        principalTable: "EnteringCargo",
                        principalColumn: "EnteringCargoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisabledAgentTariffExport_ShippingAgentTariffExport_ShippingAgentTariffExportId",
                        column: x => x.ShippingAgentTariffExportId,
                        principalTable: "ShippingAgentTariffExport",
                        principalColumn: "ShippingAgentTariffExportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DOItemExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DOItemExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    TruckNumber = table.Column<string>(nullable: true),
                    PackageType = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    NumberOfPackage = table.Column<int>(nullable: false),
                    GatePassExportId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOItemExport", x => x.DOItemExportId);
                    table.ForeignKey(
                        name: "FK_DOItemExport_GatePassExport_GatePassExportId",
                        column: x => x.GatePassExportId,
                        principalTable: "GatePassExport",
                        principalColumn: "GatePassExportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NatureOfTariff",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    NatureOfTariffId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NatureOfTariffName = table.Column<string>(nullable: true),
                    TariffType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureOfTariff", x => x.NatureOfTariffId);
                });

            migrationBuilder.CreateTable(
                name: "PortAndTerminalExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    PortAndTerminalExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PortName = table.Column<string>(nullable: true),
                    TerminalCode = table.Column<string>(nullable: true),
                    TerminalName = table.Column<string>(nullable: true),
                    PortOfDischarge = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortAndTerminalExport", x => x.PortAndTerminalExportId);
                });

            migrationBuilder.CreateTable(
                name: "ShipperExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ShipperExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShipperName = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    NTNNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperExport", x => x.ShipperExportId);
                });

            migrationBuilder.CreateTable(
                name: "ShippingLineExport",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    ShippingLineExportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShippingLineName = table.Column<string>(nullable: true),
                    NTNNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingLineExport", x => x.ShippingLineExportId);
                });

            migrationBuilder.CreateTable(
                name: "TariffRateSlabWise",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    TariffRateSlabWiseId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Rate = table.Column<long>(nullable: false),
                    FromCBM = table.Column<long>(nullable: false),
                    ToCBM = table.Column<long>(nullable: false),
                    TariffExportId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffRateSlabWise", x => x.TariffRateSlabWiseId);
                    table.ForeignKey(
                        name: "FK_TariffRateSlabWise_TariffExport_TariffExportId",
                        column: x => x.TariffExportId,
                        principalTable: "TariffExport",
                        principalColumn: "TariffExportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TariffExport_NatureOfTariffId",
                table: "TariffExport",
                column: "NatureOfTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgentExport_NatureOfTariffId",
                table: "ShippingAgentExport",
                column: "NatureOfTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemExport_TariffExportId",
                table: "InvoiceItemExport",
                column: "TariffExportId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceExport_VesselExportId",
                table: "InvoiceExport",
                column: "VesselExportId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceExport_VoyageExportId",
                table: "InvoiceExport",
                column: "VoyageExportId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportContainers_ShippingAgentExportId",
                table: "ExportContainers",
                column: "ShippingAgentExportId");

            migrationBuilder.CreateIndex(
                name: "IX_EnteringCargo_LoadingProgramId",
                table: "EnteringCargo",
                column: "LoadingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingPrograms_NatureOfTariffId",
                table: "LoadingPrograms",
                column: "NatureOfTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabledAgentTariffExport_EnteringCargoId",
                table: "DisabledAgentTariffExport",
                column: "EnteringCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabledAgentTariffExport_ShippingAgentTariffExportId",
                table: "DisabledAgentTariffExport",
                column: "ShippingAgentTariffExportId");

            migrationBuilder.CreateIndex(
                name: "IX_DOItemExport_GatePassExportId",
                table: "DOItemExport",
                column: "GatePassExportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffRateSlabWise_TariffExportId",
                table: "TariffRateSlabWise",
                column: "TariffExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoReceived_LoadingPrograms_LoadingProgramId",
                table: "CargoReceived",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnteringCargo_LoadingPrograms_LoadingProgramId",
                table: "EnteringCargo",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportCargo_LoadingPrograms_LoadingProgramId",
                table: "ExportCargo",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportContainers_ShippingAgentExport_ShippingAgentExportId",
                table: "ExportContainers",
                column: "ShippingAgentExportId",
                principalTable: "ShippingAgentExport",
                principalColumn: "ShippingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceExport_VesselExport_VesselExportId",
                table: "InvoiceExport",
                column: "VesselExportId",
                principalTable: "VesselExport",
                principalColumn: "VesselExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceExport_VoyageExport_VoyageExportId",
                table: "InvoiceExport",
                column: "VoyageExportId",
                principalTable: "VoyageExport",
                principalColumn: "VoyageExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemExport_TariffExport_TariffExportId",
                table: "InvoiceItemExport",
                column: "TariffExportId",
                principalTable: "TariffExport",
                principalColumn: "TariffExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgramDetail_LoadingPrograms_LoadingProgramId",
                table: "LoadingProgramDetail",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_ClearingAgentExport_ClearingAgentExportId",
                table: "LoadingPrograms",
                column: "ClearingAgentExportId",
                principalTable: "ClearingAgentExport",
                principalColumn: "ClearingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_NatureOfTariff_NatureOfTariffId",
                table: "LoadingPrograms",
                column: "NatureOfTariffId",
                principalTable: "NatureOfTariff",
                principalColumn: "NatureOfTariffId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_PortAndTerminal_PortAndTerminalId",
                table: "LoadingPrograms",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_Shipper_ShipperId",
                table: "LoadingPrograms",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_ShippingAgentExport_ShippingAgentExportId",
                table: "LoadingPrograms",
                column: "ShippingAgentExportId",
                principalTable: "ShippingAgentExport",
                principalColumn: "ShippingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_ShippingLine_ShippingLineId",
                table: "LoadingPrograms",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_VesselExport_VesselExportId",
                table: "LoadingPrograms",
                column: "VesselExportId",
                principalTable: "VesselExport",
                principalColumn: "VesselExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingPrograms_VoyageExport_VoyageExportId",
                table: "LoadingPrograms",
                column: "VoyageExportId",
                principalTable: "VoyageExport",
                principalColumn: "VoyageExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAgentExport_NatureOfTariff_NatureOfTariffId",
                table: "ShippingAgentExport",
                column: "NatureOfTariffId",
                principalTable: "NatureOfTariff",
                principalColumn: "NatureOfTariffId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffExport_NatureOfTariff_NatureOfTariffId",
                table: "TariffExport",
                column: "NatureOfTariffId",
                principalTable: "NatureOfTariff",
                principalColumn: "NatureOfTariffId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TPCargoDetails_LoadingPrograms_LoadingProgramId",
                table: "TPCargoDetails",
                column: "LoadingProgramId",
                principalTable: "LoadingPrograms",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoReceived_LoadingPrograms_LoadingProgramId",
                table: "CargoReceived");

            migrationBuilder.DropForeignKey(
                name: "FK_EnteringCargo_LoadingPrograms_LoadingProgramId",
                table: "EnteringCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportCargo_LoadingPrograms_LoadingProgramId",
                table: "ExportCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportContainers_ShippingAgentExport_ShippingAgentExportId",
                table: "ExportContainers");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceExport_VesselExport_VesselExportId",
                table: "InvoiceExport");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceExport_VoyageExport_VoyageExportId",
                table: "InvoiceExport");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemExport_TariffExport_TariffExportId",
                table: "InvoiceItemExport");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingProgramDetail_LoadingPrograms_LoadingProgramId",
                table: "LoadingProgramDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_ClearingAgentExport_ClearingAgentExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_NatureOfTariff_NatureOfTariffId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_PortAndTerminal_PortAndTerminalId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_Shipper_ShipperId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_ShippingAgentExport_ShippingAgentExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_ShippingLine_ShippingLineId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_VesselExport_VesselExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadingPrograms_VoyageExport_VoyageExportId",
                table: "LoadingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAgentExport_NatureOfTariff_NatureOfTariffId",
                table: "ShippingAgentExport");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffExport_NatureOfTariff_NatureOfTariffId",
                table: "TariffExport");

            migrationBuilder.DropForeignKey(
                name: "FK_TPCargoDetails_LoadingPrograms_LoadingProgramId",
                table: "TPCargoDetails");

            migrationBuilder.DropTable(
                name: "DisabledAgentTariffExport");

            migrationBuilder.DropTable(
                name: "DOItemExport");

            migrationBuilder.DropTable(
                name: "NatureOfTariff");

            migrationBuilder.DropTable(
                name: "PortAndTerminalExport");

            migrationBuilder.DropTable(
                name: "ShipperExport");

            migrationBuilder.DropTable(
                name: "ShippingLineExport");

            migrationBuilder.DropTable(
                name: "TariffRateSlabWise");

            migrationBuilder.DropIndex(
                name: "IX_TariffExport_NatureOfTariffId",
                table: "TariffExport");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAgentExport_NatureOfTariffId",
                table: "ShippingAgentExport");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemExport_TariffExportId",
                table: "InvoiceItemExport");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceExport_VesselExportId",
                table: "InvoiceExport");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceExport_VoyageExportId",
                table: "InvoiceExport");

            migrationBuilder.DropIndex(
                name: "IX_ExportContainers_ShippingAgentExportId",
                table: "ExportContainers");

            migrationBuilder.DropIndex(
                name: "IX_EnteringCargo_LoadingProgramId",
                table: "EnteringCargo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoadingPrograms",
                table: "LoadingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_LoadingPrograms_NatureOfTariffId",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "TariffHeadExportType",
                table: "TariffHeadExport");

            migrationBuilder.DropColumn(
                name: "CBMMultiplyValue",
                table: "TariffExport");

            migrationBuilder.DropColumn(
                name: "IsCBMorRate",
                table: "TariffExport");

            migrationBuilder.DropColumn(
                name: "NatureOfTariffId",
                table: "TariffExport");

            migrationBuilder.DropColumn(
                name: "TariffType",
                table: "TariffExport");

            migrationBuilder.DropColumn(
                name: "ImplementFrom",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "ImplementTo",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "NatureOfTariffId",
                table: "ShippingAgentExport");

            migrationBuilder.DropColumn(
                name: "CBM",
                table: "LoadingProgramDetail");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "LoadingProgramDetail");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "LoadingProgramDetail");

            migrationBuilder.DropColumn(
                name: "PLIDNumber",
                table: "LoadingProgramDetail");

            migrationBuilder.DropColumn(
                name: "WarehouseLocation",
                table: "LoadingProgramDetail");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "LoadingProgramDetail");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "InvoiceItemExport");

            migrationBuilder.DropColumn(
                name: "NatureOfTariff",
                table: "InvoiceItemExport");

            migrationBuilder.DropColumn(
                name: "TariffExportId",
                table: "InvoiceItemExport");

            migrationBuilder.DropColumn(
                name: "AdditionalFreeDays",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "AfterDiscount",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "AfterSalesTax",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "AtTimeOfInvoiceVesselExportId",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "AtTimeOfInvoiceVoyageExportId",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "BWTotal",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "BalanceAmountTotal",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "ConsigneeNTN",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "FFTotal",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "IsAmountReceived",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "IsPaymentCollectAllow",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "PreviousBillAmount",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "TariffAmountTotal",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "TotalCharges",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "VesselExportId",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "VoyageExportId",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "WaiverAmount",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "InvoiceExport");

            migrationBuilder.DropColumn(
                name: "GDNumber",
                table: "GatePassExport");

            migrationBuilder.DropColumn(
                name: "GDNumber",
                table: "ExportDeliveryOrders");

            migrationBuilder.DropColumn(
                name: "IsDeivered",
                table: "ExportDeliveryOrders");

            migrationBuilder.DropColumn(
                name: "ContainerCondition",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "ContainerTareWeight",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "POD",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "RecevingDate",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "ShippingAgentExportId",
                table: "ExportContainers");

            migrationBuilder.DropColumn(
                name: "ContainerNumber",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "GateOutDate",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "IsGateOut",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "IsOvams",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "ShipperName",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "SubmitedDate",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "isAmountReceived",
                table: "ExportContainerItem");

            migrationBuilder.DropColumn(
                name: "AdditionalFreeDays",
                table: "EnteringCargo");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "EnteringCargo");

            migrationBuilder.DropColumn(
                name: "ExtraFreeDaysRemarks",
                table: "EnteringCargo");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "EnteringCargo");

            migrationBuilder.DropColumn(
                name: "LoadingProgramId",
                table: "EnteringCargo");

            migrationBuilder.DropColumn(
                name: "TRNumber",
                table: "EnteringCargo");

            migrationBuilder.DropColumn(
                name: "WaiverAmount",
                table: "EnteringCargo");

            migrationBuilder.DropColumn(
                name: "CLearingAgentReprsentative",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "CargoRecevingCondition",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "ClearingAgentCNIC",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "CommodityName",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "DriverCNIC",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "FinalDestination",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "GDNumber",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "GateInDate",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "NatureOfTariffId",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "RecevingEndTime",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "RecevingStartTime",
                table: "LoadingPrograms");

            migrationBuilder.DropColumn(
                name: "ShipperSeal",
                table: "LoadingPrograms");

            migrationBuilder.RenameTable(
                name: "LoadingPrograms",
                newName: "LoadingProgram");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_VoyageExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_VoyageExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_VesselExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_VesselExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ShippingLineId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ShippingLineId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ShippingAgentExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ShippingAgentExportId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ShipperId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ShipperId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_PortAndTerminalId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_PortAndTerminalId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadingPrograms_ClearingAgentExportId",
                table: "LoadingProgram",
                newName: "IX_LoadingProgram_ClearingAgentExportId");

            migrationBuilder.AlterColumn<long>(
                name: "KnockOfInvoice",
                table: "RefundAmountExport",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BillNo",
                table: "PartyLedgerExport",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "InvoiceItemExport",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<long>(
                name: "InvoiceNo",
                table: "InvoiceExport",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IndexTotal",
                table: "InvoiceExport",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "GrandTotal",
                table: "InvoiceExport",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "CBMTotal",
                table: "InvoiceExport",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoadingProgram",
                table: "LoadingProgram",
                column: "LoadingProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoReceived_LoadingProgram_LoadingProgramId",
                table: "CargoReceived",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportCargo_LoadingProgram_LoadingProgramId",
                table: "ExportCargo",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ClearingAgentExport_ClearingAgentExportId",
                table: "LoadingProgram",
                column: "ClearingAgentExportId",
                principalTable: "ClearingAgentExport",
                principalColumn: "ClearingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_PortAndTerminal_PortAndTerminalId",
                table: "LoadingProgram",
                column: "PortAndTerminalId",
                principalTable: "PortAndTerminal",
                principalColumn: "PortAndTerminalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_Shipper_ShipperId",
                table: "LoadingProgram",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingAgentExport_ShippingAgentExportId",
                table: "LoadingProgram",
                column: "ShippingAgentExportId",
                principalTable: "ShippingAgentExport",
                principalColumn: "ShippingAgentExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_ShippingLine_ShippingLineId",
                table: "LoadingProgram",
                column: "ShippingLineId",
                principalTable: "ShippingLine",
                principalColumn: "ShippingLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_VesselExport_VesselExportId",
                table: "LoadingProgram",
                column: "VesselExportId",
                principalTable: "VesselExport",
                principalColumn: "VesselExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgram_VoyageExport_VoyageExportId",
                table: "LoadingProgram",
                column: "VoyageExportId",
                principalTable: "VoyageExport",
                principalColumn: "VoyageExportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadingProgramDetail_LoadingProgram_LoadingProgramId",
                table: "LoadingProgramDetail",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TPCargoDetails_LoadingProgram_LoadingProgramId",
                table: "TPCargoDetails",
                column: "LoadingProgramId",
                principalTable: "LoadingProgram",
                principalColumn: "LoadingProgramId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
