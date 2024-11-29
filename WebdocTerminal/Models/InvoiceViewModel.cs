using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class InvoiceViewModel : CommonProperties
    {
        public long EnteringCargoId { get; set; }

        public long ContainerIndexId { get; set; }

        public long CyContainerId { get; set; }

        public string ContainerNo { get; set; }

        public string ContainerSize { get; set; }
        public string TotalBalanceCargo { get; set; }

        public string GDNumber { get; set; }

        public long InvoiceId { get; set; }

        public long InvoiceExportId { get; set; }

        public string BillNo { get; set; }
        public long? BillNoExport { get; set; }

        public string ShippingAgnet { get; set; }
        public string VehcileAmountAllow { get; set; }
        public string ConsigneeNameIGM { get; set; }
        public string Good { get; set; }
        public string Shipper { get; set; }
        public string PortAndTerminal { get; set; }
        public string IndexCargoType { get; set; }
        public string BillToLineNumber { get; set; }
        public double WaiVerAmount { get; set; }
        public int? SalesTax { get; set; }
        public double? PreviousBillAmount { get; set; }
        public double PreviousBillTotalCharges { get; set; }
        public long? SealChargers { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? DestuffDate { get; set; }

        public DateTime? GateInDate { get; set; }

        public DateTime? AdvanceDate { get; set; }


        public long ClearingAgentId { get; set; }
        public string ISOCodeDesc { get; set; }
        public string DocumentCode { get; set; }
        public string Description { get; set; }

        public long ClearingAgentExportId { get; set; }

        public string Consignee { get; set; }
        public double CreditAmount { get; set; }

        public string ConsigneeNTN { get; set; }

        public int StorageDays { get; set; }

        public int AdditionalFreeDays { get; set; }

        public double OtherCharges { get; set; }
        public double PortCharges { get; set; }

        public string ChargesName { get; set; }

        public double? Weight { get; set; }

        public string IsPartialDelivery { get; set; }
        public bool IsDelivered { get; set; }
        public string DeliveryStatus { get; set; }
        public string PartContainer { get; set; }

        public bool IsAdvanceBill { get; set; }
        public bool IsSocCreated { get; set; }
        public bool IsLinkedWithFF { get; set; }

        public bool IsSubBill { get; set; }

        public double? CBM { get; set; }
        public double? MeasurementCBM { get; set; }
        public double? ExaminationRequestCBM { get; set; }
        public double? FFCBM { get; set; }
        public double? HigherCBM { get; set; }

        public int Size { get; set; }

        public int SubTotal { get; set; }

        public int TotalAmount { get; set; }

        public string ClearingAgentGDIO { get; set; }

        public double StorageTotal { get; set; }

        public int GrandTotal { get; set; }

        public string IGM { get; set; }

        public string PackageType { get; set; }
        public int? NoOfPackages { get; set; }

        public string DishargePort { get; set; }
        public string ShippingAgentName { get; set; }
        public long ShippingAgentId { get; set; }
        public string ConsigneeNTNNumber { get; set; }
        public string ClearingAgentRegNumber { get; set; }
        public int ActualQty { get; set; }
        public string BlQty { get; set; }
        public double? ActualWeight { get; set; }
        public string Packages { get; set; }
        public string BLNumber { get; set; }
        public string CNIC { get; set; }
        public string PhoneNumber { get; set; }
        public string ClearingAgentRep { get; set; }
        public string LastWaiverRemarks { get; set; }
        public double PreviousWaiVerAmount { get; set; }
        public double ExchangeRateAmount { get; set; }
        public double ShortWeight { get; set; }
        public double ExcesstWeight { get; set; }
        public long TotalArriveIndexes { get; set; }

        public double PreviousPaidGST { get; set; }
        public double PreviousPaidBillToLineAmount { get; set; }
        public double PreviousPaidBillToLineAmountGST { get; set; }
        public double PreviousPaidBillToLineAmountGrandTotal { get; set; }


    }
}
