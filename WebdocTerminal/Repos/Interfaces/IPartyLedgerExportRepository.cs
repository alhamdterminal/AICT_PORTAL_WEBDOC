using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IPartyLedgerExportRepository : IRepo<PartyLedgerExport>
    {
        IEnumerable<InvoiceExport> GetInvoicesNotInAmountReceivedExport();

        double GetBalanceAmountByParty(long? partyExportId);

        IEnumerable<InvoiceExport> GetUnSettledInvoices();

        List<PartyLedgerExport> GetLedgerByPartyId(long partyId);
    }
}
