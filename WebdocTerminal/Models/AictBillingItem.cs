using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AictBillingItem")]
    public class AictBillingItem : CommonProperties
    {
        public long AictBillingItemId { get; set; }
        public string VirNo { get; set; }
        public long IndexNo { get; set; }
        public string LineInvoiceno { get; set; }
        public DateTime? LineInvoiceDate { get; set; }
        public string AictInvoiceNo { get; set; }
        public DateTime? AictInvoiceDate { get; set; }
        public decimal LineStorage { get; set; }
        public decimal LineServiceChages { get; set; }
        public decimal LineTotalCharges { get; set; }
        public decimal StoragePercentToLine { get; set; }
        public decimal ServiceChargesPercentToLine { get; set; }
        public decimal AICTBillingToLine { get; set; }
        public long AictBillingId { get; set; }
        public AictBilling AictBilling { get; set; }
    }
}
