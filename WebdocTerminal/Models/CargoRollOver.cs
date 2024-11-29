using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class CargoRollOver : CommonProperties
    {
        public long CargoRollOverId { get; set; }

        public string GDNumber { get; set; }

        public long? VesselExportId { get; set; }

        public VesselExport VesselExport { get; set; }

        public long? VoyageExportId { get; set; }

        public VoyageExport VoyageExport { get; set; }
    }
}
