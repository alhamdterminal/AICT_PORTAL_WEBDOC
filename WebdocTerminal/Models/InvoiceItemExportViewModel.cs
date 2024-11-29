using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class InvoiceItemExportViewModel : CommonProperties
    {
        public long TariffId { get; set; }

        public long TariffExportId { get; set; }

        public string Description { get; set; }
        public string NatureOfTariff { get; set; }
        public string Type { get; set; }

        //public string NatureOfTariffType { get; set; }

        public double Amount { get; set; }

    }
}
