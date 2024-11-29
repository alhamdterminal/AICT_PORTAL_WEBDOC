using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ECMO")]
    public class ECMO : CommonProperties
    {
        public long ECMOId { get; set; }

        public int? TotalRecords  { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public int? IndexNumber { get; set; }

        public string HandlingCode { get; set; }

        public string ServiceStatus { get; set; }

        public string Remarks { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
