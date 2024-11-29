using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffInofrmationTariff")]
    public class TariffInofrmationTariff : CommonProperties
    {
        public long TariffInofrmationTariffId { get; set; }
        public long TariffInformationId { get; set; }
        public long TariffId { get; set; }
        public Tariff Tariff { get; set; }
        public TariffInformation TariffInformation { get; set; }
    }
}
