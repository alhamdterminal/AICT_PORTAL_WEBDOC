using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class IPOSViewModel
    {
        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string BondedCarrierId { get; set; }

        public string BondedCarrierName { get; set; }

        public string VehicleNumber { get; set; }
    }
}
