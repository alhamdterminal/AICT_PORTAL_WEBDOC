using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyContainerInvoiceItem")]
    public class EmptyContainerInvoiceItem : CommonProperties
    {
        public long EmptyContainerInvoiceItemId { get; set; }

        public string Description { get; set; }
        public string Type { get; set; }

        public double Amount { get; set; }

        public long EmptyContainerInvoiceId { get; set; }

        public EmptyContainerInvoice EmptyContainerInvoice { get; set; }

    }
}
