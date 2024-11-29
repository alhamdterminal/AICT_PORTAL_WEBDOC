using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ChequeRecivedExport")]
    public class ChequeRecivedExport : CommonProperties
    {
        public long ChequeRecivedExportId { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public string Type { get; set; }

        public long Number { get; set; }

        public DateTime ChequeDate { get; set; }

        public long? PartyExportId { get; set; }

        public PartyExport PartyExport { get; set; }

        public long? BankId { get; set; }

        public Bank Bank { get; set; }

        public List<PartyLedgerExport> PartyLedgerExports { get; set; }
    }
}
