using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CFSGateOutViewModel
    {
        public string Key { get; set; }

        public long ContainerIndexId { get; set; }
        public long GatePassId { get; set; }
        public string GatePassNumber { get; set; }

        public string Category { get; set; }

        public string VIRNo { get; set; }

        public string ContainerNo { get; set; }

        public long? IndexNo { get; set; }

        public string BLNo { get; set; }

        public int? NoOfPackages { get; set; }

        public string PackageType { get; set; }

        public string Status { get; set; }
        public long DoItemId { get; set; }

        public DateTime? GateOutTime { get; set; }

        public string TruckNo { get; set; }

        public string ShiftedYard { get; set; }

        public string PCCSSSealNo { get; set; }

        public string BondedCarrierNTN { get; set; }

        public string PortOfExit { get; set; }

        public string CountryOfExit { get; set; }

        public double? Weight { get; set; }

        public long? ShippingAgentId { get; set; }

        public bool IsGateOut { get; set; }
    }
}
