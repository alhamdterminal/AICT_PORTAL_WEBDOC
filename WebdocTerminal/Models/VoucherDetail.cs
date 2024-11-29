using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("VoucherDetail")]
    public class VoucherDetail : BaseClass
    {
     
        public long VoucherDetailId { get; set; }
        public DateTime TranDate { get; set; }
        public long VoucherId { get; set; }
        public Voucher Voucher { get; set; }
        public long CompanyId { get; set; }
        public long VoucherTypeId { get; set; }
        public VoucherType VoucherType { get; set; }
        public long DepartmentId { get; set; }
        public long VoucherServiceTypeId { get; set; }
        public VoucherServiceType VoucherServiceType { get; set; }
        public long ChartOfAccountId { get; set; }
        public ChartOfAccount ChartOfAccount { get; set; }
        public  string AccCode { get; set; }
        public  decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public string ChequeOrReference { get; set; }
        public string Narration { get; set; }
        public long LineNumber { get; set; }
        public long? CustomerId { get; set; }
        public Customer Customer { get; set; }        
    }
}
