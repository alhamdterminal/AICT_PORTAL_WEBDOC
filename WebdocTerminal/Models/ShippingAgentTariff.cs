using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ShippingAgentTariff")]
    public class ShippingAgentTariff : CommonProperties
    {

        public ShippingAgentTariff()
        {
            DisabledAgentTariffs = new List<DisabledAgentTariff>();
            DisabledAgentTariffCYs = new List<DisabledAgentTariffCY>();
             
        }

        public long ShippingAgentTariffId { get; set; }

        public long ShippingAgentId { get; set; }

        public ShippingAgent ShippingAgent { get; set; }

        public long TariffId { get; set; }

        public Tariff Tariff { get; set; }

        public long? TariffExportId { get; set; }

        public List<DisabledAgentTariff> DisabledAgentTariffs { get; set; }
        public List<DisabledAgentTariffCY> DisabledAgentTariffCYs { get; set; }



    }
}
