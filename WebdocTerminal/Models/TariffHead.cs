using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TariffHead")]
    public class TariffHead : CommonProperties
    {
        public TariffHead()
        {
            Tariffs = new List<Tariff>();
        }

        public long TariffHeadId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string TariffHeadType { get; set; }

        public List<Tariff> Tariffs {get; set;}
    }
}
