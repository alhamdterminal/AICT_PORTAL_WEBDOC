using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("LineInvoiceDetail")]
    public class LineInvoiceDetail  : CommonProperties
    {
        public long LineInvoiceDetailId { get; set; }
        public string InvoiceCode { get; set; }
        public string MainHeaderDetail { get; set; }
        public string SubHeaderDetail { get; set; }
        public string MainFooter { get; set; }
        public string SubFooter { get; set; }
        public string MainLogo { get; set; }
        public string SubLogo { get; set; }
        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
    }
}
