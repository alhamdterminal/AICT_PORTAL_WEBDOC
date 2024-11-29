using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class EHC : CommonProperties
    {
        public long EHCId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string ContainerNo { get; set; }

        public string HandlingCode { get; set; }

        public string ExecutionOrder { get; set; }

        public DateTime? Performed { get; set; }

        public string FileName { get; set; }

    }
}
