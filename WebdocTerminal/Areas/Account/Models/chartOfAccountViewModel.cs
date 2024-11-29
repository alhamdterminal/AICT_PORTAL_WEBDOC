using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Account.Models
{
    public class chartOfAccountViewModel
    {
        public long ChartOfAccountId { get; set; }
        public string SubCategory	 { get; set; }
        public string AccCode { get; set; }
        public string AccPCode { get; set; }
        public string AccountName { get; set; }
        public bool Status { get; set; }
        public long AccountTypeId { get; set; }
        public long CompanyId { get; set; }
        public long VoucherServiceTypeId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; } = DateTime.Now;
        public long? CreatedBy { get; set; }
        public long? EditedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; } = DateTime.Now;
    }
}
