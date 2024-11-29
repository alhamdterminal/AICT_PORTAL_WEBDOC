using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GIIO")]
    public class GIIO : CommonProperties
    {
        public long GIIOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string PCCSSSealNumber { get; set; }

        
        [Column(TypeName = "decimal(18, 4)")]
        public double? Weight { get; set; }


        public DateTime? GateInTime { get; set; }

        public string VehicleNumber { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public bool IsProcessed { get; set; } = false;

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
