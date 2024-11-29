using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class AmountReceivedViewmodel : CommonProperties
    {
        public long InvoiceId { get; set; }

        public long InvoiceExportId { get; set; }

        public double BalanceAmount { get; set; }

        public double SalesTax { get; set; }

        public double BillAmount { get; set; }

        public double AmountReceived { get; set; }
    }
}
