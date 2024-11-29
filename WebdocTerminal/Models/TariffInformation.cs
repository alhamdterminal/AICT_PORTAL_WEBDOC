using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffInformation")]
    public class TariffInformation : CommonProperties
    {
        public long TariffInformationId { get; set; }
        public bool IsEnableDisabled { get; set; }
        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        public long? ConsigneId { get; set; }
        public Consigne Consigne { get; set; }
        public long? ClearingAgentId { get; set; }
        public ClearingAgent ClearingAgent { get; set; }
        public long? GoodsHeadId { get; set; }
        public GoodsHead GoodsHead { get; set; }

        public long? PortAndTerminalId { get; set; }
        public PortAndTerminal PortAndTerminal { get; set; }
        public long? ISOCodeHeadId { get; set; }
        public ISOCodeHead ISOCodeHead { get; set; }
        public string TariffType { get; set; }
        public string ContainerStatus { get; set; }
        public string IndexCargoType { get; set; }
        public long? StorageFreeDays { get; set; }
        public long? DGFreeDays { get; set; }
        public long? ShipperId { get; set; }
        public Shipper Shipper { get; set; }
        public long PercentAgeShippingAgentId { get; set; }
        public List<TariffInofrmationTariff> TariffInofrmationTariffs { get; set; }

    }
}
