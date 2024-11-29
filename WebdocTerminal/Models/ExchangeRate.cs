using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExchangeRate")]
    public class ExchangeRate : CommonProperties
    {
        public long ExchangeRateId { get; set; }
        public double ExchangeRateAmount { get; set; }
        public double RateAmount20 { get; set; }
        public double RateAmount40 { get; set; }
        public double RateAmount45 { get; set; }

    }
}
