using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class EmptyGateOutContainerViewModel
    {
        public long? Key { get; set; }
        public string Type { get; set; }
        public long? ContainerIndexId { get; set; }
        public long? CYContainerId { get; set; }
        public long? EmptyGateOutContainerId { get; set; }

        public string ContainerNumber { get; set; }
        public string VirNumber { get; set; }

        public int? ContainerSize { get; set; }

        public string VehicleNumber { get; set; }

        //public string TransportorName { get; set; }

        public long? TransporterId { get; set; }

        public string DeliveredYard { get; set; }
         
        public DateTime? DeliveredYardDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ContainerCondition { get; set; }

        public long? ShippingAgentId { get; set; }
         
        public string ShippingLine { get; set; }

        public bool? isEmptyOut { get; set; }
        
    }
}
