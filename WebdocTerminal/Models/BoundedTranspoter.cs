using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("BoundedTranspoter")]
    public class BoundedTranspoter : CommonProperties
    {
        public long BoundedTranspoterId { get; set; }
        public string BoundedNTN { get; set; }
        public string BoundedCarrierName { get; set; }
    }
}
