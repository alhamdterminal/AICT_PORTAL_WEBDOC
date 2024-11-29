using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ScheduleOfCharge")]
    public class ScheduleOfCharge : CommonProperties
    {
        public ScheduleOfCharge()
        {
            ScheduleOfChargeItems = new List<ScheduleOfChargeItem>();
        }

        public long ScheduleOfChargeId { get; set; }
        public string LineName { get; set; }
        public string Principal { get; set; }
        public string VesselName { get; set; }
        public string Consignee { get; set; }
        public string NTNNo { get; set; }
        public string ClearingAgent { get; set; }
        public string Commodity { get; set; }
        public string ContainerNo { get; set; }
        public int Size { get; set; }
        public double ManifestedM3 { get; set; }
        public double? ManifestedWt { get; set; }
        public double CYStorageSizeAmount { get; set; }
        public string VoyageNo { get; set; }
        public string STNNo { get; set; }
        public string CYCFS { get; set; }
        public double ActualM3 { get; set; }
        public double? ActualWt { get; set; }
        public double BLM3 { get; set; }
        public string Indexno { get; set; }
        public string IGMNo { get; set; }
        public string BLNo { get; set; }
        public string BillEnteryNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ArriveDate { get; set; }
        public DateTime? IGMDate { get; set; }
        public double WaiverAmount { get; set; }
        public int StorageDays { get; set; }
        public double StorageAmount { get; set; }
        public double OtherChargeAmount { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? AdvanceDate { get; set; }
        public List<ScheduleOfChargeItem> ScheduleOfChargeItems { get; set; }
        public string Igmodesc { get; set; }
        public string TariffName { get; set; }
        public double Amount { get; set; }
        public double AfterWaiverAmount { get; set; }
        public double TotalCharges { get; set; }
        public double SalesTax { get; set; }
        public double SalesTaxAmount { get; set; }
        public double NetCharges { get; set; }

        public long? TariffInformationId { get; set; }
        public TariffInformation TariffInformation { get; set; }

    }
}
