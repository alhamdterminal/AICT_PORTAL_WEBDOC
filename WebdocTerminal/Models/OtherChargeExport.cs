using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("OtherChargeExport")]
    public class OtherChargeExport : CommonProperties
    {
        public long OtherChargeExportId { get; set; }
        public string ChargeName { get; set; }
        public double ChargeAmount { get; set; }
    }
}
