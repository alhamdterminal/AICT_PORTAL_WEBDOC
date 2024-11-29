using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("LineSealAmount")]
    public class LineSealAmount : CommonProperties
    {
        public long LineSealAmountId { get; set; }
        public long ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        public long Amount { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
