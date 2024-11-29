using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffInformationExport")]
    public class TariffInformationExport : CommonProperties
    {
        public long TariffInformationExportId { get; set; }

        public bool IsEnableDisabled { get; set; }
        public long? ShippingLineExportId { get; set; }
        public ShippingLineExport ShippingLineExport { get; set; }
        public long? PortAndTerminalExportId { get; set; }
        public PortAndTerminalExport PortAndTerminalExport { get; set; }
        public long? ClearingAgentExportId { get; set; }
        public ClearingAgentExport ClearingAgentExport { get; set; }
        public long? ExportConsigneeId { get; set; }
        public ExportConsignee ExportConsignee { get; set; }
        public long? ShippingAgentExportId { get; set; }
        public ShippingAgentExport ShippingAgentExport { get; set; }
        public long? GoodsHeadExportId { get; set; }
        public GoodsHeadExport GoodsHeadExport { get; set; }
        public string IndexCargoType { get; set; }
        public long? StorageFreeDays { get; set; }
        public long? DGFreeDays { get; set; }
        public long PercentAgeShippingAgentExportId { get; set; }
        public List<TariffInofrmationTariffExport> TariffInofrmationTariffExports { get; set; }

    }
}
