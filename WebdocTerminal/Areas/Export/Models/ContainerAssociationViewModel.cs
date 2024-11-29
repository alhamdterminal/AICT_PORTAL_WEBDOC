using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class ContainerAssociationViewModel
    {
        public long ExportContainerId { get; set; }

        public string ContainerNumber { get; set; }

        public string VehicleNumber { get; set; }

        public string GDNumber { get; set; }

        public int? NumberofPackages { get; set; }

        public string ShippingLineCode { get; set; }

        public string ShippingLineNTN { get; set; }

        public string ShippingLineName { get; set; }

        public string ConsolidationStatus { get; set; }

        public DateTime? AssociationDate { get; set; }

        public string HandlingCode { get; set; }

        public bool? isChecked { get; set; }
 
    }
}
