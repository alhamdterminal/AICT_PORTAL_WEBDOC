using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DisabledAgentTariffExport")]
    public class DisabledAgentTariffExport : CommonProperties
    {
        public long DisabledAgentTariffExportId { get; set; }

        public long? EnteringCargoId { get; set; }

        public EnteringCargo EnteringCargo { get; set; }

        public long ShippingAgentTariffExportId { get; set; }

        public ShippingAgentTariffExport ShippingAgentTariffExport { get; set; }
    }
}
