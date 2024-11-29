using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("InvoiceItem")]
    public class InvoiceItem : CommonProperties
    {
        public long InvoiceItemId { get; set; }
 
        //public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }

        public double Amount { get; set; }

        public long InvoiceId { get; set; }

        public Invoice Invoice { get; set; }
    }
}
