using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("InvoiceItemExport")]
    public class InvoiceItemExport : CommonProperties
    {
        public long InvoiceItemExportId { get; set; }
        public string NatureOfTariff { get; set; }
        public string Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public double Amount { get; set; }

        public long? ExportTariffId { get; set; }

        public ExportTariff ExportTariff { get; set; }

        public long InvoiceExportId { get; set; }

        public InvoiceExport InvoiceExport { get; set; }
    }
}
