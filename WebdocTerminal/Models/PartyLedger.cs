using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PartyLedger")]
    public class PartyLedger : CommonProperties
    {
        public long PartyLedgerId { get; set; }

        public DateTime? LedgerDate { get; set; }

        public string BillNo { get; set; }

        public string Type { get; set; }

        public double Balance { get; set; }

        public double Debit { get; set; }

        public double Credit { get; set; }
        public bool IsSettled { get; set; }

        public long? PartyId { get; set; }

        public Party Party { get; set; }

        public long? BankId { get; set; }

        public Bank Bank { get; set; }

        public long? ChequeRecivedId { get; set; }

        public ChequeRecived ChequeRecived { get; set; }

        public long? AmountReceivedId { get; set; }

        public AmountReceived AmountReceived { get; set; }

        public long? RefundAmountId { get; set; }

        public RefundAmount RefundAmount { get; set; }

        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }

    }
}
