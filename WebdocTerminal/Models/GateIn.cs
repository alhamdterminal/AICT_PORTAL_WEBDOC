using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GateIn")]
    public class GateIn : CommonProperties
    {
        public long GateInId { get; set; }

        public string ContainerNo { get; set; }

        public string ContainerSize { get; set; }

        public string ISOCode { get; set; }

        public string VIRNo { get; set; }

        public string CarrierId { get; set; }

        public string CarrierName { get; set; }

        public string MenifestedSealNo { get; set; }

        public string PACSSSealNo { get; set; }

        public string VehicleNo { get; set; }

        public string FoundSealNumber { get; set; }

        public DateTime? GateInDateTime { get; set; }

        public double? Weight { get; set; }

        public double? DPTareweight { get; set; }

        public double? DPManifestWeight { get; set; }

        public string BerthAt { get; set; }

        public long? ShippingAgentId { get; set; }

        public ShippingAgent ShippingAgent { get; set; }
    }
}
