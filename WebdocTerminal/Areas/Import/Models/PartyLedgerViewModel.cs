using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class PartyLedgerViewModel
    {
        public long PartyLedgerId { get; set; }

        public DateTime? LedgerDate { get; set; }

        public string BillNo { get; set; }

        public string Number { get; set; }

        public string Type { get; set; }

        public double Balance { get; set; }

        public double Debit { get; set; }

        public double Credit { get; set; }

        public long? PartyId { get; set; }
         
        public long? BankId { get; set; }
         
        public long? ChequeRecivedId { get; set; }
         
        public long? AmountReceivedId { get; set; }
         
        public long? RefundAmountId { get; set; }
         

    }
}
