using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("NatureOfTariff")]
    public class NatureOfTariff : CommonProperties
    {

        public long NatureOfTariffId { get; set; }
        public string NatureOfTariffName { get; set; }
        public string TariffType { get; set; }
    }
}
