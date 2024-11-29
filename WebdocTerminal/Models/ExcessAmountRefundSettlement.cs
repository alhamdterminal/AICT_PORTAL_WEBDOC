using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExcessAmountRefundSettlement")]
    public class ExcessAmountRefundSettlement : CommonProperties
    {
        public long ExcessAmountRefundSettlementId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public long Amount { get; set; }
        public string InvoiceNo { get; set; }
        public string InFavorof { get; set; }
        public string Type { get; set; }
        public long RefoundAmount { get; set; }
        public long AICTServiceCharges { get; set; }
        public string OnllineAccountNumber { get; set; }
        public string OnllineAccountTitle { get; set; }
        public long OnllineAccountAmount { get; set; }
        public long? BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
