using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class GDInvoicesViewModel
    {

        public long InvoiceExportId { get; set; }
        public string InvoiceNo { get; set; }

        public bool IsSubBill { get; set; }

    }
}
