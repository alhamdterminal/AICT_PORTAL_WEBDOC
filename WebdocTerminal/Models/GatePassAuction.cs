using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GatePassAuction")]
    public class GatePassAuction : CommonProperties
    {
        public GatePassAuction()
        {
            DOItems = new List<DOItem>();
        }
        public long GatePassAuctionId { get; set; }

        public DateTime? DODate { get; set; }
         

        public string GatePassNumber { get; set; }

        public string UnitType { get; set; }

        public string ImportBillNumber { get; set; }

        public DateTime? BillDate { get; set; }

        public string Remarks { get; set; }

        public int? TotalPackages { get; set; }

        public int? BalancePackages { get; set; }

        public DateTime? GatePassDate { get; set; }

        public int? Delivered { get; set; }

        public int? TotalDelivered { get; set; }


        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CYContainerId { get; set; } 
 
        public CYContainer CYContainer { get; set; } 

        public List<DOItem> DOItems { get; set; }

    }
}
