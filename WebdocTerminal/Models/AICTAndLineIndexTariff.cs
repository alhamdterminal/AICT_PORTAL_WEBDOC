using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AICTAndLineIndexTariff")]
    public class AICTAndLineIndexTariff : CommonProperties
    {
        public long AICTAndLineIndexTariffId { get; set; }

        public double AICTPerCBMRateShareRate { get; set; }
        public double AICTPerIndexRateShareRate { get; set; }
        public double FFPerCBMRateShareRate { get; set; }
        public double FFPerIndexRateShareRate { get; set; }

        public double AICTPerCBMRate { get; set; }
        public double AICTPerIndexRate { get; set; }
        public double FFPerCBMRate { get; set; }
        public double FFPerIndexRate { get; set; }

         
        public double TotalCBMRate { get; set; }
        public double TotalPerIndexRate { get; set; }
        public double PerBoxRate { get; set; }
        public string VirNumber { get; set; }
        public string ContainerNumber { get; set; }
        public int IndexNumber { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
