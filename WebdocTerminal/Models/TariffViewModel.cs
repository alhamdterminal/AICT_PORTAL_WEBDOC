using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class TariffViewModel : CommonProperties
    {
        public long TariffId { get; set; }
        public long TariffExportId { get; set; }

        public DateTime? TariffDate { get; set; }

        public DateTime? ImplementFrom { get; set; }

        public double Rate20 { get; set; }

        public double Rate40 { get; set; }

        public double Rate45 { get; set; }

        public double CBMRate { get; set; }

        public double WeightRate { get; set; }

        public double PerIndexRate { get; set; }
        public double DevededIndexAmount { get; set; }

        public bool? RoundCBMCode { get; set; }

        public bool DailyCharges { get; set; }

        public bool IsActive { get; set; }

        public long? AgentTariffId { get; set; }

        public long? TariffHeadId { get; set; }
        public string Type { get; set; }


        public TariffHead TariffHead { get; set; }


        public long? TariffHeadExportId { get; set; }


        public TariffHeadExport TariffHeadExport { get; set; }
        public bool InvoiceCreated { get; set; }
    }
}
