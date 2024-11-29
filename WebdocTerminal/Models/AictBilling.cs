using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AictBilling")]
    public class AictBilling : CommonProperties
    {
        public long AictBillingId { get; set; }
        public string BillingFrom { get; set; }
        public string AictBillingNumber { get; set; }
        public long SalesTax { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime PerformedDate { get; set; }
        public long ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
    }
}
