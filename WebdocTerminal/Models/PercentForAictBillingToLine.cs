using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PercentForAictBillingToLine")]
    public class PercentForAictBillingToLine : CommonProperties
    {
        public long PercentForAictBillingToLineId { get; set; }
        public DateTime PerformedDate { get; set; }
        public string UserNmae { get; set; }
        public string SystemAddress { get; set; }
        public long StoragePercentToLine { get; set; }
        public long ServiceChargesPercentToLine { get; set; }        
        public long ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
    }
}
