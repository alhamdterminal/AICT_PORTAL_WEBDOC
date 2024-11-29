using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("InvoiceExport")]
    public class InvoiceExport : CommonProperties
    {

        public InvoiceExport()
        {
            InvoiceItemExports = new List<InvoiceItemExport>();
            AmountReceivedExports = new List<AmountReceivedExport>();
            InvoiceDocumentExports = new List<InvoiceDocumentExport>();
        }

        public long InvoiceExportId { get; set; }

        public string InvoiceNo { get; set; }
        public string Year { get; set; }
        public long WaiverAmount { get; set; }

        public long PreviousBillNo { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? GateInDate { get; set; }

        public DateTime? DestuffDate { get; set; }

        public string Consignee { get; set; }
        public string ConsigneeNTN { get; set; }

        public double GrandTotal { get; set; }
        public double BWTotal { get; set; }
        public double FFTotal { get; set; }
        public int TotalAmount { get; set; }
        public double AfterDiscount { get; set; }

        public int PaidAmount { get; set; }
        public double BalanceAmountTotal { get; set; }
        public double PreviousBillAmount { get; set; }
        public double AfterSalesTax { get; set; }
        public int BalanceAmount { get; set; }

        public int? AdditionalFreeDays { get; set; }
        public double TariffAmountTotal { get; set; }
        public string InvoiceType { get; set; }

        public string Type { get; set; }

        public string OtherChargeName { get; set; }

        public int OtherChargeAmount { get; set; }

        public int Discount { get; set; }

        public string Remarks { get; set; }

        public int SizeTotal { get; set; }

        public double CBMTotal { get; set; }

        public double IndexTotal { get; set; }

        public int StorageTotal { get; set; }

        public double? CBM { get; set; }

        public int Weight { get; set; }

        public int Size { get; set; }

        public int StorageDays { get; set; }

        public bool IsAdvanceBill { get; set; }

        public bool IsSubBill { get; set; }

        public bool IsCancelled { get; set; }
        public bool IsAmountReceived { get; set; }
        public bool IsPaymentCollectAllow { get; set; }

        public DateTime? AdvanceDate { get; set; }

        public long IsPartialDelivery { get; set; }

        public string ImageUrl { get; set; }

        public int SalesTax { get; set; }
        public double TotalCharges { get; set; }
        public string CNIC { get; set; }

        public string PhoneNumber { get; set; }


        public long? AtTimeOfInvoiceVesselExportId { get; set; }

        public long? VesselExportId { get; set; }

        public VesselExport VesselExport { get; set; }

        public long? AtTimeOfInvoiceVoyageExportId { get; set; }
        public long? VoyageExportId { get; set; }

        public VoyageExport VoyageExport { get; set; }


        public string ClearingAgentRep { get; set; }

        public long? ClearingAgentExportId { get; set; }

        public ClearingAgentExport ClearingAgentExport { get; set; }

        public long? EnteringCargoId { get; set; }

        public EnteringCargo EnteringCargo { get; set; }

        public string ShippingAgent { get; set; }

        public string ContainerNo { get; set; }

        public string ContainerSize { get; set; }

        public List<InvoiceItemExport> InvoiceItemExports { get; set; }

        public List<AmountReceivedExport> AmountReceivedExports { get; set; }

        public List<InvoiceDocumentExport> InvoiceDocumentExports { get; set; }

    }
}
