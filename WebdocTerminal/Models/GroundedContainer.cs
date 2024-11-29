using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GroundedContainer")]
    public class GroundedContainer : CommonProperties
    {
        public long GroundedContainerId { get; set; }

        public string HandlingCode { get; set; }

        public  double? Weight { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }

        public string ActivityType { get; set; }

        public string Category { get; set; }

        public DateTime? GroundedDate { get; set; }

        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CyContainerId { get; set; }

        public CYContainer CYContainer { get; set; }
    }
}
