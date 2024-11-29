using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class DestuffingViewModelForTTSOUpload
    {
        public long ContainerIndexId { get; set; }

        public int? IndexNumber { get; set; }
        public string VIRNumber { get; set; }

        public string ContainerNo { get; set; }

        public string Description { get; set; }

        public string BLNo { get; set; }

        public double? ManifestWeight { get; set; }

        public double? FoundWeight { get; set; }

        public int? Package { get; set; }

        public string PackageType { get; set; }

        public string FoundPackageType { get; set; }

        public double? CBM { get; set; }

        public string MarksAndNumber { get; set; }

        public string Location { get; set; }

        public string Remarks { get; set; }

        public string ShortExcess { get; set; }

        public string ShortExcessRemarks { get; set; }

        public string InvoiceFoud { get; set; }
         
        public string CosigneeName { get; set; }

        public int? ManifestQuantity { get; set; }

        public Boolean Ischeck { get; set; }

        public bool IsHold { get; set; }
        public string TellyClerk { get; set; }
        public DateTime DestuffDate { get; set; }
        public string DayNight { get; set; }
        public long? ShippingAgentId { get; set; }
        public string ContainerType { get; set; }
        public string InvoiceAmount { get; set; }
        public long? VehicleMeasurementId { get; set; }
         
    }
}
