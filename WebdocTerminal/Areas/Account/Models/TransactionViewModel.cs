using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Account.Models
{
    public class TransactionViewModel
    {
        public long VoucherDetailId { get; set; }
        public string VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string VoucherType { get; set; }
        public string ServiceType { get; set; }
        public string Party { get; set; }
        public string Department { get; set; }
        public string Account { get; set; }
        public string Reference { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public string Narration { get; set; }
 

    }
}
