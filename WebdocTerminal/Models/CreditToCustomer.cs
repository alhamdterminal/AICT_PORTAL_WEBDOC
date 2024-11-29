using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CreditToCustomer")]
    public class CreditToCustomer : CommonProperties
    {
        public long CreditToCustomerId { get; set; }
        public string AuthorizedBy { get; set; }
        public int AuthorizedDays { get; set; }
        public long EnteringCargoId { get; set; }
        public EnteringCargo EnteringCargo { get; set; }
    }
}
