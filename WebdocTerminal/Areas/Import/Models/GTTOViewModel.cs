using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class GTTOViewModel
    {
        public long ContainerId { get; set; }
        public long SVMOId { get; set; }
        public string Key { get; set; }
        public string BlNo { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerNoIGMO { get; set; }
        public string ContainerSize { get; set; } 
        public string VIRNo { get; set; }
        public string VehicleNo { get; set; }
        public string Category { get; set; }
        public string ShiftedYard { get; set; }
        public string Status { get; set; }
        public int? IndexNo { get; set; }
        public DateTime? GateOutTime { get; set; }
        public string PCCSSSealNumber { get; set; }
        public long? ShippingAgentId { get; set; }
        public string BondedCarrier { get; set; }
        public string PortOfExit { get; set; }
        public string CountryOfExit { get; set; } 
        public double? GrossWeight { get; set; }  
        public bool IsGateOut { get; set; }
    }
}
