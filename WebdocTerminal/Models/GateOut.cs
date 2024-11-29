using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GateOut")]
    public class GateOut : CommonProperties
    {
        public long GateOutId { get; set; }

        public DateTime? DeliverDate { get; set; }

        public string ShiftedYard { get; set; }

        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CYContainerId { get; set; }

        public CYContainer CYContainer { get; set; }

        //public long? ShippingAgentId { get; set; }

        //public ShippingAgent ShippingAgent { get; set; }

        public string TruckNo { get; set; }
    }
}
