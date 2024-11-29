using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CRL")]
    public class CRL : CommonProperties
    {
        public long CRLId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string Category { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string Status { get; set; }

        public DateTime? Performed { get; set; }

        public string DocumentCodes  { get; set; }

        public string GDNumber{ get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
