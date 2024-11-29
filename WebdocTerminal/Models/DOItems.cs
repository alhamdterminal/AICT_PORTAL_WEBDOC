using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DOItem")]
    public class DOItem : CommonProperties
    {
        public long DOItemId { get; set; }

        public string TruckNumber { get; set; }
        public string ContainerNumber { get; set; }

        public string PackageType { get; set; }

        public int NoOfPackages { get; set; }

        public string Status { get; set; }

        public double Quantity { get; set; }

        public bool IsGateOut { get; set; }

        public long? GatePassId { get; set; }

        public GatePass GatePass { get; set; }

        public long? GatePassExportId { get; set; }

        public GatePassAuction GatePassAuction { get; set; }

        public long? GatePassAuctionId { get; set; }

        public GatePassExport GatePassExport { get; set; }

    }
}
