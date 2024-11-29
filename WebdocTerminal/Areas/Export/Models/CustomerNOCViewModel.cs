using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class CustomerNOCViewModel
    {
        public string ShippingAgentName { get; set; }
        public string ShippingAgentNameB { get; set; }
        public string NameOfPerson { get; set; }
        public string ContactNumber { get; set; }
        public DateTime? EmailReceived { get; set; }

        public int? PackagesReceived { get; set; }

        public string PackageType { get; set; }

        public string CommodityName { get; set; }

        public string Vessel { get; set; }

        public string Voyage { get; set; }

        public double? CBM { get; set; }

        public double? Weight { get; set; }
    }
}
