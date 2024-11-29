using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ShippingAgentTariffExport")] 
    public class ShippingAgentTariffExport : CommonProperties
    {
        public long ShippingAgentTariffExportId { get; set; }

        public long ShippingAgentExportId { get; set; }
        public ShippingAgentExport ShippingAgentExport { get; set; }

        public int CBMMultiplyValue { get; set; }

        public bool CBMMultiply { get; set; }

        public long TariffExportId { get; set; }

        public TariffExport TariffExport { get; set; }


    }
}
