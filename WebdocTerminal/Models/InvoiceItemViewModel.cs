using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class InvoiceItemViewModel : CommonProperties
    {
        public long TariffId { get; set; }

        public long TariffExportId { get; set; }
        public long ExportTariffId { get; set; }
        public long TariffPercentId { get; set; }
        public long TariffInformationId { get; set; }
        public string TariffType { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }
        public string NatureOfTariff { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

    }
}
