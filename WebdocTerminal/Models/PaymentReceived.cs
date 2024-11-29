using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PaymentReceived")]
    public class PaymentReceived : CommonProperties
    {

        public long PaymentReceivedId { get; set; }

        //public string AICTBankName { get; set; }
        public long? BankId { get; set; }
        public Bank Bank { get; set; }
        public string AICTAccountNumber { get; set; }
        public long ReceivedAmount { get; set; }
        public string InsturmentNo { get; set; }
        public long CreditAllowed { get; set; }
        public string InoviceNo { get; set; }
        public string KnockOffInvoiceNo { get; set; }
        public long KnockOffAmount { get; set; }
        public string NatureOfAmount { get; set; }
        public string PaymentFor { get; set; }
        public bool IsSettle { get; set; }
               

    }
}
