using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PGO")]
    public class PGO : CommonProperties
    {
        public long PGOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string VehicleNumber { get; set; }

        public string BondedCarrierId { get; set; }

        public string BondedCarrierName { get; set; }

        public DateTime? Performed { get; set; }

        public string OpType { get; set; }

        public string FileName { get; set; }

        public string MessageTo { get; set; }
        public bool IsProcessed { get; set; } = false;
        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
