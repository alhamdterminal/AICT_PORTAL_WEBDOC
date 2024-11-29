using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ScheduleOfChargesViewModel
    {
        public ScheduleOfChargesViewModel()
        {
            InvoiceItems = new List<InvoiceItem>(); 
        }
        public string LineName { get; set; }
        public string VesselName { get; set; }
        public string Consignee { get; set; }
        public string NTNNo { get; set; }
        public string ClearingAgent { get; set; }
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
        public string notnot { get; set; }
        public string ASDASD { get; set; }
        public string BillEnteryNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ArriveDate { get; set; }
        public DateTime? IGMDate { get; set; }
        public double WaiverAmount { get; set; }
        public int StorageDays { get; set; }
        public double StorageAmount { get; set; }
        public double OtherChargeAmount { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
        public string Igmodesc { get; set; }
        public string TariffName { get; set; }
        public double Amount { get; set; }
        public double AfterWaiverAmount { get; set; }
        public double TotalCharges { get; set; }
        public double SalesTax { get; set; }
        public double SalesTaxAmount { get; set; }
        public double NetCharges { get; set; }



    }
}
