using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Cargo")]
    public class Cargo : CommonProperties
    {
        public long CargoId { get; set; }

        public string ColorCode { get; set; }

        public string FinalDestination { get; set; }

        public string GDNumber { get; set; }

        public string DishargePort { get; set; }

        public string PLIDNumber { get; set; }

        public string WarehouseLocation { get; set; }

        public double? CBM { get; set; }

        public double? Weight { get; set; }

        public string Location { get; set; }

        public int? PackageReceived { get; set; }

        public DateTime? IssueDate { get; set; }

        public string TRNumber { get; set; }

        public long? LoadingProgramDetailId { get; set; }

        public LoadingProgramDetail LoadingProgramDetail { get; set; }
        
        public long? VesselExportId { get; set; }

        public VesselExport VesselExport { get; set; }

        public long? VoyageExportId { get; set; }

        public VoyageExport VoyageExport { get; set; }

        public long? CommodityId { get; set; }

        public Commodity Commodity { get; set; }

        public long? ExportContainerId { get; set; }

        public ExportContainer ExportContainer { get; set; }

        public long? PortAndTerminalId { get; set; }

        public PortAndTerminal PortAndTerminal { get; set; }

    }
}
