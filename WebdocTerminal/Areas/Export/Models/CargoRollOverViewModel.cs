using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class CargoRollOverViewModel
    {
        public string GDNumber { get; set; }
        public string ClearingAgent { get; set; }
        public string ShippingAgent { get; set; }
        public string Shipper { get; set; }
        public int? NoOfPackages { get; set; }
        public string PackageType { get; set; }
        public string Commodity { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public long? VoyageExportId { get; set; }
        public long? VesselExportId { get; set; }

    }
}
