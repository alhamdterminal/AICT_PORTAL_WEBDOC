using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class GateInExport : CommonProperties
    {
        public long GateInExportId { get; set; }

        public string GDNumber { get; set; }

        public int? NumberofPackages { get; set; }

        public string PackageType { get; set; }

        public double Weight { get; set; }

        public DateTime? GateInDate { get; set; }

        public string VehicleNumber { get; set; }

        public string GateInStatus { get; set; }
    }
}
