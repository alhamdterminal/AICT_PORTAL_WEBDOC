using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffPercentageExport")]
    public class TariffPercentageExport : CommonProperties
    {
        public long TariffPercentageExportId { get; set; }
        public long RateOnPersent { get; set; }
        public string TariffPercentType { get; set; }
        public long TariffHeadExportId { get; set; }
        public TariffHeadExport TariffHeadExport { get; set; }
        public long ShippingAgentExportId { get; set; }
        public ShippingAgentExport ShippingAgentExport { get; set; }
    }
}
