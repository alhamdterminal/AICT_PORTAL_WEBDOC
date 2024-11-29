using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{

    [Table("DestuffedContainer")]
    public class DestuffedContainer : CommonProperties
    {
        [Key]
        public long DestuffedContainerId { get; set; }

        public int Index { get; set; }

        public string Description { get; set; }

        public int? PackageQuantity { get; set; }

        public string PackageType { get; set; }

        public string FoundPackageType { get; set; }

        public double? CBM { get; set; }

        public string Location { get; set; }

        public string MarksAndNumber { get; set; }

        public int? ManifestWeight { get; set; }

        public double? FoundWeight { get; set; }

        public int? ManifestQuantity { get; set; }

        public string ConsigneeName { get; set; }

        public string Remarks { get; set; }

        public string ShortExcess { get; set; } 

        public string ShortExcessRemarks { get; set; }

        public string InvoiceFound { get; set; }

        public string InvoiceAmount { get; set; }

        public long? TellySheetId { get; set; }

        public TellySheet TellySheet { get; set; }


        public long? VehicleMeasurementId { get; set; }

        public VehicleMeasurement VehicleMeasurement { get; set; }

        public long ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }
    }
}
