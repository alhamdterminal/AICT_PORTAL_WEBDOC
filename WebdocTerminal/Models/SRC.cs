using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("SRC")]
    public class SRC : CommonProperties
    {
        public long SRCId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string Category { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string ActivityType { get; set; }

        public string Location { get; set; }

        public double? Weight { get; set; }

        public string HandlingCode { get; set; }

        public DateTime? Performed { get; set; }

        public string Status { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;

        public bool IsProcessed { get; set; } = false;

    }
}
