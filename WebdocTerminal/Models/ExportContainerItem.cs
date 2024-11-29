using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExportContainerItem")]
    public class ExportContainerItem : CommonProperties
    {
        public long ExportContainerItemId { get; set; }

        public string GDNumber { get; set; }
        public string ContainerNumber { get; set; }

        public int? NumberOfPackages { get; set; }

        public string AllowLoading { get; set; }

        public string Destination { get; set; }

        public bool? Ischecked { get; set; }

        public bool IsSubmitted { get; set; }
        public bool IsGateOut { get; set; }
        public bool IsOvams { get; set; }

        public DateTime? GateOutDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? SubmitedDate { get; set; }

        public long? OrderNumber { get; set; }

        public string ShipperName { get; set; }
        //public string isAmountReceived { get; set; }
         
        public long? ExportContainerId { get; set; }

        public ExportContainer ExportContainer { get; set; }

        public long? VesselExportId { get; set; }

        public VesselExport VesselExport { get; set; }

        public long? VoyageExportId { get; set; }

        public VoyageExport VoyageExport { get; set; }
        

    }
}
