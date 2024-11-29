using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CargoReceived")]
    public class CargoReceived : CommonProperties
    {
        public CargoReceived()
        {
            var Cargos = new List<Cargo>();
        }
        public long CargoReceivedId { get; set; }

        public string TruckNumber{ get; set; }

        public string CLearingAgentReprsentative{ get; set; }

        public string ClearingAgentCNIC { get; set; }

        public string DriverName { get; set; }

        public string DriverCNIC { get; set; }

        public DateTime? GateInDateTime { get; set; }

        public int? NumberOfPackagesReceived { get; set; }

        public string PackageType { get; set; }

        public DateTime? RecevingStartTime { get; set; }

        public DateTime? RecevingEndTime { get; set; }


        public string CargoRecevingCondition { get; set; }

        public double? WeightDeclaredByDriver { get; set; }

        public long? LoadingProgramId { get; set; }

        public LoadingProgram LoadingProgram { get; set; }

        public long? ClearingAgentExportId { get; set; }

        public ClearingAgentExport ClearingAgentExport { get; set; }

        public List<Cargo> Cargos { get; set; }



    }
}
