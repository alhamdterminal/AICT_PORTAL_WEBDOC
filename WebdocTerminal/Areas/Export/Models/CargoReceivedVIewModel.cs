using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class CargoReceivedVIewModel
    {
        public long ?  CargoReceivedId { get; set; }

        public string TruckNumber { get; set; }

        public string GDNumber { get; set; }

        public DateTime? VehicleReceivedDate { get; set; }

        public string ClearingAgent { get; set; }

        public long ?   ClearingAgentExportId { get; set; }

        public long ? LoadingProgramId { get; set; }

        public string AgentRepresentative { get; set; }

        public string AgentCNIC { get; set; }

        public string PackageType { get; set; }

        public string DriverName { get; set; }

        public string DriverCNIC { get; set; }

        public double? WeightDeclaredByDriver { get; set; }

        public int? NumberOfPackagesReceived { get; set; }

        public DateTime? RecevingStartTime { get; set; }

        public DateTime? RecevingEndTime { get; set; }
         
        public string CargoRecevingCondition { get; set; }


    }
}
