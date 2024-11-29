using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class UnSettledInvoicesViewModel
    {
        public long SerialNumber { get; set; }
        public string ShippingAgent { get; set; }
        public string ClearingAgent { get; set; }
        public string Shipper { get; set; }
        public string GdNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double AmountExTax { get; set; }
        public double SalesTax { get; set; }
        public double AmountIncTax { get; set; }
        public long DwellDays { get; set; }
        public string ContactNumber { get; set; }
        public string ApprovedBy { get; set; }
    }
}
