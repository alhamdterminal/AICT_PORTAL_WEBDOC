using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Areas.Export.Models
{
    public class EnteringCargoVIewModel
    {
        public long EnteringCargoId { get; set; }
        public string LPNumber { get; set; }
        public double NoOfPackages { get; set; }
        public string PackageType { get; set; }
        public string Consignee { get; set; }
        public string ClearingAgent { get; set; }
        public string ShipperName { get; set; }
        public string ChallanNumber { get; set; }
        public double GrossWeight { get; set; }
        public DateTime? GateInDate { get; set; }
        public string VehicleNo { get; set; }
        public string ShipperSeal { get; set; }

        public string Remarks { get; set; }

        public List<ExportVehicle> ExportVehicles { get; set; }
    }
}
