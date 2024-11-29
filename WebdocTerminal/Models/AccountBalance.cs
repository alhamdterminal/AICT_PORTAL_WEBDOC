using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AccountBalance")]
    public class AccountBalance : BaseClass
    {
        public long AccountBalanceId { get; set; }
        public long ChartOfAccountId { get; set; }
        public ChartOfAccount ChartOfAccount { get; set; }
        public long CompanyId { get; set; }
        public long? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public DateTime? TranDate { get; set; }
        public bool IsActive { get; set; }
    }
}
