using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CYGateOutViewModel
    {

        public string Key { get; set; }        
        public string VIRNumber { get; set; }
        public string ContainerNumber { get; set; }
        public DateTime? GateOutDate { get; set; }
        public string Status { get; set; }
        public long DoItemId { get; set; }
        public string TruckNumber { get; set; }

        public string SealNumber { get; set; }

        public string BondedCarrier { get; set; }
        public string GatePassNumber { get; set; }

        public string PortOfExit { get; set; }

        public string Country { get; set; }

        public string GrossWeight { get; set; }

        public double? TerminalWeight { get; set; }

        public string ShiftedYard { get; set; }

        public long? ShippingAgentId { get; set; }

        public long CYContainerId { get; set; }

        public bool IsGateOut { get; set; }

        public string OldContainerNumberForCS { get; set; }
        public string Type { get; set; }
    }
}
