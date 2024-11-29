using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class TPCargoDetails : CommonProperties
    {
        public long TPCargoDetailsId { get; set; }
        public DateTime? ReceiveStartDate { get; set; }
        public DateTime? ReceiveEndDate { get; set; }
        public int NoOfPackagesReceived { get; set; }
        public string PackageType { get; set; }
        public bool IsDG { get; set; }
        public bool IsReefer { get; set; }
        public double DGClass { get; set; }
        public double WeightDecalred { get; set; }
        public string ColorCode { get; set; }
        public string FinalDestination { get; set; }
        public string DischargePort { get; set; }
        public string PLIDNumber { get; set; }
        public string WarehouseLocation { get; set; }
        public double CBM { get; set; }
        public double Weight { get; set; }
        public string Temperature { get; set; }
        public long? VesselExportId { get; set; }
        public VesselExport VesselExport { get; set; }
        public long? VoyageExportId { get; set; }
        public VoyageExport VoyageExport { get; set; }
        public long? LoadingProgramId { get; set; }
        public LoadingProgram LoadingProgram { get; set; }
        public long? TPReceiveVehicleId { get; set; }
        public TPReceiveVehicle TPReceiveVehicle { get; set; }
        public string ReceivedFor { get; set; }
        public string Location { get; set; }
        public string CargoCondition { get; set; }
        public bool IsPIFFA { get; set; }
        public string Remarks { get; set; }
    }
}
