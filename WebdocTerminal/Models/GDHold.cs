using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table ("GDHold")]
    public class GDHold : CommonProperties
    {
        public long GDHoldId { get; set; }

        public string Remarks { get; set; }

        public DateTime? HoldDate { get; set; }

        public long? EnteringCargoId { get; set; }

        public EnteringCargo EnteringCargo { get; set; }
    }
}
