using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Account.Models
{
    public class FinancialYearClosureViewModel
    {
        public string VoucherType { get; set; }
        public string ServiceType { get; set; }
        public string AccountName { get; set; }
        public string DepartName { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }

        public decimal? Balance { get; set; }

        public string Narration { get; set; }

        public long DepartmentId { get; set; }
        public long ChartOfAccountId { get; set; }
        public long VoucherServiceTypeId { get; set; }
    }
}
