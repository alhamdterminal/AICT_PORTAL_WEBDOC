using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
     public class GateOutGD : CommonProperties
    {
        public long GateOutGDId { get; set; }
        public string GDNumber { get; set; }
        public string VehicleNumber { get; set; }
        public int? NumberOfPackages { get; set; }
        public string PackageType { get; set; }
        public double? Weight { get; set; }
        public string Status { get; set; }
        public DateTime? GateOutDateime { get; set; }
        public bool? IsGateOut { get; set; }

    }
}
