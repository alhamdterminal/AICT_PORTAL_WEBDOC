using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class AictBillingViewModel
    {
        public string VirNo { get; set; }
        public long IndexNo { get; set; }
        public string LineInvoiceno { get; set; }
        public DateTime? LineInvoiceDate { get; set; }
        public string AictInvoiceNo { get; set; }
        public string AictBillingNumber { get; set; }
        public DateTime? AictInvoiceDate { get; set; }
        public double LineStorage { get; set; }
        public double LineServiceChages { get; set; }
        public double LineTotalCharges { get; set; }
        public double StoragePercentToLine { get; set; }
        public double ServiceChargesPercentToLine { get; set; }
        public double AICTBillingToLine { get; set; }
 
        public bool IsChecked { get; set; }
    }
}
