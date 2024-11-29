using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GTOO")]
    public class GTOO : CommonProperties
    {
        public long GTOOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string Category { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string BLNumber { get; set; }

        public decimal? NumberofPackages { get; set; }

        public string PackageType	 { get; set; }

        public string Status { get; set; }

        public DateTime? GateOutTime { get; set; }

        public string Truck { get; set; }

        public string PCCSSSealNo { get; set; }

        public string BondedCarrierNTN { get; set; }

        public string PortOfExit { get; set; }

        public string CountryofExit { get; set; }

        public double? Weight { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public bool IsProcessed { get; set; } = false;

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
