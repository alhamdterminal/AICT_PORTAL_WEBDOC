using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OCRL : CommonProperties
    {
        public long OCRLId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string Category { get; set; }

        public string GDNumber { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public int? IndexNo { get; set; }

        public string Status { get; set; }

        public DateTime? Performed { get; set; }

        public string DocumentCodes { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }



    }
}
