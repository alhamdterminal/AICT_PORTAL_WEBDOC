﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AmountReceivedExport")]
    public class AmountReceivedExport : CommonProperties
    {
        public long AmountReceivedExportId { get; set; }

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

        public long? InvoiceExportId { get; set; }

        public InvoiceExport InvoiceExport { get; set; }

        public long? PartyExportId { get; set; }

        public PartyExport PartyExport { get; set; }

        public List<PartyLedgerExport> PartyLedgerExports { get; set; }

    }
}