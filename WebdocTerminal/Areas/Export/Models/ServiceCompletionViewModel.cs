using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class ServiceCompletionViewModel
    {
        public long ExportContainerId { get; set; }

        public string Category { get; set; }

        public string ContainerNo { get; set; }

        public string VehicleNo { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public string IndexNumber { get; set; }

        public string GDNumber { get; set; }

        public string ActivityType { get; set; }

        public string Location { get; set; }

        public int? Weight { get; set; }

        public string HandlingCode { get; set; }

        public DateTime? ServiceDate { get; set; }

        public bool? isChecked { get; set; }

        public int? NoOfPackages{ get; set; }

        public string PackageType{ get; set; }
    }
}
