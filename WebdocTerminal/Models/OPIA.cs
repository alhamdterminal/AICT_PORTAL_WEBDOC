using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OPIA : CommonProperties
    {
        public long OPIAId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string GDNumber { get; set; }

        public string VehicleNo { get; set; }

        public string ContainerNo { get; set; }

        public double NoOfPackages { get; set; }

        public string PackageType { get; set; }

        public string ConsignorName { get; set; }

        public string ConsignorNTN { get; set; }

        public string ConsignorAddress { get; set; }

        public string CongisneeName { get; set; }

        public string CongisneeAddress { get; set; }

        public string ClearingAgentChallanNumber { get; set; }

        public string ClearingAgentName { get; set; }

        public double GrossWeight { get; set; }

        public string OperationType { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }
    }
}
