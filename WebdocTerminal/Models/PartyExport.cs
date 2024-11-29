using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PartyExport")]
    public class PartyExport : CommonProperties
    {

        public PartyExport()
        {
            ChequeRecivedExports = new List<ChequeRecivedExport>();

            RefundAmounts = new List<RefundAmount>();

            AmountReceivedExports = new List<AmountReceivedExport>();

        }

        public long PartyExportId { get; set; }

        public string PartyName { get; set; }

        public string Consignee { get; set; }

        public List<ChequeRecivedExport> ChequeRecivedExports { get; set; }

        public List<RefundAmount> RefundAmounts { get; set; }

        public List<AmountReceivedExport> AmountReceivedExports { get; set; }

        public List<PartyLedgerExport> PartyLedgerExports { get; set; }
    }
}
