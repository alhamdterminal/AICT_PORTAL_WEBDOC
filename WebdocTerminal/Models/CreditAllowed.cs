using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CreditAllowed")]
    public class CreditAllowed : CommonProperties
    {
        public long CreditAllowedId { get; set; }
        public string VirNumber { get; set; }
        public long IndexNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long CreditAllowedRs { get; set; }
        public long CreditAllowedExcessSettelment { get; set; }
        public long CreditDays { get; set; }
        public string Remarks   { get; set; }
        public string InvoiceNo   { get; set; }
        public bool IsSettled   { get; set; }
        public bool IsRefound   { get; set; }
        public bool IsApprove   { get; set; }
        public bool IsReject   { get; set; }
        public bool IsCancel   { get; set; }
        public string StatusType   { get; set; }


    }
}
