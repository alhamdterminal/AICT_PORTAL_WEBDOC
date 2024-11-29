using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("NatureOfPayment")]
    public class NatureOfPayment : CommonProperties
    {
        public long NatureOfPaymentId { get; set; }
        public string NatureOfPaymentCode { get; set; }
        public string NatureOfPaymentName { get; set; }
    }
}
