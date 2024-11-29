using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class ExportDeliveryOrderViewModel : CommonProperties
    {
        public ExportDeliveryOrderViewModel()
        {
            DOItemExports = new List<DOItemExport>();
        }
        public long CargoReceivedId { get; set; }
        public long EnteringCargoId { get; set; }
        public string DrayOffStatus { get; set; }
        public DateTime? GdDate { get; set; }
        public string GDNumber { get; set; }
        public string InvoiceNo { get; set; }
        public string ClearingAgent { get; set; }
        public string ChallanNumber { get; set; }
        public string Shipper { get; set; }
        public int NoOfPackages { get; set; }
        public string Commodity { get; set; }
        public string AgentRepresentative { get; set; }
        public string AgentRepresentativeCNIC { get; set; }
        public int? TotalDelivered { get; set; }
        public int? BalancePackages { get; set; }
        public string Remarks { get; set; }
        public string TruckNumber { get; set; }
        public int NumberOfPackges { get; set; }
        public string PackgesType { get; set; }
        public string Status { get; set; }
        public long ExportDeliveryOrderId { get; set; }
        public long GatePassExportId { get; set; }
        public long DOItemExportId { get; set; }
        public List<DOItemExport> DOItemExports { get; set; }


    }
}