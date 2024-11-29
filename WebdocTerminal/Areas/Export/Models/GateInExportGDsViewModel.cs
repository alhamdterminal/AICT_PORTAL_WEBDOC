using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class GateInExportGDsViewModel
    {
        public string ContainerNo { get; set; }
        public string Type { get; set; }
        public string GDNumber { get; set; }
        public string VehicleNo { get; set; }
        public double GrossWeight { get; set; }
        public int NoOfPackages { get; set; }
        public string PackageType { get; set; }
        public string ClearingAgentName { get; set; }
        public double DeliveredGrossWeight { get; set; }
        public int DeliveredNoOfPackages { get; set; }
        public double RemainingGrossWeight { get; set; }
        public int RemainingNoOfPackages { get; set; }
        public string GateInStatus { get; set; }
        public string MessageFrom { get; set; }
        public bool? IsGateIn { get; set; }
        public DateTime? GateInDateTime { get; set; }
    }
}
