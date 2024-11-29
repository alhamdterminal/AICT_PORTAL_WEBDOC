using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffInofrmationTariffExport")]
    public class TariffInofrmationTariffExport : CommonProperties
    {
        public long TariffInofrmationTariffExportId { get; set; }
         public long TariffInformationExportId { get; set; }
        public long ExportTariffId { get; set; }
        public ExportTariff ExportTariff { get; set; }
        public TariffInformationExport TariffInformationExport { get; set; }
    }
}
