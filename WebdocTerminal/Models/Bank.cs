using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Bank")]
    public class Bank : CommonProperties
    {
        public Bank()
        {
            chequeReciveds = new List<ChequeRecived>();

            ChequeRecivedExports = new List<ChequeRecivedExport>();

            RefundAmounts = new List<RefundAmount>();
        }

        public long BankId { get; set; }

        public string BankCode { get; set; }

        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string Type { get; set; }

        public List<ChequeRecived> chequeReciveds { get; set; }

        public List<ChequeRecivedExport> ChequeRecivedExports { get; set; }

        public List<RefundAmount> RefundAmounts { get; set; }

        public List<PartyLedger> PartyLedgers { get; set; }
        public List<PartyLedgerExport> PartyLedgerExports { get; set; }


    }
}
