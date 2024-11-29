using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("VIRO")]
    public class VIRO : CommonProperties
    {
        public long VIROId { get; set; }

        public int? TotalRecords { get; set; }

        public int?  RecordSequence { get; set; }

        public string  VIRNumber { get; set; }

        public string VesselName { get; set; }

        public string Voyage { get; set; }

        public DateTime? BerthOn { get; set; }

        public string  BerthAt { get; set; }

        public string OPType { get; set; }

        public  DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
