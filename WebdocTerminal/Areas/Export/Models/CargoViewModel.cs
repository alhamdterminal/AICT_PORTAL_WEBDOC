using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class CargoViewModel
    {
        public long ? CargoId { get; set; }

        public string VehicleNumber { get; set; }

        public string ClearingAgentName { get; set; }

        public string ShipperName { get; set; }

        public string ConsigneeName { get; set; }

        public int? PackagesReceived { get; set; }

        public string PackageType { get; set; }

        public DateTime? CargoReceivedDate { get; set; }

        public long? CargoReceivedId { get; set; }

        public long? PortAndterminalId { get; set; }

        public string FinalDestination { get; set; }

        public string DischargePort { get; set; }

        public long? VesselExportId { get; set; }

        public long? VoyageExportId { get; set; }

        public long? CommodityId { get; set; }

        public string PLIDNumber { get; set; }

        public string WarehouseLocation { get; set; }

        public double? CBM { get; set; }

        public double? Weight { get; set; }

        public string Location { get; set; }

        public int? PackageReceived { get; set; }

        public string ColorCode { get; set; }

        public long? LoadingProgramDetailId { get; set; }
         



    }
}
