using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffPercentage")]
    public class TariffPercentage : CommonProperties
    {
        public long TariffPercentageId { get; set; }

        public long RateOnPersent { get; set; }
        public string TariffPercentType { get; set; }
        public long TariffHeadId { get; set; }
        public TariffHead TariffHead { get; set; }
        public long ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }

    }
}
