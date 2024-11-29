using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExportTariff")]
    public class ExportTariff : CommonProperties
    {
        public ExportTariff()
        {
            StorageSlabExports = new List<StorageSlabExport>();
        }
        public long ExportTariffId { get; set; }        
        public DateTime? TariffDate { get; set; }

        public DateTime? ImplementFrom { get; set; }
        public DateTime? ImplementTo { get; set; }

        public bool? IsCBMorRate { get; set; }
        public double Rate20 { get; set; }

        public double Rate40 { get; set; }

        public double Rate45 { get; set; }

        public double CBMRate { get; set; }

        public double WeightRate { get; set; }

        public double PerIndexRate { get; set; }
        public double DevededIndexAmount { get; set; }
        public string TypeOfTarifff { get; set; }
        public string TypeOfImplementation { get; set; }

        //aict
        //public int AICTRate20 { get; set; }

        //public int AICTRate40 { get; set; }

        //public int AICTRate45 { get; set; }

        //public int AICTCBMRate { get; set; }

        //public int AICTWeightRate { get; set; }

        //public int AICTPerIndexRate { get; set; }
        //public int AICTDevededIndexAmount { get; set; }

        ////ff
        //public int FFRate20 { get; set; }

        //public int FFRate40 { get; set; }

        //public int FFRate45 { get; set; }

        //public int FFCBMRate { get; set; }

        //public int FFWeightRate { get; set; }

        //public int FFPerIndexRate { get; set; }
        //public int FFDevededIndexAmount { get; set; }

        public bool? RoundCBMCode { get; set; }

        public bool DailyCharges { get; set; }

        public bool IsActive { get; set; }
        public bool IsGeneral { get; set; }
        public bool IsDollerRate { get; set; }
        public bool IsFixedRate { get; set; }
        //public string TariffType { get; set; }

        public long? TariffHeadExportId { get; set; }

        public TariffHeadExport TariffHeadExport { get; set; }

        public long? PortAndTerminalExportId { get; set; }

        public PortAndTerminalExport PortAndTerminalExport { get; set; }

        public List<StorageSlabExport> StorageSlabExports { get; set; }
        public List<TariffInofrmationTariffExport> TariffInofrmationTariffExports { get; set; }
 
    }
}
