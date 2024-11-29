using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class GateInViewModel
    {
        public long   ExportContainerId { get; set; }
        public string GDNumber { get; set; }

        public string VehicleNo { get; set; }

        public string ContainerNo { get; set; }

        public int? NoOfPackages { get; set; }

        public string PackageType { get; set; }

        public string ConsignorName { get; set; }

        public string ConsignorNTN { get; set; }

        public string ConsignorAddress { get; set; }

        public string CongisneeName { get; set; }

        public string CongisneeAddress { get; set; }

        public string ClearingAgentChallanNumber { get; set; }

        public string ClearingAgentName { get; set; }

        public double GrossWeight { get; set; }

        public bool? IsGateIn { get; set; }

        public DateTime? GateInDateTime { get; set; }

    }
}
