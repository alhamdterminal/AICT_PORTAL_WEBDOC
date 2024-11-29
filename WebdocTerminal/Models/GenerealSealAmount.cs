using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GenerealSealAmount")]
    public class GenerealSealAmount : CommonProperties
    {
        public long GenerealSealAmountId { get; set; }
        public long Amount { get; set; }
    }
}
