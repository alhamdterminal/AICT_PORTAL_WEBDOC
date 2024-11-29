using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffHeadExport")]
    public class TariffHeadExport : CommonProperties
    {
        public TariffHeadExport()
        {
            ExportTariffs = new List<ExportTariff>();
        }

        public long TariffHeadExportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string TariffHeadExportType { get; set; }

        public List<ExportTariff> ExportTariffs { get; set; }
    }
}
