using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PartyLedgerExport")]
    public class PartyLedgerExport : CommonProperties
    {

        public long PartyLedgerExportId { get; set; }

        public DateTime? LedgerDate { get; set; }

        public string BillNo { get; set; }

        public string Type { get; set; }

        public double Balance { get; set; }

        public double Debit { get; set; }

        public double Credit { get; set; }

        public long? PartyExportId { get; set; }

        public PartyExport PartyExport { get; set; }

        public long? BankId { get; set; }

        public Bank Bank { get; set; }

        public long? ChequeRecivedExportId { get; set; }

        public ChequeRecivedExport ChequeRecivedExport { get; set; }

        public long? AmountReceivedExportId { get; set; }

        public AmountReceivedExport AmountReceivedExport { get; set; }

        public long? RefundAmountExportId { get; set; }

        public RefundAmountExport RefundAmountExport { get; set; }
    }
}
