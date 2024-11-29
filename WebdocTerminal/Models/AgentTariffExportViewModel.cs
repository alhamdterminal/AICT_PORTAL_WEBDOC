using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class AgentTariffExportViewModel : CommonProperties
    {
        public long ShippingAgentTariffExportId { get; set; }

        public long TariffExportId { get; set; }

        public DateTime? TariffDate { get; set; }

        public DateTime? ImplementFrom { get; set; }

        public int Rate20 { get; set; }

        public int Rate40 { get; set; }

        public int Rate45 { get; set; }

        public int CBMRate { get; set; }

        public int CBMFixedRate { get; set; }

        public int WeightRate { get; set; }

        public int PerIndexRate { get; set; }

        public bool? RoundCBMCode { get; set; }

        public bool DailyCharges { get; set; }

        public bool IsActive { get; set; }

        public int CBMMultiplyValue { get; set; }

        public bool CBMMultiply { get; set; }

        public long? TariffHeadExportId { get; set; }

        public TariffHeadExport TariffHeadExport { get; set; }

    }
}
