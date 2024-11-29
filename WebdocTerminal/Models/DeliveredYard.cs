using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DeliveredYard")]
    public class DeliveredYard : CommonProperties
    {
        public long DeliveredYardId { get; set; }
        public string DeliveredYardName { get; set; }
    }
}
