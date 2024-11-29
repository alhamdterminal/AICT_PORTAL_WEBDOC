using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class DeliveryOrderViewModel
    {
        public string VesselName { get; set; }

        public string IGMYear { get; set; }

        public string IGMNumber { get; set; }

        public string VoyageNo { get; set; }
        public string CargoType { get; set; }
        public string GatePassType { get; set; }
        public string DeliveryType { get; set; }

        public string ContainerNumber { get; set; }
        public long ContainerIndexId { get; set; }

        public int? indexNumner { get; set; }
        public DateTime? ValidTo { get; set; }

        public string BLNumber { get; set; }
    }
}
