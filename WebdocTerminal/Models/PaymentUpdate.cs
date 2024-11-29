using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PaymentUpdate")]
    public class PaymentUpdate : CommonProperties
    {
        public PaymentUpdate()
        {
            PaymentUpdateDetails = new List<PaymentUpdateDetail>();
        }

        public long PaymentUpdateId { get; set; }
        public string PreAlertNumber { get; set; }
        public DateTime PaymentUpdateCreatedDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public long RequestNumber { get; set; } 
        public DateTime RequestDate { get; set; }
 
        public List<PaymentUpdateDetail> PaymentUpdateDetails { get; set; }

    }
}
