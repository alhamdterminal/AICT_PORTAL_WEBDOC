using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OVAM : CommonProperties
    {
        public long OVAMId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIR_AIRNumber { get; set; }

        public string ActivityType { get; set; }

        public DateTime? ActivityTime  { get; set; }

        public DateTime? Performed { get; set; }

        public string OperationType { get; set; }

        public string ContainerNumber { get; set; }

        public string FileName { get; set; }


    }
}
