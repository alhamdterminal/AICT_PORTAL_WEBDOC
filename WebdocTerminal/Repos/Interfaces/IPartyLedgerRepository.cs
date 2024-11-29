using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IPartyLedgerRepository : IRepo<PartyLedger>
    {

        double GetBalanceAmountByParty(long? partyId);

        double GetBalanceAmountByPartyForAmountReceived(long? partyId);
        IEnumerable<Invoice> GetInvoicesNotInAmountReceived();
        IEnumerable<Invoice> GetInvoicesNotInAmountReceivedExport();

        IEnumerable<PartyLedgerViewModel> GetPartyLedgerDetailPartyWise(long? partyId);

        List<PartyLedger> GetrPartyUnSettledAmount(long partyId);

        double PartyBalanceAmount(long partyid);

        Party PartyByClearnignAgentID(long ClearingAgentId);

        CreditAllowed CreditAllowedCFSBYcontainerindexid(long Containerindexid);

        CreditAllowed CreditAllowedCYbycycontainerid(long CYContainerid);

        Invoice GetInvoiceByInvoiceNo(string invoiceno);

        Invoice FindInvoiceAndupdate(string invoiceno, string newinvoiceno);

        PartyCodeAndBalanceViewModel PartyInfo(long partyid , string igm, long index, string type);

        CreditAllowed CreditAllowedCYbyid(long CYContainerid);
        CreditAllowed CreditAllowedCFSBYid(long Containerindexid);

    }
}
