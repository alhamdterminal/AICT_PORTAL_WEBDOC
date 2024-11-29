using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("SCMO")]
    public class SCMO : CommonProperties
    {
        public long SCMOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequenceNo { get; set; }

        public string Category { get; set; }

        public string VIRNo { get; set; }

        public string BLNo { get; set; }

        public int IndexNo { get; set; }

        public string ContainerNo { get; set; }

        public double? Weight { get; set; }

        public double? NoOfPackages { get; set; }

        public string OpType { get; set; }

        public DateTime? Performed { get; set; }

        public string TpNo { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public bool IsProcessed { get; set; } = false;

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
