using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("InvoiceDocumentExport")]
    public class InvoiceDocumentExport : CommonProperties
    {
        public long InvoiceDocumentExportId { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }

        public long? InvoiceExportId { get; set; }

        public InvoiceExport InvoiceExport { get; set; }
    }
}
