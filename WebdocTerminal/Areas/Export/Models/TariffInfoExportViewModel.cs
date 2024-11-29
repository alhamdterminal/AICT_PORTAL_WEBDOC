using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Areas.Export.Models
{
    public class TariffInfoExportViewModel
    {
        public long ExportTariffId { get; set; }

        public DateTime? TariffDate { get; set; }

        public DateTime? ImplementFrom { get; set; }
        public DateTime? ImplementTo { get; set; }

        public bool? IsCBMorRate { get; set; }

        public long? StorageFreeDays { get; set; }
        public long? DGFreeDays { get; set; }
        public double Rate20 { get; set; }

        public double Rate40 { get; set; }

        public double Rate45 { get; set; }

        public double CBMRate { get; set; }
        public string TypeOfTariff { get; set; }

        public double WeightRate { get; set; }

        public double PerIndexRate { get; set; }
        public double DevededIndexAmount { get; set; }
        public string portname { get; set; }

        public bool? RoundCBMCode { get; set; }

        public bool DailyCharges { get; set; }

        public bool IsActive { get; set; }
        public bool IsFixedRate { get; set; }
        public bool IsGeneral { get; set; }
        public bool IsDollerRate { get; set; }

        public long? TariffInofrmationTariffExportId { get; set; }

        public long? TariffHeadExportId { get; set; }

        public string Name { get; set; }
        public string TypeOfImplementation { get; set; }

        public string Description { get; set; }
        public string TariffHeadExportType { get; set; }
        public long TariffCode { get; set; }
        public bool IsEnableDisabled { get; set; }


        public long? PortAndTerminalExportId { get; set; }

        public PortAndTerminalExport PortAndTerminalExport { get; set; }


    }
}
