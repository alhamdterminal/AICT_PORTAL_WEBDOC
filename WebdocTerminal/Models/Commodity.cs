using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Commodity")]
    public class Commodity : CommonProperties
    {
        public Commodity()
        {
            Cargos = new List<Cargo>();

        }
        public long CommodityId { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public string HsCode { get; set; }

        public List<Cargo> Cargos { get; set; }

    }
}
