using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EnteringCargo")]
    public class EnteringCargo : CommonProperties
    {

        public EnteringCargo()
        {
            GDTariffs = new List<GDTariff>();
            ExportVehicles = new List<ExportVehicle>();
        }

        public long EnteringCargoId { get; set; }

        public string GDNumber { get; set; }

        public DateTime? OPIAReceiveTime { get; set; }


        public string Remarks { get; set; }

        public string Key { get; set; }

        public string GateInStatus { get; set; }

        public int? NumberOfPackages { get; set; }

        public string PackageType { get; set; }

        public string ShipperName { get; set; }

        public string ChallanNumber { get; set; }

        public string ClearingAgentName { get; set; }

        public string ConsigneeName { get; set; }

        public double GrossWeight { get; set; }

        public string LoadingProgramNumber { get; set; }

        public bool? isGrounded { get; set; }

        public bool? isGateIn { get; set; }

        public bool? IsHold { get; set; }

        public string PaperReady { get; set; }

        public string ANFStatus { get; set; }
        //public string TariffType { get; set; }


        public int AdditionalFreeDays { get; set; }
        public long WaiverAmount { get; set; }


        public DateTime? IssueDate { get; set; }

        public string TRNumber { get; set; }

        public DateTime? PaperReadyDate { get; set; }

        public DateTime? ANFDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ExtraFreeDaysRemarks { get; set; }
        public long? LoadingProgramId { get; set; }

        public LoadingProgram LoadingProgram { get; set; }
        public ExportGroundedCargo ExportGroundedCargo { get; set; }

        public List<ExportVehicle> ExportVehicles { get; set; }

        public List<GDTariff> GDTariffs { get; set; }

        public GDHold GDHold { get; set; }




    }
}
