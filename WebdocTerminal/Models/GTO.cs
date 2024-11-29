using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GTO")]
    public class GTO : CommonProperties
    {
        public long GTOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string Category { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public DateTime? GateOutDate { get; set; }

        public string Status { get; set; }

        public string TruckNumber{ get; set; }

        public string SealNumber { get; set; }

        public string BondedCarrier { get; set; }

        public string PortofExit { get; set; }

        public string Country { get; set; }

        public double? GrossWeight { get; set; }

        public string FileName { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public bool IsProcessed { get; set; } = false;

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
