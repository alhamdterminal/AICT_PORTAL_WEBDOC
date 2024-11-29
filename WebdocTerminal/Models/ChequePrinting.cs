using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ChequePrinting")]
    public class ChequePrinting : BaseClass
    {
        public long ChequePrintingId { get; set; }
        public string VoucherNo { get; set; }
        public string BankName { get; set; }
        public DateTime VoucherDate { get; set; }
        public string ChequeNo { get; set; }
        public string PartyName { get; set; }
        public decimal? Amount { get; set; }
        public string AmountInWords { get; set; }
        public long VoucherId { get; set; }
        public long VoucherDetailId { get; set; }
        public long CompanyId  { get; set; }
        public long Count  { get; set; }
        public bool? IsPrinted { get; set; }
        
    }
}
