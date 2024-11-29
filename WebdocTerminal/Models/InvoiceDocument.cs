using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("InvoiceDocument")]
    public class InvoiceDocument : CommonProperties
    {
        public long InvoiceDocumentId { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }

        public long? InvoiceId { get; set; }

        public Invoice Invoice { get; set; }
    }
}
