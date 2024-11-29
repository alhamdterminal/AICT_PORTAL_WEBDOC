using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DisabledAgentTariff")]

    public class DisabledAgentTariff : CommonProperties
    {
        public long DisabledAgentTariffId  { get; set; }

        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }
         
        public long ShippingAgentTariffId { get; set; }

        public ShippingAgentTariff ShippingAgentTariff { get; set; } 
    }
}
