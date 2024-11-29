using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class GateInViewModel
    {
        public long ContainerId { get; set; }

        public string ContainerNo { get; set; }
        public string ContainerTypeStatus { get; set; }

        public string ContainerSize { get; set; }

        public string ISOCode { get; set; }

        public string VIRNo { get; set; }
        public long VoyageId { get; set; }

        public string CarrierId { get; set; }

        public string CarrierName { get; set; }
        public string Key { get; set; }
        public string Status { get; set; }

        public string MenifestedSealNo { get; set; }

        public string PACSSSealNo { get; set; }

        public string VehicleNo { get; set; }

        public string FoundSealNumber { get; set; }
        public string ContainerLocation { get; set; }

        public DateTime? GateInDateTime { get; set; }

        public double? Weight { get; set; }

        public double? FoundWeight { get; set; }

        public double? DPTareweight { get; set; }

        public double? DPManifestWeight { get; set; }

        public long? ShippingAgentId { get; set; }
        public long? TransporterId { get; set; }

        public bool IsGateIn { get; set; }
    }
}
