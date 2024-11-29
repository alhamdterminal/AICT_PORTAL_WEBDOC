using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class GroundingStatusViewModel : CommonProperties
    {
        public long ContainerIndexId { get; set; }

        public long CyContainerId { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public string ContainerNumber { get; set; }

        public int? IndexNumber { get; set; }

        public string Location { get; set; }

        public string HandlingCode { get; set; }

        public string ServiceStatus { get; set; }

        public string ActivityStatus { get; set; }
        public DateTime? Performed { get; set; }
    }
}
