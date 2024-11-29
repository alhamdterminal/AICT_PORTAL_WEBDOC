using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffExport")]
    public class TariffExport : CommonProperties
    {
        public TariffExport()
        {
            ShippingAgentTariffs = new List<ShippingAgentTariff>();

            GDTariffs = new List<GDTariff>();

            //StorageSlabExports = new List<StorageSlabExport>();

            TariffRateSlabWises = new List<TariffRateSlabWise>();

        }
        public long TariffExportId { get; set; }

        public DateTime? TariffDate { get; set; }

        public DateTime? ImplementFrom { get; set; }
        public bool? IsCBMorRate { get; set; }
        public int Rate20 { get; set; }

        public int Rate40 { get; set; }

        public int Rate45 { get; set; }

        public int CBMRate { get; set; }

        public int WeightRate { get; set; }

        public int PerIndexRate { get; set; }

        public int CBMFixedRate { get; set; }
        public int CBMMultiplyValue { get; set; }
        public bool? RoundCBMCode { get; set; }

        public bool DailyCharges { get; set; }

        public bool IsActive { get; set; }
        public string TariffType { get; set; }
        public long? TariffHeadExportId { get; set; }

        public long? NatureOfTariffId { get; set; }

        public NatureOfTariff NatureOfTariff { get; set; }

        public TariffHeadExport TariffHeadExport { get; set; }

        public List<GDTariff> GDTariffs { get; set; }

        public List<ShippingAgentTariff> ShippingAgentTariffs { get; set; }

        //public List<StorageSlabExport> StorageSlabExports { get; set; }
        public List<TariffRateSlabWise> TariffRateSlabWises { get; set; }
    }
}
