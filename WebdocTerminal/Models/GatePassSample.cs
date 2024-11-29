using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GatePassSample")]
    public class GatePassSample : CommonProperties
    {
        public long GatePassSampleId { get; set; }
        public DateTime GatePassDate { get; set; }
        public long GatePassNumber { get; set; }
        public string DetailOfSample { get; set; }
        public string AgentRepresentative { get; set; }
        public string ReceivedBy { get; set; }
        public string ExaminedBy { get; set; }
        public DateTime ExaminedDate { get; set; }
        public long? ContainerIndexId { get; set; }
        public ContainerIndex ContainerIndex { get; set; }
        public long? CYContainerId { get; set; }
        public CYContainer CYContainer { get; set; }
    }
}
