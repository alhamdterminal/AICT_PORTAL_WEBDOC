using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ChartOfAccount")]
    public class ChartOfAccount : BaseClass
    {
        public ChartOfAccount()
        {
            VoucherServiceTypeDetails = new List<VoucherServiceTypeDetail>();
        }

        public long ChartOfAccountId { get; set; }

        public string AccCode { get; set; }
        public string AccPCode { get; set; }
        public string AccountName { get; set; }
        public bool Status { get; set; }        
        public long AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
        public long CompanyId { get; set; }

        public List<VoucherServiceTypeDetail> VoucherServiceTypeDetails { get; set; }

    }
}
