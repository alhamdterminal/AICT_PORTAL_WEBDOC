using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffRateSlabWise")]
    public class TariffRateSlabWise : CommonProperties
    {
        public long TariffRateSlabWiseId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public long Rate { get; set; }

        public long FromCBM { get; set; }

        public long ToCBM { get; set; }

        public long TariffExportId { get; set; }

        public TariffExport TariffExport { get; set; }
    }
}
