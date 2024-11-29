using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GDTariff")]
    public class GDTariff : CommonProperties
    {
        public long GDTariffId { get; set; }

        public long TariffExportId { get; set; }

        public TariffExport TariffExport { get; set; }

        public long? EnteringCargoId { get; set; }

        public EnteringCargo EnteringCargo { get; set; }
    }
}
