using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyGateOutContainer")]
    public class EmptyGateOutContainer : CommonProperties
    {
        public long EmptyGateOutContainerId { get; set; }
        public string VehicleNumber { get; set; }
        //public string TransportorName { get; set; }
        public string DeliveredYard { get; set; }
        public DateTime? DeliveredYardDate { get; set; }
        public string ContainerNo { get; set; }
        public string VirNo { get; set; }
        public string ContainerCondition { get; set; }
        public DateTime? CreatedDate { get; set; }         
        public long? ContainerIndexId { get; set; }
        public ContainerIndex ContainerIndex { get; set; }

        public long? CYContainerId { get; set; }
        public CYContainer CYContainer { get; set; }

        public string Type { get; set; }

        public long? TransporterId { get; set; }
        public Transporter Transporter { get; set; }

        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        public string   ShippingLine { get; set; }
    }
}
