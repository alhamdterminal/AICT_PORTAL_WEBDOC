using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{

    [Table("RefundAmountExport")]
    public class RefundAmountExport : CommonProperties
    {
        public long RefundAmountExportId { get; set; }

        public DateTime Date { get; set; }

        public double CreditAmount { get; set; }
        public double DebitAmount { get; set; }

        public string Type { get; set; }
        public string KnockOfInvoice { get; set; }
        public string chequeNumber { get; set; }
        public string Remarks { get; set; }

        public DateTime ChequeDate { get; set; }

        public long? PartyId { get; set; }

        public PartyExport Party { get; set; }

        public long? PartyExportId { get; set; }

        public PartyExport PartyExport { get; set; }

        public long? BankId { get; set; }

        public Bank Bank { get; set; }



        public List<PartyLedgerExport> PartyLedgerExports { get; set; }
    }
}
