using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class ServiceCompletion : CommonProperties
    {
        public long ServiceCompletionId { get; set; }

        public string Category { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public string IndexNumber { get; set; }

        public string GDNumber { get; set; }

        public string ActivityType { get; set; }

        public string Location { get; set; }

        public int? Weight { get; set; }

        public string HandlingCode { get; set; }

        public DateTime? ServiceDate { get; set; }
         
    }
}
