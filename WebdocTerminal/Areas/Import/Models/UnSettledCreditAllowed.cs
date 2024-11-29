using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class UnSettledCreditAllowed
    {
        public string InvoiceNumber { get; set; }
        public string VirNumber { get; set; }
        public long IndexNumber { get; set; }
        public long PaymentReceivedId { get; set; }
    }
}
