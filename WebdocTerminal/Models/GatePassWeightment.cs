using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GatePassWeightment")]
    public class GatePassWeightment : CommonProperties
    {

        public long GatePassWeightmentId { get; set; }
        public double? GrossWeight { get; set; }
        public double? ManifestedWeight { get; set; }
        public string TruckNumber { get; set; }
        public string Virnumber { get; set; }
        public int? IndexNumber { get; set; }

    }
}
