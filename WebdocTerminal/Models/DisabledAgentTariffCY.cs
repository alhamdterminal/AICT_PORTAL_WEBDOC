using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DisabledAgentTariffCY")]
    public class DisabledAgentTariffCY : CommonProperties
    {
        public long DisabledAgentTariffCYId { get; set; }

        public long? CYContainerId { get; set; }

        public CYContainer CYContainer { get; set; }

        public long ShippingAgentTariffId { get; set; }

        public ShippingAgentTariff ShippingAgentTariff { get; set; }
    }
}
