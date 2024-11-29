using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OGTE : CommonProperties
    {
        public long OGTEId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string ContainerNumber { get; set; }

        public string GDNumber { get; set; }

        public string Status { get; set; }

        public DateTime? GateOutTime { get; set; }

        public string VehicleNumber { get; set; }

        public string PCCSSSeal { get; set; }

        public string BondedCarrierNTN { get; set; }

        public string PortofExit { get; set; }

        public string CountryofExit { get; set; }

        public double ContainerGrossWeight { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string LocationCode { get; set; }

        public string FileName { get; set; }

        public bool IsProcessed { get; set; } = false;


    }
}
