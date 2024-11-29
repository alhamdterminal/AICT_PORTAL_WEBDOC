using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ReturnToYardViewModel
    {
        public long ReturnToYardId { get; set; }
        public string ReturnToYardName { get; set; }
        public DateTime? ReturnToYardDate { get; set; }
        public long EmptyGateOutContainerId { get; set; }
        public string ContainerNumber { get; set; }
        public string VirNumber { get; set; }
        public string DocumentReceived { get; set; }
        public int? ContainerSize { get; set; }
        public string VehicleNumber { get; set; }
        public string ShiftedYard { get; set; }
        public DateTime? ShiftedYardDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ContainerCondition { get; set; }
        public long? ShippingAgentId { get; set; }
        public string ShippingLine { get; set; }
        public bool? isEmptyOut { get; set; }



    }
}
