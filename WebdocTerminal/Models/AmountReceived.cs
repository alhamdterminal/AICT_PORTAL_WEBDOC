using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AmountReceived")]
    public class AmountReceived : CommonProperties
    {
        public long AmountReceivedId { get; set; }

        public DateTime AmountReceivedDate { get; set; }

        public int BalanceAmount { get; set; }

        public int SalesTax { get; set; }

        public int BillAmount { get; set; }

        public int ChequeAmount { get; set; }

        public int CashAmount { get; set; }

        public int Discount { get; set; }

        public string Type { get; set; }

        public bool AACR { get; set; }

        public int BalanceLedgerAmount { get; set; }

        public long?  InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public long? PartyId { get; set; }

        public Party Party { get; set; }

        public List<PartyLedger> PartyLedgers { get; set; }

    }
}
