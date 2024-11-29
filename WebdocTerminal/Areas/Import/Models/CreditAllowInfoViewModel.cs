using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CreditAllowInfoViewModel
    {
        public string FF { get; set; }
        public string ClearingAgent { get; set; }
        public string Consignee { get; set; }
        public double Storage { get; set; }
        public double CBM { get; set; }
        public double Weight { get; set; }
        public double FFAictShare { get; set; }
        public double? TotalCharges { get; set; }
        public double? OtherCharges { get; set; }
        public long CreditAllowedRs { get; set; }
        public long CreditDays { get; set; }
        public string Remarks { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
