using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class GDHoldDetailViewModel
    {
        public long EnteringCargoId { get; set; }

        public string PackageType { get; set; }

        public int? NoOfPackages { get; set; }

        public string ClearingAgent { get; set; }

        public string ShipperName { get; set; }
        public string Remarks { get; set; }

        public string ConsignmentReceived { get; set; }

        public long? GDHoldId { get; set; }
    }
}
