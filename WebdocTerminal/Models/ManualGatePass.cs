using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ManualGatePass")]
    public class ManualGatePass : CommonProperties
    {
        public long ManualGatePassId { get; set; }
        public long? ContainerindexId { get; set; }
        public ContainerIndex Containerindex { get; set; }
        public long? CYContainerId { get; set; }
        public CYContainer CYContainer { get; set; }
        public long? ManualGatePassNumber { get; set; }

        public string TruckNumber { get; set; }
        public string Shift { get; set; }
        public string Remarks { get; set; }
        public string Type { get; set; }

        public long? RandDClerkId { get; set; }
        public RandDClerk RandDClerk { get; set; }

        public double? TotalPackages { get; set; }

        public double? BalancePackages { get; set; }

        public DateTime? GatePassDate { get; set; }

        public double? Delivered { get; set; }

        public double? TotalDelivered { get; set; }

    }
}
