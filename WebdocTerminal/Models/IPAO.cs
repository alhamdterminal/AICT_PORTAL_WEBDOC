using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class IPAO : CommonProperties
    {
        public long IPAOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string BondedCarrierId { get; set; }

        public string BondedCarrierName { get; set; }

        public string VehicleNumber { get; set; }

        public DateTime? OutTime { get; set; }

        public string PCCSSSealNumber { get; set; }

        public string PortOfEntry { get; set; }

        public string OpType { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
