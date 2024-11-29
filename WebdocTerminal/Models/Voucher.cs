using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Voucher")]
    public class Voucher : BaseClass
    {
        public Voucher()
        {
            VoucherDetails = new List<VoucherDetail>();
        }
        public long VoucherId { get; set; } 
        public string VoucherNo { get; set; }
        public long VoucherTypeId { get; set; }
        //public string InvoiceType { get; set; }
        //public string Reference { get; set; }
        //public string CustomerVendorDescription { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime VoucherDate { get; set; }
        public string Narration { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public long CompanyId { get; set; }
        public long? VerifyById { get; set; }
        public DateTime? VerifyDate { get; set; } = DateTime.Now;

        public List<VoucherDetail> VoucherDetails { get; set; }

    }
}
