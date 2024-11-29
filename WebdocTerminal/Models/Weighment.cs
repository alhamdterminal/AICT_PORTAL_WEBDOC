using System;
using DevExpress.Xpo;

namespace WebdocTerminal.Models
{

    public class Weighment : CommonProperties
    {
        public long WeighmentId { get; set; }

        public double? GrossWeight { get; set; }

        public double? FoundWeight { get; set; }

        public long? ContainerIndexId { get; set; }

        public string HandlingCode { get; set; }

        public DateTime? WeighmentDate { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CyContainerId { get; set; }

        public CYContainer CYContainer { get; set; }
    }

}