using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Invoice")]
    public class Invoice : CommonProperties
    {
        public Invoice()
        {
            InvoiceItems = new List<InvoiceItem>();
            AmountReceiveds = new List<AmountReceived>();
            InvoiceDocuments = new List<InvoiceDocument>();
        }

        public long InvoiceId { get; set; }

        public string InvoiceNo { get; set; }
         public string Year { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public DateTime? GateInDate { get; set; }

        public DateTime? DestuffDate { get; set; }

         
        public double GrandTotal { get; set; }
        public double ActualTariffCharges { get; set; }
        public double OtherChargeAmount { get; set; }
        public double PortChargeAmount { get; set; }
        public double SealCharger { get; set; }
        public double CYStorageSizeAmount { get; set; }

        public double TotalAmount { get; set; }
        public int BalanceAmount { get; set; }
        public double BalanceCargo { get; set; }
        public double TotalCharges { get; set; }
        public double BalanceAmountTotal { get; set; }
   
        public string Type { get; set; }
        public bool InvoiceNatureType { get; set; }
        public string BillToLineNumber { get; set; }
  
        public double TariffAmountTotal { get; set; }

        public double StorageTotal { get; set; }
        public double BillToLineAmount { get; set; }
        public double WaiverDiscountAmount { get; set; }
        public double VehicleCharges { get; set; }
        public double HandingCharges { get; set; }
        public double WeightmentCharges { get; set; }
        public long AuctionSalesTax { get; set; }
        public long AuctionGrandTotal { get; set; }
        public double ExchangeRateAmount { get; set; }

        public double CBM { get; set; }

        public double Weight { get; set; }

        public int Size { get; set; }
      

        public int StorageDays { get; set; } 
        public int? AdditionalFreeDays { get; set; } 
        public int? TotalFreeDays { get; set; } 

        public bool IsAdvanceBill { get; set; }         

        public bool IsCancelled { get; set; }

        public DateTime? AdvanceDate { get; set; }

        public string CNIC { get; set; }

        public string PhoneNumber { get; set; }
        public string CargoType { get; set; }

        public long DeliveredStorage { get; set; }

        public string ClearingAgentRep { get; set; }
        public string StorageApplicableon { get; set; }

        public long? ClearingAgentId { get; set; }

        public ClearingAgent ClearingAgent { get; set; }

        public string ClearingAgentNTN { get; set; }

        public string ConsigneeName { get; set; }
        public string ConsigneeNTN { get; set; }
        public int SalesTax { get; set; } 
        public double PreviousBillAmount { get; set; } 
        public double AfterSalesTax { get; set; } 
        public string BillType { get; set; } 
        public string PartContainer { get; set; } 
        public long ExcessAmount { get; set; }
        public string KnockOffInvoiceNumber { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceCategory { get; set; }
        public long IsLetPass { get; set; }
        public string PerfomaReceiptNumber { get; set; }
        public bool IsAdjust { get; set; }
        public bool IsLineIndexRate { get; set; }
        public double IGMCBM { get; set; }
        public double DestuffCBM { get; set; }
        public double ExaminationCBM { get; set; }
        public double FFCBM { get; set; }
        public double ActualWT { get; set; }
        public double MFTWT { get; set; }
        public long FFStorageShare { get; set; }
        public double AictPerBoxRate { get; set; }
        public long TotalArriveIndexes { get; set; }
         
        public long? ContainerIndexId { get; set; }
        public ContainerIndex ContainerIndex { get; set; }

        public long? CYContainerId { get; set; }
        public CYContainer CYContainer { get; set; }


        public long? TariffInformationId { get; set; }
        public TariffInformation TariffInformation { get; set; }


        public List<InvoiceItem> InvoiceItems { get; set; }

        public List<AmountReceived> AmountReceiveds { get; set; }

        public List<InvoiceDocument> InvoiceDocuments { get; set; }

    }
}
