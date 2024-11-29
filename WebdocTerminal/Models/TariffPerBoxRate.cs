using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffPerBoxRate")]
    public class TariffPerBoxRate : CommonProperties
    {
        public long TariffPerBoxRateId { get; set; }
        public double Rate20 { get; set; }
        public double Rate40 { get; set; }
        public double Rate45 { get; set; }
        public double CBMRate { get; set; }
        public double WeightRate { get; set; }
        public double PerIndex { get; set; }
        public double DividedIndex { get; set; }
        public DateTime? ImplementFrom { get; set; }
        public DateTime? ImplementTo { get; set; }
        public string TypeOfImplementation { get; set; }
        public long TariffInformationId { get; set; }
        public TariffInformation TariffInformation { get; set; }
        public long? PortAndTerminalId { get; set; }
        public PortAndTerminal PortAndTerminal { get; set; }
    }
}
